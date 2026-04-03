namespace ParallelPCTester
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            txt_sendspan = new TextBox();
            txt_losttime = new TextBox();
            txt_timeout = new TextBox();
            cmb_rate = new ComboBox();
            cmb_port = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            btn_port = new Button();
            button2 = new Button();
            groupBox2 = new GroupBox();
            num_row = new NumericUpDown();
            num_col = new NumericUpDown();
            panel1 = new Panel();
            linkLabel2 = new LinkLabel();
            chk_cheat = new CheckBox();
            rad_sc = new RadioButton();
            rad_pc = new RadioButton();
            rad_pb = new RadioButton();
            rad_bz = new RadioButton();
            rad_sb = new RadioButton();
            txt_qspan = new TextBox();
            txt_atimeout = new TextBox();
            txt_topcard = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label10 = new Label();
            label4 = new Label();
            label3 = new Label();
            txt_rst = new RichTextBox();
            pictureBox1 = new PictureBox();
            linkLabel1 = new LinkLabel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_row).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_col).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txt_sendspan);
            groupBox1.Controls.Add(txt_losttime);
            groupBox1.Controls.Add(txt_timeout);
            groupBox1.Controls.Add(cmb_rate);
            groupBox1.Controls.Add(cmb_port);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(169, 175);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Port Setting";
            // 
            // txt_sendspan
            // 
            txt_sendspan.Location = new Point(67, 141);
            txt_sendspan.Name = "txt_sendspan";
            txt_sendspan.Size = new Size(96, 23);
            txt_sendspan.TabIndex = 2;
            txt_sendspan.Text = "20";
            // 
            // txt_losttime
            // 
            txt_losttime.Location = new Point(67, 112);
            txt_losttime.Name = "txt_losttime";
            txt_losttime.Size = new Size(96, 23);
            txt_losttime.TabIndex = 2;
            txt_losttime.Text = "1000";
            // 
            // txt_timeout
            // 
            txt_timeout.Location = new Point(67, 83);
            txt_timeout.Name = "txt_timeout";
            txt_timeout.Size = new Size(96, 23);
            txt_timeout.TabIndex = 2;
            txt_timeout.Text = "20";
            // 
            // cmb_rate
            // 
            cmb_rate.FormattingEnabled = true;
            cmb_rate.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "56000", "57600", "115200", "194000", "230400", "460800", "921600" });
            cmb_rate.Location = new Point(42, 54);
            cmb_rate.Name = "cmb_rate";
            cmb_rate.Size = new Size(121, 23);
            cmb_rate.TabIndex = 1;
            // 
            // cmb_port
            // 
            cmb_port.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_port.FormattingEnabled = true;
            cmb_port.Location = new Point(42, 25);
            cmb_port.Name = "cmb_port";
            cmb_port.Size = new Size(121, 23);
            cmb_port.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 57);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 0;
            label2.Text = "Rate:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 28);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 0;
            label1.Text = "Port:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 144);
            label7.Name = "label7";
            label7.Size = new Size(62, 15);
            label7.TabIndex = 0;
            label7.Text = "SendSpan:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 115);
            label6.Name = "label6";
            label6.Size = new Size(59, 15);
            label6.TabIndex = 0;
            label6.Text = "LostTime:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 86);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 0;
            label5.Text = "Timeout:";
            // 
            // btn_port
            // 
            btn_port.Location = new Point(12, 193);
            btn_port.Name = "btn_port";
            btn_port.Size = new Size(132, 31);
            btn_port.TabIndex = 1;
            btn_port.Text = "Open Port";
            btn_port.UseVisualStyleBackColor = true;
            btn_port.Click += btn_port_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGray;
            button2.Font = new Font("Segoe UI", 19.5F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(5, 233);
            button2.Name = "button2";
            button2.Size = new Size(157, 49);
            button2.TabIndex = 1;
            button2.Text = "Run";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(num_row);
            groupBox2.Controls.Add(num_col);
            groupBox2.Controls.Add(panel1);
            groupBox2.Controls.Add(txt_qspan);
            groupBox2.Controls.Add(txt_atimeout);
            groupBox2.Controls.Add(txt_topcard);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Enabled = false;
            groupBox2.Location = new Point(12, 230);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(169, 288);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Action";
            // 
            // num_row
            // 
            num_row.Location = new Point(116, 147);
            num_row.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            num_row.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_row.Name = "num_row";
            num_row.Size = new Size(45, 23);
            num_row.TabIndex = 6;
            num_row.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // num_col
            // 
            num_col.Location = new Point(34, 147);
            num_col.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            num_col.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_col.Name = "num_col";
            num_col.Size = new Size(45, 23);
            num_col.TabIndex = 6;
            num_col.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(linkLabel2);
            panel1.Controls.Add(chk_cheat);
            panel1.Controls.Add(rad_sc);
            panel1.Controls.Add(rad_pc);
            panel1.Controls.Add(rad_pb);
            panel1.Controls.Add(rad_bz);
            panel1.Controls.Add(rad_sb);
            panel1.Location = new Point(0, 13);
            panel1.Name = "panel1";
            panel1.Size = new Size(169, 98);
            panel1.TabIndex = 5;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(88, 77);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(78, 15);
            linkLabel2.TabIndex = 2;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Cheat Setting";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // chk_cheat
            // 
            chk_cheat.AutoSize = true;
            chk_cheat.ForeColor = Color.Fuchsia;
            chk_cheat.Location = new Point(6, 76);
            chk_cheat.Name = "chk_cheat";
            chk_cheat.Size = new Size(79, 19);
            chk_cheat.TabIndex = 1;
            chk_cheat.Text = "Use Cheat";
            chk_cheat.UseVisualStyleBackColor = true;
            // 
            // rad_sc
            // 
            rad_sc.AutoSize = true;
            rad_sc.Location = new Point(88, 28);
            rad_sc.Name = "rad_sc";
            rad_sc.Size = new Size(77, 19);
            rad_sc.TabIndex = 0;
            rad_sc.Text = "StoreCard";
            rad_sc.UseVisualStyleBackColor = true;
            // 
            // rad_pc
            // 
            rad_pc.AutoSize = true;
            rad_pc.Location = new Point(88, 53);
            rad_pc.Name = "rad_pc";
            rad_pc.Size = new Size(72, 19);
            rad_pc.TabIndex = 0;
            rad_pc.Text = "PickCard";
            rad_pc.UseVisualStyleBackColor = true;
            // 
            // rad_pb
            // 
            rad_pb.AutoSize = true;
            rad_pb.Location = new Point(5, 53);
            rad_pb.Name = "rad_pb";
            rad_pb.Size = new Size(74, 19);
            rad_pb.TabIndex = 0;
            rad_pb.Text = "PickBook";
            rad_pb.UseVisualStyleBackColor = true;
            // 
            // rad_bz
            // 
            rad_bz.AutoSize = true;
            rad_bz.Checked = true;
            rad_bz.Location = new Point(5, 3);
            rad_bz.Name = "rad_bz";
            rad_bz.Size = new Size(132, 19);
            rad_bz.TabIndex = 0;
            rad_bz.TabStop = true;
            rad_bz.Text = "All Motors BackZero";
            rad_bz.UseVisualStyleBackColor = true;
            // 
            // rad_sb
            // 
            rad_sb.AutoSize = true;
            rad_sb.Location = new Point(5, 28);
            rad_sb.Name = "rad_sb";
            rad_sb.Size = new Size(79, 19);
            rad_sb.TabIndex = 0;
            rad_sb.Text = "StoreBook";
            rad_sb.UseVisualStyleBackColor = true;
            // 
            // txt_qspan
            // 
            txt_qspan.Location = new Point(101, 204);
            txt_qspan.Name = "txt_qspan";
            txt_qspan.Size = new Size(61, 23);
            txt_qspan.TabIndex = 4;
            txt_qspan.Text = "20";
            // 
            // txt_atimeout
            // 
            txt_atimeout.Location = new Point(101, 175);
            txt_atimeout.Name = "txt_atimeout";
            txt_atimeout.Size = new Size(61, 23);
            txt_atimeout.TabIndex = 4;
            txt_atimeout.Text = "30000";
            // 
            // txt_topcard
            // 
            txt_topcard.Location = new Point(92, 117);
            txt_topcard.Name = "txt_topcard";
            txt_topcard.Size = new Size(70, 23);
            txt_topcard.TabIndex = 3;
            txt_topcard.Text = "160";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(5, 207);
            label9.Name = "label9";
            label9.Size = new Size(68, 15);
            label9.TabIndex = 0;
            label9.Text = "QuerySpan:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(4, 178);
            label8.Name = "label8";
            label8.Size = new Size(90, 15);
            label8.TabIndex = 0;
            label8.Text = "ActionTimeout:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(4, 120);
            label10.Name = "label10";
            label10.Size = new Size(82, 15);
            label10.TabIndex = 0;
            label10.Text = "TopCardNum:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 149);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 0;
            label4.Text = "Col:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(81, 149);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 0;
            label3.Text = "Row:";
            // 
            // txt_rst
            // 
            txt_rst.BackColor = Color.White;
            txt_rst.BorderStyle = BorderStyle.FixedSingle;
            txt_rst.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_rst.Location = new Point(187, 30);
            txt_rst.Name = "txt_rst";
            txt_rst.ReadOnly = true;
            txt_rst.ScrollBars = RichTextBoxScrollBars.Vertical;
            txt_rst.Size = new Size(569, 488);
            txt_rst.TabIndex = 3;
            txt_rst.Text = "";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Red;
            pictureBox1.Location = new Point(150, 193);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(31, 31);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(691, 12);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(65, 15);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "UploadLog";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(768, 530);
            Controls.Add(linkLabel1);
            Controls.Add(pictureBox1);
            Controls.Add(txt_rst);
            Controls.Add(groupBox2);
            Controls.Add(btn_port);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Parallel PCTester";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_row).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_col).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox cmb_rate;
        private ComboBox cmb_port;
        private Label label2;
        private Label label1;
        private Button btn_port;
        private Button button2;
        private GroupBox groupBox2;
        private Label label4;
        private Label label3;
        private RichTextBox txt_rst;
        private TextBox txt_sendspan;
        private TextBox txt_losttime;
        private TextBox txt_timeout;
        private Label label6;
        private Label label5;
        private Label label7;
        private TextBox txt_atimeout;
        private Label label8;
        private TextBox txt_qspan;
        private Label label9;
        private Panel panel1;
        private RadioButton rad_sc;
        private RadioButton rad_pc;
        private RadioButton rad_pb;
        private RadioButton rad_bz;
        private RadioButton rad_sb;
        private TextBox txt_topcard;
        private Label label10;
        private PictureBox pictureBox1;
        private NumericUpDown num_row;
        private NumericUpDown num_col;
        private LinkLabel linkLabel1;
        private CheckBox chk_cheat;
        private LinkLabel linkLabel2;
    }
}
