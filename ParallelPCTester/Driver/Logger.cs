#define PRINT_LOG

using ParallelPCTester.Entity;
using ParallelPCTester.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Driver;



internal static class Logger
{
    private static RichTextBox richTextBox;
    private static Form mainForm;


    public static void SetParent(Form mainForm, RichTextBox richTextBox)
    {
        Logger.mainForm = mainForm;
        Logger.richTextBox = richTextBox;
    }

    private static long last_time = 0;
    [Conditional("PRINT_LOG")]
    [Conditional("PRINT_TIME")]
    private static void PrintTimeStamp(bool always = false)
    {
        //Check if current cursor is at the beginning of a line
        if (always || richTextBox.SelectionStart == 0 || richTextBox.Text[richTextBox.TextLength - 1] == '\n')
        {
            long ms = Program.gtimer.ElapsedMilliseconds;
            InsertColoredText($"[{ms/1000}.{ms%1000}({ms-last_time})]", Color.DarkGray);
            last_time = ms;
        }
    }

    [Conditional("PRINT_LOG")]
    public static void InsertColoredText(string text, Color color, bool isBold = false)
    {
        // 保存当前的选择开始位置和颜色
        int selectionStart = richTextBox.SelectionStart;
        Color previousColor = richTextBox.SelectionColor;
        Font previousFont = richTextBox.SelectionFont;

        // 插入带有颜色的文本
        richTextBox.SelectionStart = richTextBox.TextLength;
        richTextBox.SelectionColor = color;
        richTextBox.SelectionFont = new Font(previousFont, isBold ? FontStyle.Bold : FontStyle.Regular);
        richTextBox.AppendText(text);

        // 恢复之前的颜色
        richTextBox.SelectionStart = selectionStart;
        richTextBox.SelectionColor = previousColor;
        richTextBox.SelectionFont = previousFont;
    }

    [Conditional("PRINT_LOG")]
    public static void Append(string msg, string color = "#000", bool isBold = false)
    {
        Color c = ColorTranslator.FromHtml(color);
        mainForm.BeginInvoke((MethodInvoker)(() =>
        {
            PrintTimeStamp();
            InsertColoredText(msg, c, isBold);
            // set the current caret position to the end
            richTextBox.SelectionStart = richTextBox.Text.Length;
            // scroll it automatically
            richTextBox.ScrollToCaret();
        }));
    }

    [Conditional("PRINT_LOG")]
    public static void AppendLine(string msg = "", string color = "#000", bool isBold = false)
    {
        Color c = ColorTranslator.FromHtml(color);
        mainForm.BeginInvoke((MethodInvoker)(() =>
        {
            PrintTimeStamp();
            InsertColoredText(msg + Environment.NewLine, c, isBold);
            // set the current caret position to the end
            //ResultTextBox.SelectionStart = ResultTextBox.Text.Length;
            // scroll it automatically
            richTextBox.ScrollToCaret();
        }));
    }

    [Conditional("PRINT_LOG")]
    public static void Backspace(int n)
    {
        mainForm.BeginInvoke((MethodInvoker)(() =>
        {
            if (richTextBox.Text.Length >= n)
            {
                richTextBox.ReadOnly = false;
                // 使用 Select 方法选择末尾字符并删除
                richTextBox.SelectionStart = richTextBox.Text.Length - n;
                richTextBox.SelectionLength = n;
                richTextBox.SelectedText = "";
                richTextBox.ReadOnly = true;

            }
            else
            {
                richTextBox.Text = string.Empty;
            }

        }));

    }

    public static void Clear()
    {
        mainForm.BeginInvoke((MethodInvoker)(() =>
        {
            richTextBox.Clear();
        }));
    }

    [Conditional("PRINT_LOG")]
    public static void PrintResult(ErrorType errcode, string succ, string fail= "执行任务出错！")
    {
        if (errcode == 0)
        {
            AppendLine(succ, "green");
        }
        else
        {
            string errstr = errcode.GetDescription() ?? "未定义错误";
            AppendLine($"{fail}{errstr}！({errcode.ToInt()})", "red");
        }
    }

    private static readonly string[] spinchar = ["-", "/", "|", "\\"];
    private static int spinidx = 0;
    [Conditional("PRINT_LOG")]
    public static void SpinUpdate(bool first = false)
    {
        if (first)
            spinidx = 0;

        Backspace(1);
        Append(spinchar[spinidx]);
        spinidx = (spinidx + 1) % spinchar.Length;
    }

    [Conditional("PRINT_LOG")]
    public static void PrintRunTime(long ms,string caption= "任务共花费：")
    {
        AppendLine($"{caption}{ms}毫秒", "pink");
    }
}
