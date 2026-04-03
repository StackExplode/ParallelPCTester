namespace ParallelPCTester;

partial class Form3
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
        textBox1 = new TextBox();
        button1 = new Button();
        button2 = new Button();
        txt_para = new TextBox();
        label1 = new Label();
        SuspendLayout();
        // 
        // textBox1
        // 
        textBox1.Location = new Point(12, 37);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.ReadOnly = true;
        textBox1.Size = new Size(226, 312);
        textBox1.TabIndex = 0;
        textBox1.Text = "存证时升降对准修正预估时间:\r\n存证时无条件下沉预估时间:";
        textBox1.TextAlign = HorizontalAlignment.Right;
        // 
        // button1
        // 
        button1.Location = new Point(12, 355);
        button1.Name = "button1";
        button1.Size = new Size(126, 40);
        button1.TabIndex = 1;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Location = new Point(204, 355);
        button2.Name = "button2";
        button2.Size = new Size(126, 40);
        button2.TabIndex = 1;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // txt_para
        // 
        txt_para.Location = new Point(238, 37);
        txt_para.Multiline = true;
        txt_para.Name = "txt_para";
        txt_para.Size = new Size(93, 312);
        txt_para.TabIndex = 0;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(254, 15);
        label1.TabIndex = 2;
        label1.Text = "延时作弊专用参数，一行一个，单位为毫秒";
        // 
        // Form3
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(342, 400);
        Controls.Add(label1);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(txt_para);
        Controls.Add(textBox1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Form3";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Cheat Setting";
        Load += Form3_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox textBox1;
    private Button button1;
    private Button button2;
    private TextBox txt_para;
    private Label label1;
}