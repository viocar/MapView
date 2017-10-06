using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace MapView
{
    public partial class Form1 : Form
    {
        byte[] databyte = new byte[0x396C]; //sized for the EO4+ arrays even if it's EO3 mode. is there a better way?
        int dangerstate = 0;
        int eo4state = 0;
        int ymdpos = 0;
        int floortype = 0;
        int floorvalue = 0;
        int floorencount = 0;
        int floordanger = 0;
        int floorchaos = 0;
        int[] currentcell = new int[2] { 0, 0 };
        List<TextBox> textboxes = new List<TextBox>();
        public Form1()
        {
            InitializeComponent();
            textboxes.Add(typeBox); textboxes.Add(valueBox); textboxes.Add(encBox); textboxes.Add(dangerBox); textboxes.Add(chaosBox); //populate a list for easy tracking of the boxes
            button1.Click += new EventHandler(button1_Click);
            map.CellClick += new DataGridViewCellEventHandler(map_CellClick);
            dangerCBox.CheckedChanged += new EventHandler(dangerCBox_CheckedChanged);
            EO4Box.CheckedChanged += new EventHandler(EO4Box_CheckedChanged);
            typeBox.KeyDown += new KeyEventHandler(typeBox_KeyDown); //can't really use the list here, but it's still useful
            valueBox.KeyDown += new KeyEventHandler(valueBox_KeyDown);
            encBox.KeyDown += new KeyEventHandler(encBox_KeyDown);
            dangerBox.KeyDown += new KeyEventHandler(dangerBox_KeyDown);
            chaosBox.KeyDown += new KeyEventHandler(chaosBox_KeyDown);
        }
        private void dangerCBox_CheckedChanged(object sender, EventArgs e) //danger mode. needs adjustments for EO2U
        {
            if (dangerstate == 0)
            {
                dangerstate = 1;
                InitializeMapCells();
            }
            else
            {
                dangerstate = 0;
                InitializeMapCells();
            }
        }
        private void EO4Box_CheckedChanged(object sender, EventArgs e) //3DS mode
        {
            if (eo4state == 0)
            {
                eo4state = 1;
            }
            else
            {
                eo4state = 0;
            }
        }
        private void button1_Click(object sender, EventArgs e) //load a .ymd
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = ".ymd File|*.ymd",
                Title = "Select a .ymd File",
                InitialDirectory = "D:\\Emulation\\eo3files\\data\\Data\\@Target\\Data\\MapDat\\Ymd" //should probably store the last directory but fuck it
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (BinaryReader ymdfile = new BinaryReader(new FileStream(openFileDialog1.FileName, FileMode.Open)))
                {
                    if (eo4state == 0)
                    {
                        ymdfile.BaseStream.Seek(0x90, SeekOrigin.Begin);
                        ymdfile.Read(databyte, 0, 0x20D0);
                        InitializeMapCells();
                    }
                    else
                    {
                        ymdfile.BaseStream.Seek(0x10A, SeekOrigin.Begin); //3DS games have different sizes. also this doesn't work for EO4 caves
                        ymdfile.Read(databyte, 0, 0x396C);
                        InitializeMapCells();
                    }
                }

            }
        }
        private void map_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var x = e.ColumnIndex;
            var y = e.RowIndex;
            GetMapDataForTile(x, y);
            currentcell[0] = x; currentcell[1] = y;
            textboxes[0].Text = floortype.ToString("X2"); //populate the text boxes
            textboxes[1].Text = floorvalue.ToString("X2");
            textboxes[2].Text = floorencount.ToString("X2");
            textboxes[3].Text = floordanger.ToString("X2");
            textboxes[4].Text = floorchaos.ToString("X2");
        }
        private void typeBox_KeyDown(object sender, KeyEventArgs e) //these pass through to a handler to reduce repeated code
        {
            boxHandler(0, e);
        }
        private void valueBox_KeyDown(object sender, KeyEventArgs e)
        {
            boxHandler(1, e);
        }
        private void encBox_KeyDown(object sender, KeyEventArgs e)
        {
            boxHandler(2, e);
        }
        private void dangerBox_KeyDown(object sender, KeyEventArgs e)
        {
            boxHandler(3, e);
        }
        private void chaosBox_KeyDown(object sender, KeyEventArgs e)
        {
            boxHandler(4, e);
        }
        private void boxHandler(int id, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ushort dataentry = 0;
                if (ushort.TryParse(textboxes[id].Text, out dataentry)) //textboxes[id] lets us get the correct textbox without needing lots of repeat code
                {
                    dataentry = Convert.ToUInt16(textboxes[id].Text, 16);
                    SetMapDataForTile(currentcell[0], currentcell[1], id, dataentry); //id is which one we fill
                    DrawTile(currentcell[0], currentcell[1]);
                }
            }
            e.Handled = true;
        }
        private void InitializeMapCells()
        {
            map.ColumnCount = 35; //ehhh this is mostly okay but it'll break on EO4 caves as well as the ocean voyage
            map.RowCount = 30;
            map.RowHeadersWidth = 12;
            map.ColumnHeadersHeight = 4;
            for (int y = 0; y < 30; y++) //y first because we iterate through x mainly
            {
                for (int x = 0; x < 35; x++)
                {
                    DrawTile(x, y);
                }
            }
            foreach (DataGridViewColumn column in map.Columns) //this is not actually "data", so we don't want to be able to screw it up by sorting it
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void GetMapDataForTile(int x, int y)
        {
            if (eo4state == 0)
            {
                ymdpos = (x * 8 + (y * 0x118));
                floortype = databyte[ymdpos]; //begin getting the data out of the array
                floorvalue = databyte[ymdpos + 1];
                floorencount = 0;
                if (databyte[ymdpos + 3] == 00)
                {
                    floorencount = databyte[ymdpos + 2];
                }
                else
                {
                    floorencount = BitConverter.ToUInt16(new byte[2] { (byte)databyte[ymdpos + 3], (byte)databyte[ymdpos + 2] }, 0); //this is how you convert little endian bytes in c#
                }
                floordanger = databyte[ymdpos + 4];
                floorchaos = databyte[ymdpos + 5];
            }
            else
            {
                ymdpos = (x * 14 + (y * 0x1EA));
                floortype = databyte[ymdpos + 2];   //offsets are different in the 3DS games because certain values got bumped from 8 bit to 16 bit
                floorvalue = databyte[ymdpos + 4];  //but the structure is otherwise the same
                floorencount = 0;
                if (databyte[ymdpos + 3] == 00)
                {
                    floorencount = databyte[ymdpos + 6];
                }
                else
                {
                    floorencount = BitConverter.ToUInt16(new byte[2] { (byte)databyte[ymdpos + 7], (byte)databyte[ymdpos + 6] }, 0); //this is how you convert little endian bytes in c#
                }
                floordanger = databyte[ymdpos + 8];
                floorchaos = databyte[ymdpos + 10];
            }
        }
        private void SetMapDataForTile(int x, int y, int id, ushort value)
        {
            byte[] b = new byte[2];
            b[0] = (byte)value;
            b[1] = (byte)(((uint)value >> 8) & 0xFF);
            Debug.WriteLine(b[0]); Debug.WriteLine(b[1]);
            if (eo4state == 0)
            {
                ymdpos = (x * 8 + (y * 0x118));
                switch (id)
                {
                    case 0:
                        databyte[ymdpos] = b[0];
                        break;
                    case 1:
                        databyte[ymdpos + 1] = b[0];
                        break;
                    case 2:
                        databyte[ymdpos + 2] = b[0];
                        databyte[ymdpos + 3] = b[1];
                        break;
                    case 3:
                        databyte[ymdpos + 4] = b[0];
                        break;
                    case 4:
                        databyte[ymdpos + 5] = b[0];
                        break;
                    default:
                        Debug.WriteLine("box id passed invalid ID");
                        break;
                }
            }
            else
            {
                ymdpos = (x * 14 + (y * 0x1EA));
                switch (id) //all values are 16-bit in the 3DS games
                {
                    case 0:
                        databyte[ymdpos + 2] = b[0];
                        databyte[ymdpos + 3] = b[1];
                        break;
                    case 1:
                        databyte[ymdpos + 4] = b[0];
                        databyte[ymdpos + 5] = b[1];
                        break;
                    case 2:
                        databyte[ymdpos + 6] = b[0];
                        databyte[ymdpos + 7] = b[1];
                        break;
                    case 3:
                        databyte[ymdpos + 8] = b[0];
                        databyte[ymdpos + 9] = b[1];
                        break;
                    case 4:
                        databyte[ymdpos + 10] = b[0];
                        databyte[ymdpos + 11] = b[1];
                        break;
                    default:
                        Debug.WriteLine("box id passed invalid ID");
                        break;
                }
            }
        }
        private void DrawTile(int x, int y)
        {
            map.CellBorderStyle = DataGridViewCellBorderStyle.None; //no borders
            map.DefaultCellStyle.Font = new Font("Calibri", 11F, GraphicsUnit.Pixel); //font
            DataGridViewCellStyle styleOOB = new DataGridViewCellStyle();           styleOOB.BackColor = Color.FromArgb(50, 50, 50);
            DataGridViewCellStyle styleWall = new DataGridViewCellStyle();          styleWall.BackColor = Color.FromArgb(85, 85, 85);
            DataGridViewCellStyle styleNormal = new DataGridViewCellStyle();        styleNormal.BackColor = Color.White;
            DataGridViewCellStyle styleDamage = new DataGridViewCellStyle();        styleDamage.BackColor = Color.Red;
            DataGridViewCellStyle styleSlide = new DataGridViewCellStyle();         styleSlide.BackColor = Color.Green;
            DataGridViewCellStyle styleTrap = new DataGridViewCellStyle();          styleTrap.BackColor = Color.Black;
            DataGridViewCellStyle styleForce = new DataGridViewCellStyle();         styleForce.BackColor = Color.Chartreuse;
            DataGridViewCellStyle styleMud = new DataGridViewCellStyle();           styleMud.BackColor = Color.SaddleBrown;
            DataGridViewCellStyle styleNoMap = new DataGridViewCellStyle();         styleNoMap.BackColor = Color.LightGray;
            DataGridViewCellStyle styleUnknown = new DataGridViewCellStyle();       styleUnknown.BackColor = Color.Blue;
            DataGridViewCellStyle styleSpinner = new DataGridViewCellStyle();       styleSpinner.BackColor = Color.LightYellow;
            DataGridViewCellStyle styleCampsite = new DataGridViewCellStyle();      styleCampsite.BackColor = Color.PaleVioletRed;
            DataGridViewCellStyle styleRiver = new DataGridViewCellStyle();         styleRiver.BackColor = Color.RoyalBlue;
            DataGridViewCellStyle styleImpassible = new DataGridViewCellStyle();    styleImpassible.BackColor = Color.Gray;
            DataGridViewCellStyle styleDoor = new DataGridViewCellStyle();          styleDoor.BackColor = Color.PowderBlue;
            DataGridViewCellStyle stylePole = new DataGridViewCellStyle();          stylePole.BackColor = Color.Pink;
            DataGridViewCellStyle styleStairs = new DataGridViewCellStyle();        styleStairs.BackColor = Color.Cyan;
            DataGridViewCellStyle styleChest = new DataGridViewCellStyle();         styleChest.BackColor = Color.FromArgb(177, 156, 217);
            DataGridViewCellStyle styleCrevice = new DataGridViewCellStyle();       styleCrevice.BackColor = Color.FromArgb(122, 107, 87);
            DataGridViewCellStyle styleShutters = new DataGridViewCellStyle();      styleShutters.BackColor = Color.Silver;
            DataGridViewCellStyle styleLockedDoor = new DataGridViewCellStyle();    styleLockedDoor.BackColor = Color.FromArgb(128, 64, 192);
            DataGridViewCellStyle styleTeleport = new DataGridViewCellStyle();      styleTeleport.BackColor = Color.DarkCyan;
            DataGridViewCellStyle styleShutterButton = new DataGridViewCellStyle(); styleShutterButton.BackColor = Color.OliveDrab;
            DataGridViewCellStyle styleWhirlpool = new DataGridViewCellStyle();     styleWhirlpool.BackColor = Color.LightSalmon;
            DataGridViewCellStyle styleSeaweed = new DataGridViewCellStyle();       styleSeaweed.BackColor = Color.Aquamarine;
            DataGridViewCellStyle styleRocks = new DataGridViewCellStyle();         styleRocks.BackColor = Color.DarkGray;
            DataGridViewCellStyle styleEye = new DataGridViewCellStyle();           styleEye.BackColor = Color.LightGreen;
            DataGridViewCellStyle styleDanger1 = new DataGridViewCellStyle();       styleDanger1.BackColor = Color.FromArgb(255, 255, 128);
            DataGridViewCellStyle styleDanger2 = new DataGridViewCellStyle();       styleDanger2.BackColor = Color.FromArgb(255, 192, 0);
            DataGridViewCellStyle styleDanger3 = new DataGridViewCellStyle();       styleDanger3.BackColor = Color.FromArgb(255, 128, 0);
            DataGridViewCellStyle styleDanger4 = new DataGridViewCellStyle();       styleDanger4.BackColor = Color.FromArgb(255, 0, 0);
            DataGridViewCellStyle styleDanger5 = new DataGridViewCellStyle();       styleDanger5.BackColor = Color.FromArgb(128, 0, 0);
            GetMapDataForTile(x, y);
            map[x, y].Value = floorencount.ToString("X2"); //write this first so it can be overridden if needed
            switch (floortype) //handle the floor colour
            {
                default: //unknown floor
                    map[x, y].Style = styleUnknown;
                    map[x, y].Value = floortype.ToString("X2"); //overwrite the encount to easily see what the unknown floor type is
                    break;
                case 0x0: //invalid probably
                    map[x, y].Style = styleOOB;
                    break;
                case 0x1: //normal floor. this is needed in case we load multiple maps
                    map[x, y].Style = styleNormal;
                    break;
                case 0x2: //damaging floor
                    map[x, y].Style = styleDamage;
                    break;
                case 0x3: //slide along floor
                    map[x, y].Style = styleSlide;
                    break;
                case 0x4: //booby trap
                    map[x, y].Style = styleTrap;
                    break;
                case 0x5: //force move up
                    map[x, y].Style = styleForce;
                    map[x, y].Value = "↑";
                    break;
                case 0x6: //force move down
                    map[x, y].Style = styleForce;
                    map[x, y].Value = "↓";
                    break;
                case 0x7: //force move left
                    map[x, y].Style = styleForce;
                    map[x, y].Value = "←";
                    break;
                case 0x8: //force move right
                    map[x, y].Style = styleForce;
                    map[x, y].Value = "→";
                    break;
                case 0xA:
                case 0x1D: //seems like two mud cases
                    map[x, y].Style = styleMud;
                    break;
                case 0xB: //no map
                    map[x, y].Style = styleNoMap;
                    break;
                case 0xC: //spinner
                    map[x, y].Style = styleSpinner;
                    break;
                case 0xD: //campsite
                    map[x, y].Style = styleCampsite;
                    break;
                case 0xE: //wall
                    map[x, y].Style = styleWall;
                    break;
                case 0xF: //river
                    map[x, y].Style = styleRiver;
                    break;
                case 0x10: //door
                    map[x, y].Style = styleDoor;
                    break;
                case 0x11: //submagnetic pole
                    map[x, y].Style = stylePole;
                    break;
                case 0x12: //stairs
                    map[x, y].Style = styleStairs;
                    break;
                case 0x13: //chest
                    map[x, y].Style = styleChest;
                    break;
                case 0x14: //one-way crevice
                    map[x, y].Style = styleCrevice;
                    break;
                case 0x15: //two way crevice - find a way to show the difference
                    map[x, y].Style = styleCrevice;
                    GetMapDataForTile(x + 1, y);
                    if (floortype == 0x1 || floortype == 0xB)
                    {
                        map[x, y].Value = "↔";
                    }
                    else
                    {
                        map[x, y].Value = "↕";
                    }
                    GetMapDataForTile(x, y); //ugh, how ugly
                    break;
                case 0x16: //shutters
                    map[x, y].Style = styleShutters;
                    break;
                case 0x17: //locked door
                    map[x, y].Style = styleLockedDoor;
                    break;
                case 0x18: //teleport
                    map[x, y].Style = styleTeleport;
                    break;
                case 0x19: //shutters button
                    map[x, y].Style = styleShutterButton;
                    break;
                case 0x1A: //whirlpool
                    map[x, y].Style = styleWhirlpool;
                    break;
                case 0x1B: //seaweed
                    map[x, y].Style = styleSeaweed;
                    break;
                case 0x1C: //rocks
                    map[x, y].Style = styleRocks;
                    break;
                case 0x1E: //FOE eye
                    map[x, y].Style = styleEye;
                    break;
            }
            if (dangerstate == 1)
            {
                switch (floortype)
                {
                    default:
                        break;
                    case 0x1:
                    case 0x2:
                    case 0xB:
                        switch (floordanger)
                        {
                            case 0:
                                map[x, y].Style = styleNormal;
                                break;
                            case 1:
                                map[x, y].Style = styleDanger1;
                                break;
                            case 2:
                                map[x, y].Style = styleDanger2;
                                break;
                            case 3:
                                map[x, y].Style = styleDanger3;
                                break;
                            case 4:
                                map[x, y].Style = styleDanger4;
                                break;
                            case 5:
                                map[x, y].Style = styleDanger5;
                                break;
                        }
                        break;
                }
            }
        }
    }
}
