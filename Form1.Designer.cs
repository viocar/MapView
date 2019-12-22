namespace MapView
{
    partial class Form1
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
            this.typeBox = new System.Windows.Forms.TextBox();
            this.valueBox = new System.Windows.Forms.TextBox();
            this.encBox = new System.Windows.Forms.TextBox();
            this.dangerBox = new System.Windows.Forms.TextBox();
            this.chaosBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.map = new System.Windows.Forms.DataGridView();
            this.dangerCBox = new System.Windows.Forms.CheckBox();
            this.EO4Box = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupsBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
            this.SuspendLayout();
            // 
            // typeBox
            // 
            this.typeBox.Location = new System.Drawing.Point(842, 12);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(40, 20);
            this.typeBox.TabIndex = 0;
            this.typeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(842, 38);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(40, 20);
            this.valueBox.TabIndex = 1;
            this.valueBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // encBox
            // 
            this.encBox.Location = new System.Drawing.Point(842, 64);
            this.encBox.Name = "encBox";
            this.encBox.Size = new System.Drawing.Size(40, 20);
            this.encBox.TabIndex = 2;
            this.encBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dangerBox
            // 
            this.dangerBox.Location = new System.Drawing.Point(842, 90);
            this.dangerBox.Name = "dangerBox";
            this.dangerBox.Size = new System.Drawing.Size(40, 20);
            this.dangerBox.TabIndex = 3;
            this.dangerBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chaosBox
            // 
            this.chaosBox.Location = new System.Drawing.Point(842, 116);
            this.chaosBox.Name = "chaosBox";
            this.chaosBox.Size = new System.Drawing.Size(40, 20);
            this.chaosBox.TabIndex = 4;
            this.chaosBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(805, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(801, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Value";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(780, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Encounter";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(794, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Danger";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(783, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "ＣＨＡＯＳ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(786, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Load .ymd";
            this.button1.UseVisualStyleBackColor = true;
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
            // dangerCBox
            // 
            this.dangerCBox.AutoSize = true;
            this.dangerCBox.Location = new System.Drawing.Point(793, 171);
            this.dangerCBox.Name = "dangerCBox";
            this.dangerCBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dangerCBox.Size = new System.Drawing.Size(89, 17);
            this.dangerCBox.TabIndex = 13;
            this.dangerCBox.Text = "Show danger";
            this.dangerCBox.UseVisualStyleBackColor = true;
            // 
            // EO4Box
            // 
            this.EO4Box.AutoSize = true;
            this.EO4Box.Location = new System.Drawing.Point(829, 194);
            this.EO4Box.Name = "EO4Box";
            this.EO4Box.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EO4Box.Size = new System.Drawing.Size(53, 17);
            this.EO4Box.TabIndex = 14;
            this.EO4Box.Text = "+EO4";
            this.EO4Box.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(807, 240);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Save As...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupsBox
            // 
            this.groupsBox.AutoSize = true;
            this.groupsBox.Location = new System.Drawing.Point(784, 217);
            this.groupsBox.Name = "groupsBox";
            this.groupsBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupsBox.Size = new System.Drawing.Size(98, 17);
            this.groupsBox.TabIndex = 16;
            this.groupsBox.Text = "Encount Group";
            this.groupsBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 736);
            this.Controls.Add(this.groupsBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.EO4Box);
            this.Controls.Add(this.dangerCBox);
            this.Controls.Add(this.map);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chaosBox);
            this.Controls.Add(this.dangerBox);
            this.Controls.Add(this.encBox);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.typeBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox typeBox;
        private System.Windows.Forms.TextBox valueBox;
        private System.Windows.Forms.TextBox encBox;
        private System.Windows.Forms.TextBox dangerBox;
        private System.Windows.Forms.TextBox chaosBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView map;
        private System.Windows.Forms.CheckBox dangerCBox;
        private System.Windows.Forms.CheckBox EO4Box;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox groupsBox;
    }
}

