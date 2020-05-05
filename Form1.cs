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
    public partial class MapView : Form
    {
        byte[] ymd_data = new byte[0x396C]; //0x396C = EO4+, 0x20D0 = EO3
        byte[] ydd_data = new byte[0x834]; //dunno EO4+
        byte[] ymd_file;
        byte[] ydd_file;
        bool danger_state = false;
        bool group_state = false;
        //bool eo4_state = false;
        bool ydd_show_state = false;
        int ymd_pos = 0;
        int ydd_pos = 0;
        int ymd_type = 0;
        int ymd_value = 0;
        int ymd_encount = 0;
        int ymd_danger = 0;
        int ydd_type = 0;
        int ydd_angle = 0;
        int[] current_cell = new int[2] { 0, 0 };
        List<TextBox> textboxes = new List<TextBox>();
        List<int> encounts = new List<int>();
        public MapView()
        {
            InitializeComponent();
            textboxes.Add(ymd_TypeBox); textboxes.Add(ymd_ValueBox); textboxes.Add(ymd_EncBox); textboxes.Add(ymd_DangerBox); textboxes.Add(ydd_TypeBox); textboxes.Add(ydd_AngleBox); //populate a list for easy tracking of the boxes
            loadButton.Click += new EventHandler(button1_Click);
            saveButton.Click += new EventHandler(button2_Click);
            map.CellEnter += new DataGridViewCellEventHandler(map_CellEnter);
            //map.KeyDown += new DataGridViewCellEventHandler(map_KeyDown);
            ymd_DangerCBox.CheckedChanged += new EventHandler(ymd_DangerCBox_CheckedChanged);
            ymd_GroupBox.CheckedChanged += new EventHandler(ymd_GroupBox_CheckedChanged);
            ydd_ShowBox.CheckedChanged += new EventHandler(ydd_ShowBox_CheckedChanged);
            ymd_TypeBox.KeyDown += new KeyEventHandler(ymd_TypeBox_KeyDown); //can't really use the list here, but it's still useful
            ymd_ValueBox.KeyDown += new KeyEventHandler(ymd_ValueBox_KeyDown);
            ymd_EncBox.KeyDown += new KeyEventHandler(ymd_EncBox_KeyDown);
            ymd_DangerBox.KeyDown += new KeyEventHandler(ymd_DangerBox_KeyDown);
            ydd_TypeBox.KeyDown += new KeyEventHandler(ydd_TypeBox_KeyDown);
            ydd_AngleBox.KeyDown += new KeyEventHandler(ydd_AngleBox_KeyDown);
        }
        private void ymd_DangerCBox_CheckedChanged(object sender, EventArgs e) //danger mode. needs adjustments for EO2U
        {
            danger_state = !danger_state;
            InitializeMapCells();
        }
        private void ymd_GroupBox_CheckedChanged(object sender, EventArgs e)
        {
            group_state = !group_state;
            InitializeMapCells();
        }
        private void ydd_ShowBox_CheckedChanged(object sender, EventArgs e)
        {
            ydd_show_state = !ydd_show_state;
            InitializeMapCells();
        }
        private void button1_Click(object sender, EventArgs e) //load a .ymd
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = ".ymd File|*.ymd",
                Title = "Select MapDat folder",
                InitialDirectory = GetInitialDirectory() //should probably store the last directory but fuck it
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String map_id = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                String directory_name = Path.GetDirectoryName(openFileDialog1.FileName);
                String ymd_name = openFileDialog1.FileName;
                String ydd_name = String.Concat(directory_name.Substring(0, directory_name.Length - 4), "\\Ydd\\", map_id, ".ydd");
                ymd_file = File.ReadAllBytes(ymd_name);
                ydd_file = File.ReadAllBytes(ydd_name);
                Array.Copy(ymd_file, 0x90, ymd_data, 0, 0x20D0); //something like 0x10A and 0x396C for EO4 - I dunno, check it if you need that functionality again
                Array.Copy(ydd_file, 0x1804, ydd_data, 0, 0x834);
                Properties.Settings.Default.LastPath = Path.GetDirectoryName(openFileDialog1.FileName);
                CreateEncountList();
                InitializeMapCells();
                InitializeSelection();
            }
        }
        private string GetInitialDirectory()
        {
            string dir = "C:\\Emulation\\EOMod\\EO3Rv2\\Data\\@Target\\Data\\MapDat\\Ymd"; //my personal directory
            if (Properties.Settings.Default.LastPath != "")
            {
                return Properties.Settings.Default.LastPath;
            }
            if (!Directory.Exists(dir))
            {
                return "%userprofile%\\Documents"; //if you don't have this, I can't really help you
            }
            else
            {
                return dir;
            }
        }
        private void button2_Click(object sender, EventArgs e) //save as
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = ".ymd File|*.ymd",
                Title = "Save as...",
                InitialDirectory = GetInitialDirectory()
            };
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String map_id = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                String directory_name = Path.GetDirectoryName(saveFileDialog1.FileName);
                String ymd_name = saveFileDialog1.FileName;
                String ydd_name = String.Concat(directory_name.Substring(0, directory_name.Length - 4), "\\Ydd\\", map_id, ".ydd");
                Array.Copy(ymd_data, 0, ymd_file, 0x90, 0x20D0); //something like 0x10A and 0x396C for EO4 - I dunno, check it if you need that functionality again
                Array.Copy(ydd_data, 0, ydd_file, 0x1804, 0x834);
                File.WriteAllBytes(ymd_name, ymd_file);
                File.WriteAllBytes(ydd_name, ydd_file);
            }
        }
        private void map_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            for (int box_idx = 0; box_idx <= 5; box_idx++)
            {
                boxHandler(box_idx);
            }
            GetMapDataForTile(e.ColumnIndex, e.RowIndex);
            current_cell[0] = e.ColumnIndex; current_cell[1] = e.RowIndex;
            textboxes[0].Text = ymd_type.ToString("X2"); //populate the text boxes
            textboxes[1].Text = ymd_value.ToString("X2");
            textboxes[2].Text = ymd_encount.ToString("X2");
            textboxes[3].Text = ymd_danger.ToString("X2");
            textboxes[4].Text = ydd_type.ToString("X2");
            textboxes[5].Text = ydd_angle.ToString("X2");
        }
        private void ymd_TypeBox_KeyDown(object sender, KeyEventArgs e) //these pass through to a handler to reduce repeated code
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                boxHandler(0);
            }
        }
        private void ymd_ValueBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                boxHandler(1);
            }
        }
        private void ymd_EncBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                boxHandler(2);
            }
        }
        private void ymd_DangerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                boxHandler(3);
            }
        }
        private void ydd_TypeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                boxHandler(4);
            }
        }
        private void ydd_AngleBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                boxHandler(5);
            }
        }
        private void boxHandler(int id)
        {
            ushort data_entry = 0;
            if (ushort.TryParse(textboxes[id].Text, System.Globalization.NumberStyles.HexNumber, null, out data_entry)) //textboxes[id] lets us get the correct textbox without needing lots of repeat code
            {
                data_entry = Convert.ToUInt16(textboxes[id].Text, 16);
                Console.WriteLine(data_entry);
                SetMapDataForTile(current_cell[0], current_cell[1], id, data_entry); //id is which one we fill
                DrawTile(current_cell[0], current_cell[1]);
            }
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
        private void InitializeSelection()
        {
            danger_state = false;
            group_state = false;
            ydd_show_state = false;
            ymd_pos = 0;
            ydd_pos = 0;
            ymd_type = 0;
            ymd_value = 0;
            ymd_encount = 0;
            ymd_danger = 0;
            ydd_type = 0;
            ydd_angle = 0;
            current_cell = new int[2] { 0, 0 };
            textboxes[0].Text = ymd_type.ToString("X2"); //populate the text boxes
            textboxes[1].Text = ymd_value.ToString("X2");
            textboxes[2].Text = ymd_encount.ToString("X2");
            textboxes[3].Text = ymd_danger.ToString("X2");
            textboxes[4].Text = ydd_type.ToString("X2");
            textboxes[5].Text = ydd_angle.ToString("X2");
        }
        private void CreateEncountList() //EO3 only because I don't care
        {
            int pos = 0;
            encounts.Clear();
            for (int x = 0; x < 1050; x++)
            {
                ymd_encount = 0;
                pos = 2 + (x * 8);
                int encval = ymd_data[pos];
                if (!encounts.Contains(encval))
                {
                    encounts.Add(encval);
                }
            }
        }
        private void GetMapDataForTile(int x, int y)
        {
            ymd_pos = (x * 8 + (y * 0x118));
            ydd_pos = (x * 2 + (y * 0x46));
            //ymd data
            ymd_type = ymd_data[ymd_pos]; //begin getting the data out of the array
            ymd_value = ymd_data[ymd_pos + 1];
            ymd_encount = 0;
            if (ymd_data[ymd_pos + 3] == 00)
            {
                ymd_encount = ymd_data[ymd_pos + 2];
            }
            else
            {
                ymd_encount = BitConverter.ToUInt16(new byte[2] { (byte)ymd_data[ymd_pos + 3], (byte)ymd_data[ymd_pos + 2] }, 0); //this is how you convert little endian bytes in c#
            }
            ymd_danger = ymd_data[ymd_pos + 4];
            //ydd data
            ydd_type = ydd_data[ydd_pos];
            ydd_angle = ydd_data[ydd_pos + 1];
        }
        private void SetMapDataForTile(int x, int y, int id, ushort value)
        {
            byte[] b = new byte[2];
            b[0] = (byte)value;
            b[1] = (byte)(((uint)value >> 8) & 0xFF);
            ymd_pos = (x * 8 + (y * 0x118));
            ydd_pos = (x * 2 + (y * 0x46));
            switch (id)
            {
                case 0:
                    ymd_data[ymd_pos] = b[0];
                    break;
                case 1:
                    ymd_data[ymd_pos + 1] = b[0];
                    break;
                case 2:
                    ymd_data[ymd_pos + 2] = b[0];
                    ymd_data[ymd_pos + 3] = b[1];
                    break;
                case 3:
                    ymd_data[ymd_pos + 4] = b[0];
                    break;
                case 4:
                    ydd_data[ydd_pos] = b[0];
                    break;
                case 5:
                    ydd_data[ydd_pos + 1] = b[0];
                    break;
                default:
                    Debug.WriteLine(String.Concat("box id passed invalid ID", id));
                    break;
            }
        }
        private void DrawTile(int x, int y)
        {
            map.CellBorderStyle = DataGridViewCellBorderStyle.None; //no borders
            map.DefaultCellStyle.Font = new Font("Calibri", 11F, GraphicsUnit.Pixel); //font
            GetMapDataForTile(x, y);
            if (!ydd_show_state)
            {
                map[x, y].Value = ymd_encount.ToString("X2"); //write this first so it can be overridden if needed
            }
            else
            {
                map[x, y].Value = ydd_type.ToString("X2");
            }
            switch (ymd_type) //handle the floor colour
            {
                default: //unknown floor
                    DataGridViewCellStyle styleUnknown = new DataGridViewCellStyle(); styleUnknown.BackColor = Color.Blue;
                    map[x, y].Style = styleUnknown;
                    map[x, y].Value = ymd_type.ToString("X2"); //overwrite the encount to easily see what the unknown floor type is
                    break;
                case 0x0: //invalid probably
                    DataGridViewCellStyle styleOOB = new DataGridViewCellStyle(); styleOOB.BackColor = Color.FromArgb(50, 50, 50);
                    map[x, y].Style = styleOOB;
                    break;
                case 0x1: //normal floor. this is needed in case we load multiple maps
                    DataGridViewCellStyle styleNormal = new DataGridViewCellStyle(); styleNormal.BackColor = Color.White;
                    map[x, y].Style = styleNormal;
                    break;
                case 0x2: //damaging floor
                    DataGridViewCellStyle styleDamage = new DataGridViewCellStyle(); styleDamage.BackColor = Color.Red;
                    map[x, y].Style = styleDamage;
                    break;
                case 0x3: //slide along floor
                    DataGridViewCellStyle styleSlide = new DataGridViewCellStyle(); styleSlide.BackColor = Color.Green;
                    map[x, y].Style = styleSlide;
                    break;
                case 0x4: //booby trap
                    DataGridViewCellStyle styleTrap = new DataGridViewCellStyle(); styleTrap.BackColor = Color.Black;
                    map[x, y].Style = styleTrap;
                    break;
                case 0x5: //force move up
                    DataGridViewCellStyle styleForceU = new DataGridViewCellStyle(); styleForceU.BackColor = Color.Chartreuse;
                    map[x, y].Style = styleForceU;
                    map[x, y].Value = "↑";
                    break;
                case 0x6: //force move down
                    DataGridViewCellStyle styleForceD = new DataGridViewCellStyle(); styleForceD.BackColor = Color.Chartreuse;
                    map[x, y].Style = styleForceD;
                    map[x, y].Value = "↓";
                    break;
                case 0x7: //force move left
                    DataGridViewCellStyle styleForceL = new DataGridViewCellStyle(); styleForceL.BackColor = Color.Chartreuse;
                    map[x, y].Style = styleForceL;
                    map[x, y].Value = "←";
                    break;
                case 0x8: //force move right
                    DataGridViewCellStyle styleForceR = new DataGridViewCellStyle(); styleForceR.BackColor = Color.Chartreuse;
                    map[x, y].Style = styleForceR;
                    map[x, y].Value = "→";
                    break;
                case 0xA: //mud
                    DataGridViewCellStyle styleMud = new DataGridViewCellStyle(); styleMud.BackColor = Color.SaddleBrown;
                    map[x, y].Style = styleMud;
                    break;
                case 0xB: //no map
                    DataGridViewCellStyle styleNoMap = new DataGridViewCellStyle(); styleNoMap.BackColor = Color.LightGray;
                    map[x, y].Style = styleNoMap;
                    break;
                case 0xC: //spinner
                    DataGridViewCellStyle styleSpinner = new DataGridViewCellStyle(); styleSpinner.BackColor = Color.LightYellow;
                    map[x, y].Style = styleSpinner;
                    break;
                case 0xD: //campsite
                    DataGridViewCellStyle styleCampsite = new DataGridViewCellStyle(); styleCampsite.BackColor = Color.PaleVioletRed;
                    map[x, y].Style = styleCampsite;
                    break;
                case 0xE: //wall
                    DataGridViewCellStyle styleWall = new DataGridViewCellStyle(); styleWall.BackColor = Color.FromArgb(85, 85, 85);
                    map[x, y].Style = styleWall;
                    break;
                case 0xF: //river
                    DataGridViewCellStyle styleRiver = new DataGridViewCellStyle(); styleRiver.BackColor = Color.RoyalBlue;
                    map[x, y].Style = styleRiver;
                    break;
                case 0x10: //door
                    DataGridViewCellStyle styleDoor = new DataGridViewCellStyle(); styleDoor.BackColor = Color.PowderBlue;
                    map[x, y].Style = styleDoor;
                    break;
                case 0x11: //submagnetic pole
                    DataGridViewCellStyle stylePole = new DataGridViewCellStyle(); stylePole.BackColor = Color.Pink;
                    map[x, y].Style = stylePole;
                    break;
                case 0x12: //stairs
                    DataGridViewCellStyle styleStairs = new DataGridViewCellStyle(); styleStairs.BackColor = Color.Cyan;
                    map[x, y].Style = styleStairs;
                    break;
                case 0x13: //chest
                    DataGridViewCellStyle styleChest = new DataGridViewCellStyle(); styleChest.BackColor = Color.FromArgb(177, 156, 217);
                    map[x, y].Style = styleChest;
                    break;
                case 0x14: //one-way crevice
                    DataGridViewCellStyle styleCreviceO = new DataGridViewCellStyle(); styleCreviceO.BackColor = Color.FromArgb(122, 107, 87);
                    map[x, y].Style = styleCreviceO;
                    break;
                case 0x15: //two way crevice - find a way to show the difference
                    DataGridViewCellStyle styleCreviceT = new DataGridViewCellStyle(); styleCreviceT.BackColor = Color.FromArgb(122, 107, 87);
                    map[x, y].Style = styleCreviceT;
                    GetMapDataForTile(x + 1, y);
                    if (ymd_type == 0x1 || ymd_type == 0xB)
                    {
                        map[x, y].Value = "↔";
                    }
                    else
                    {
                        map[x, y].Value = "↕";
                    }
                    GetMapDataForTile(x, y); //this prevents a visual bug and I don't know how
                    break;
                case 0x16: //shutters
                    DataGridViewCellStyle styleShutters = new DataGridViewCellStyle(); styleShutters.BackColor = Color.Silver;
                    map[x, y].Style = styleShutters;
                    break;
                case 0x17: //locked door
                    DataGridViewCellStyle styleLockedDoor = new DataGridViewCellStyle(); styleLockedDoor.BackColor = Color.FromArgb(128, 64, 192);
                    map[x, y].Style = styleLockedDoor;
                    break;
                case 0x18: //teleport
                    DataGridViewCellStyle styleTeleport = new DataGridViewCellStyle(); styleTeleport.BackColor = Color.DarkCyan;
                    map[x, y].Style = styleTeleport;
                    break;
                case 0x19: //shutters button
                    DataGridViewCellStyle styleShutterButton = new DataGridViewCellStyle(); styleShutterButton.BackColor = Color.OliveDrab;
                    map[x, y].Style = styleShutterButton;
                    break;
                case 0x1A: //whirlpool
                    DataGridViewCellStyle styleWhirlpool = new DataGridViewCellStyle(); styleWhirlpool.BackColor = Color.LightSalmon;
                    map[x, y].Style = styleWhirlpool;
                    break;
                case 0x1B: //seaweed
                    DataGridViewCellStyle styleSeaweed = new DataGridViewCellStyle(); styleSeaweed.BackColor = Color.Aquamarine;
                    map[x, y].Style = styleSeaweed;
                    break;
                case 0x1C: //rocks
                    DataGridViewCellStyle styleRocks = new DataGridViewCellStyle(); styleRocks.BackColor = Color.DarkGray;
                    map[x, y].Style = styleRocks;
                    break;
                case 0x1D: //no map mud
                    DataGridViewCellStyle styleMudNoMap = new DataGridViewCellStyle(); styleMudNoMap.BackColor = Color.Sienna;
                    map[x, y].Style = styleMudNoMap;
                    break;
                case 0x1E: //FOE eye
                    DataGridViewCellStyle styleEye = new DataGridViewCellStyle(); styleEye.BackColor = Color.LightGreen;
                    map[x, y].Style = styleEye;
                    break;
            }
            if (danger_state)
            {
                switch (ymd_type)
                {
                    default:
                        break;
                    case 0x1:
                    case 0x2:
                    case 0xB:
                        switch (ymd_danger)
                        {
                            case 0:
                                DataGridViewCellStyle styleNormal = new DataGridViewCellStyle(); styleNormal.BackColor = Color.White;
                                map[x, y].Style = styleNormal;
                                break;
                            case 1:
                                DataGridViewCellStyle styleDanger1 = new DataGridViewCellStyle(); styleDanger1.BackColor = Color.FromArgb(255, 255, 128);
                                map[x, y].Style = styleDanger1;
                                break;
                            case 2:
                                DataGridViewCellStyle styleDanger2 = new DataGridViewCellStyle(); styleDanger2.BackColor = Color.FromArgb(255, 192, 0);
                                map[x, y].Style = styleDanger2;
                                break;
                            case 3:
                                DataGridViewCellStyle styleDanger3 = new DataGridViewCellStyle(); styleDanger3.BackColor = Color.FromArgb(255, 128, 0);
                                map[x, y].Style = styleDanger3;
                                break;
                            case 4:
                                DataGridViewCellStyle styleDanger4 = new DataGridViewCellStyle(); styleDanger4.BackColor = Color.FromArgb(255, 0, 0);
                                map[x, y].Style = styleDanger4;
                                break;
                            case 5:
                                DataGridViewCellStyle styleDanger5 = new DataGridViewCellStyle(); styleDanger5.BackColor = Color.FromArgb(192, 0, 0);
                                map[x, y].Style = styleDanger5;
                                break;
                            case 6:
                                DataGridViewCellStyle styleDanger6 = new DataGridViewCellStyle(); styleDanger6.BackColor = Color.FromArgb(160, 0, 0);
                                map[x, y].Style = styleDanger6;
                                break;
                            case 7:
                                DataGridViewCellStyle styleDanger7 = new DataGridViewCellStyle(); styleDanger7.BackColor = Color.FromArgb(128, 0, 0);
                                map[x, y].Style = styleDanger7;
                                break;
                            case 8:
                                DataGridViewCellStyle styleDanger8 = new DataGridViewCellStyle(); styleDanger8.BackColor = Color.FromArgb(96, 0, 0);
                                map[x, y].Style = styleDanger8;
                                break;
                            case 9:
                            default:
                                DataGridViewCellStyle styleDanger9 = new DataGridViewCellStyle(); styleDanger9.BackColor = Color.FromArgb(64, 0, 0);
                                map[x, y].Style = styleDanger9;
                                break;
                        }
                        break;
                }
            }
            if (group_state)
            {
                switch (ymd_type)
                {
                    default:
                        break;
                    case 0x1:
                    case 0x2:
                    case 0xA:
                    case 0xB:
                    case 0xD:
                    case 0x1D:
                        if (ymd_encount == encounts[0])
                        {
                            DataGridViewCellStyle styleEncount0 = new DataGridViewCellStyle(); styleEncount0.BackColor = Color.FromArgb(255, 255, 128);
                            map[x, y].Style = styleEncount0;
                        }
                        else if (ymd_encount == encounts[1])
                        {
                            DataGridViewCellStyle styleEncount1 = new DataGridViewCellStyle(); styleEncount1.BackColor = Color.FromArgb(128, 255, 255);
                            map[x, y].Style = styleEncount1;
                        }
                        else if (ymd_encount == encounts[2])
                        {
                            DataGridViewCellStyle styleEncount2 = new DataGridViewCellStyle(); styleEncount2.BackColor = Color.FromArgb(255, 128, 255);
                            map[x, y].Style = styleEncount2;
                        }
                        else if (ymd_encount == encounts[3])
                        {
                            DataGridViewCellStyle styleEncount3 = new DataGridViewCellStyle(); styleEncount3.BackColor = Color.FromArgb(128, 255, 128);
                            map[x, y].Style = styleEncount3;
                        }
                        else if (ymd_encount == encounts[4])
                        {
                            DataGridViewCellStyle styleEncount4 = new DataGridViewCellStyle(); styleEncount4.BackColor = Color.FromArgb(255, 128, 128);
                            map[x, y].Style = styleEncount4;
                        }
                        else if (ymd_encount == encounts[5])
                        {
                            DataGridViewCellStyle styleEncount5 = new DataGridViewCellStyle(); styleEncount5.BackColor = Color.FromArgb(128, 128, 255);
                            map[x, y].Style = styleEncount5;
                        }
                        else if (ymd_encount == encounts[6])
                        {
                            DataGridViewCellStyle styleEncount6 = new DataGridViewCellStyle(); styleEncount6.BackColor = Color.FromArgb(192, 192, 192);
                            map[x, y].Style = styleEncount6;
                        }
                        else if (ymd_encount == encounts[7])
                        {
                            DataGridViewCellStyle styleEncount7 = new DataGridViewCellStyle(); styleEncount7.BackColor = Color.FromArgb(0, 255, 0);
                            map[x, y].Style = styleEncount7;
                        }
                        break;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dangerCBox_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void ymd_label_Click(object sender, EventArgs e)
        {

        }
    }
}
