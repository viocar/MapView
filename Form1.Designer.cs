namespace MapView
{
    partial class MapView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ymd_TypeBox = new System.Windows.Forms.TextBox();
            this.ymd_ValueBox = new System.Windows.Forms.TextBox();
            this.ymd_EncBox = new System.Windows.Forms.TextBox();
            this.ymd_DangerBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.map = new System.Windows.Forms.DataGridView();
            this.ymd_DangerCBox = new System.Windows.Forms.CheckBox();
            this.EO4Box = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.ymd_GroupBox = new System.Windows.Forms.CheckBox();
            this.openFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.ymd_label = new System.Windows.Forms.Label();
            this.ydd_label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ydd_AngleBox = new System.Windows.Forms.TextBox();
            this.ydd_TypeBox = new System.Windows.Forms.TextBox();
            this.ydd_ShowBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
            this.SuspendLayout();
            // 
            // ymd_TypeBox
            // 
            this.ymd_TypeBox.Location = new System.Drawing.Point(842, 29);
            this.ymd_TypeBox.Name = "ymd_TypeBox";
            this.ymd_TypeBox.Size = new System.Drawing.Size(40, 20);
            this.ymd_TypeBox.TabIndex = 0;
            this.ymd_TypeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ymd_ValueBox
            // 
            this.ymd_ValueBox.Location = new System.Drawing.Point(842, 55);
            this.ymd_ValueBox.Name = "ymd_ValueBox";
            this.ymd_ValueBox.Size = new System.Drawing.Size(40, 20);
            this.ymd_ValueBox.TabIndex = 1;
            this.ymd_ValueBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ymd_EncBox
            // 
            this.ymd_EncBox.Location = new System.Drawing.Point(842, 81);
            this.ymd_EncBox.Name = "ymd_EncBox";
            this.ymd_EncBox.Size = new System.Drawing.Size(40, 20);
            this.ymd_EncBox.TabIndex = 2;
            this.ymd_EncBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ymd_DangerBox
            // 
            this.ymd_DangerBox.Location = new System.Drawing.Point(842, 107);
            this.ymd_DangerBox.Name = "ymd_DangerBox";
            this.ymd_DangerBox.Size = new System.Drawing.Size(40, 20);
            this.ymd_DangerBox.TabIndex = 3;
            this.ymd_DangerBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(805, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(801, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Value";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(780, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Encounter";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(794, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Danger";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(786, 267);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(96, 23);
            this.loadButton.TabIndex = 11;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // map
            // 
            this.map.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.map.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.map.Location = new System.Drawing.Point(13, 13);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(767, 674);
            this.map.TabIndex = 12;
            // 
            // ymd_DangerCBox
            // 
            this.ymd_DangerCBox.AutoSize = true;
            this.ymd_DangerCBox.Location = new System.Drawing.Point(793, 133);
            this.ymd_DangerCBox.Name = "ymd_DangerCBox";
            this.ymd_DangerCBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ymd_DangerCBox.Size = new System.Drawing.Size(89, 17);
            this.ymd_DangerCBox.TabIndex = 13;
            this.ymd_DangerCBox.Text = "Show danger";
            this.ymd_DangerCBox.UseVisualStyleBackColor = true;
            this.ymd_DangerCBox.CheckedChanged += new System.EventHandler(this.dangerCBox_CheckedChanged_1);
            // 
            // EO4Box
            // 
            this.EO4Box.AutoSize = true;
            this.EO4Box.Location = new System.Drawing.Point(829, 670);
            this.EO4Box.Name = "EO4Box";
            this.EO4Box.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EO4Box.Size = new System.Drawing.Size(53, 17);
            this.EO4Box.TabIndex = 14;
            this.EO4Box.Text = "+EO4";
            this.EO4Box.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(786, 296);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(96, 23);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // ymd_GroupBox
            // 
            this.ymd_GroupBox.AutoSize = true;
            this.ymd_GroupBox.Location = new System.Drawing.Point(786, 156);
            this.ymd_GroupBox.Name = "ymd_GroupBox";
            this.ymd_GroupBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ymd_GroupBox.Size = new System.Drawing.Size(96, 17);
            this.ymd_GroupBox.TabIndex = 16;
            this.ymd_GroupBox.Text = "Encount group";
            this.ymd_GroupBox.UseVisualStyleBackColor = true;
            // 
            // ymd_label
            // 
            this.ymd_label.AutoSize = true;
            this.ymd_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ymd_label.Location = new System.Drawing.Point(851, 13);
            this.ymd_label.Name = "ymd_label";
            this.ymd_label.Size = new System.Drawing.Size(31, 13);
            this.ymd_label.TabIndex = 17;
            this.ymd_label.Text = "YMD";
            this.ymd_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.ymd_label.Click += new System.EventHandler(this.ymd_label_Click);
            // 
            // ydd_label
            // 
            this.ydd_label.AutoSize = true;
            this.ydd_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ydd_label.Location = new System.Drawing.Point(851, 176);
            this.ydd_label.Name = "ydd_label";
            this.ydd_label.Size = new System.Drawing.Size(30, 13);
            this.ydd_label.TabIndex = 18;
            this.ydd_label.Text = "YDD";
            this.ydd_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(800, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Angle";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(804, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Type";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ydd_AngleBox
            // 
            this.ydd_AngleBox.Location = new System.Drawing.Point(841, 218);
            this.ydd_AngleBox.Name = "ydd_AngleBox";
            this.ydd_AngleBox.Size = new System.Drawing.Size(40, 20);
            this.ydd_AngleBox.TabIndex = 20;
            this.ydd_AngleBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ydd_TypeBox
            // 
            this.ydd_TypeBox.Location = new System.Drawing.Point(841, 192);
            this.ydd_TypeBox.Name = "ydd_TypeBox";
            this.ydd_TypeBox.Size = new System.Drawing.Size(40, 20);
            this.ydd_TypeBox.TabIndex = 19;
            this.ydd_TypeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ydd_ShowBox
            // 
            this.ydd_ShowBox.AutoSize = true;
            this.ydd_ShowBox.Location = new System.Drawing.Point(829, 244);
            this.ydd_ShowBox.Name = "ydd_ShowBox";
            this.ydd_ShowBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ydd_ShowBox.Size = new System.Drawing.Size(53, 17);
            this.ydd_ShowBox.TabIndex = 23;
            this.ydd_ShowBox.Text = "Show";
            this.ydd_ShowBox.UseVisualStyleBackColor = true;
            // 
            // MapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 736);
            this.Controls.Add(this.ydd_ShowBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ydd_AngleBox);
            this.Controls.Add(this.ydd_TypeBox);
            this.Controls.Add(this.ydd_label);
            this.Controls.Add(this.ymd_label);
            this.Controls.Add(this.ymd_GroupBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.EO4Box);
            this.Controls.Add(this.ymd_DangerCBox);
            this.Controls.Add(this.map);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ymd_DangerBox);
            this.Controls.Add(this.ymd_EncBox);
            this.Controls.Add(this.ymd_ValueBox);
            this.Controls.Add(this.ymd_TypeBox);
            this.Name = "MapView";
            this.Text = "MapView";
            ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ymd_TypeBox;
        private System.Windows.Forms.TextBox ymd_ValueBox;
        private System.Windows.Forms.TextBox ymd_EncBox;
        private System.Windows.Forms.TextBox ymd_DangerBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView map;
        private System.Windows.Forms.CheckBox ymd_DangerCBox;
        private System.Windows.Forms.CheckBox EO4Box;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox ymd_GroupBox;
        private System.Windows.Forms.FolderBrowserDialog openFolder;
        private System.Windows.Forms.Label ymd_label;
        private System.Windows.Forms.Label ydd_label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ydd_AngleBox;
        private System.Windows.Forms.TextBox ydd_TypeBox;
        private System.Windows.Forms.CheckBox ydd_ShowBox;
    }
}

