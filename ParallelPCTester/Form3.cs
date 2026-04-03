using ParallelPCTester.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelPCTester;
public partial class Form3 : Form
{
    public Form3()
    {
        InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        this.Close();
    }



    private void button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        txt_para.Text = txt_para.Text.Trim();
        foreach (var line in txt_para.Lines)
        {
            DelayAction.CheatTimes[i++] = int.Parse(line.Trim());
        }
        this.Close();
    }

    private void Form3_Load(object sender, EventArgs e)
    {
        txt_para.Text = "";
        for(int i=0;i<DelayAction.CheatTimes.Length; i++)
        {
            txt_para.AppendText(DelayAction.CheatTimes[i].ToString());
            txt_para.AppendText(Environment.NewLine);
        }
    }
}
