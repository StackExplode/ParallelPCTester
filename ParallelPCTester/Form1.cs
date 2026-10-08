using ParallelPCTester.App;
using ParallelPCTester.BLL;
using ParallelPCTester.Driver;
using ParallelPCTester.Entity;
using ParallelPCTester.Helper;
using System.Diagnostics;

namespace ParallelPCTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox2.EnabledChanged += GroupBox2_EnabledChanged;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
        }

        private void GroupBox2_EnabledChanged(object? sender, EventArgs e)
        {
            if (groupBox2.Enabled)
            {
                button2.BackColor = Color.Green;
            }
            else
            {
                button2.BackColor = Color.DarkGray;
            }
        }

        Form2 debug_form = null;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                if (debug_form is null)
                {
                    ParseEnv();
                    debug_form = new Form2(env);
                    debug_form.FormClosed += (_, _) => debug_form = null;
                    debug_form.Show();
                }
                else
                {
                    debug_form.Focus();
                }
                return true; // 表示已处理
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text += $" v{version.Major}.{version.Minor}.{version.Build}";
            Logger.SetParent(this, txt_rst);
            var ports = JSerialPort.EnumPorts();
            cmb_port.Items.AddRange(ports);
            if (ports.Length > 0)
                cmb_port.SelectedIndex = 0;
            cmb_rate.SelectedIndex = 9;
            rad_bz.Tag = MissionType.IsBackZero;
            rad_sc.Tag = MissionType.IsCard;
            rad_pb.Tag = MissionType.IsPick;
            rad_pc.Tag = MissionType.IsCard | MissionType.IsPick;
            rad_batch.Tag = MissionType.IsBatch;
        }

        private MissionType GetMissionType()
        {
            MissionType t = 0;
            foreach (var c in panel1.Controls)
            {
                if (c is RadioButton r && r.Checked)
                {
                    t |= (MissionType)(r.Tag ?? 0);
                }
            }
            if(chk_cheat.Checked)
                t |= MissionType.IsCheat;
            return t;
        }

        private string GetMissionName(MissionType mtype, int row, int col)
        {
            if (mtype.HasFlag(MissionType.IsBackZero))
                return "全部回零";

            string verb = mtype.HasFlag(MissionType.IsPick) ? "取出" : "存入";
            string prep = mtype.HasFlag(MissionType.IsPick) ? "从" : "到";
            string obj = mtype.HasFlag(MissionType.IsCard) ? "卡" : "证件";
            string sp = mtype.HasFlag(MissionType.IsTop) ? "(最顶部)" : "";

            return $"{verb}{obj},{prep}第{col}列第{row}行{sp}";
        }

        JSerialPort port;

        private void btn_port_Click(object sender, EventArgs e)
        {
            if (!port?.IsOpen ?? true)
            {
                if (port is not null)
                    port.Close();
                port = new JSerialPort();
                port.PortName = cmb_port.Text;
                port.BaudRate = int.Parse(cmb_rate.Text);
                port.Timeout = int.Parse(txt_timeout.Text);
                port.LostTimeout = int.Parse(txt_losttime.Text);
                port.Open();
                btn_port.Text = "Close Port";
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                pictureBox1.BackColor = Color.Green;
            }
            else
            {
                port.Close();
                btn_port.Text = "Open Port";
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                pictureBox1.BackColor = Color.Red;
            }


        }

        private void ParseEnv()
        {
            env.Port = port;
            env.SendSpan = int.Parse(txt_sendspan.Text);
            env.QuerySpan = int.Parse(txt_qspan.Text);
            env.MaxActionTime = int.Parse(txt_atimeout.Text);
        }

        ActionEnvironment env = new();
        private async void button2_Click(object sender, EventArgs e)
        {
            if (port is null || !port.IsOpen)
            {
                MessageBox.Show("请先打开串口！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            groupBox2.Enabled = false;

            MissionType mtype = GetMissionType();
            int row = ((int)num_row.Value);
            int col = ((int)num_col.Value);
            int maxcard = int.Parse(txt_topcard.Text);
            if (row >= maxcard)
                mtype |= MissionType.IsTop;
            int sendspan = int.Parse(txt_sendspan.Text);
            int queryspan = int.Parse(txt_qspan.Text);
            int actiontimeout = int.Parse(txt_atimeout.Text);

            var builder = ActionBuilderFactory.CreateActionBuilder(mtype, row, col);
            ParseEnv();

            var dispatcher = new ActionDispatcher(builder.Root, env);
            string mname = GetMissionName(mtype, row, col);

            Program.gtimer.Reset();
            Program.gtimer.Start();
            Logger.Clear();
            Logger.AppendLine($"开始执行【{mname}】！", "Orange");
            Logger.AppendLine();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var rt = await dispatcher.Run();
            sw.Stop();
            Logger.AppendLine();
            if (!rt)
                Logger.AppendLine($"【{mname}】因错误而中断！", "red");
            else
                Logger.AppendLine($"【{mname}】全部执行完毕！", "Orange");
            Logger.PrintRunTime(sw.ElapsedMilliseconds, "任务总执行时间：");

            groupBox2.Enabled = true;
        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var drst = MessageBox.Show("你确认上传日志吗？请不要滥用接口，避免无用日志太多！", "确认上传", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drst != DialogResult.Yes)
                return;
            linkLabel1.Enabled = false;
            var rst = await UploadLogHelper.UploadRichTextBoxAsync(
                "http://jp.jloli.cc/pctester/uploader.php",
                "114514",
                txt_rst);
            MessageBox.Show(rst);
            linkLabel1.Enabled = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fm = new Form3();
            fm.ShowDialog();
        }
    }
}
