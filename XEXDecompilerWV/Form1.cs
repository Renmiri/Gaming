using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XEXDecompilerWV
{
    public partial class Form1 : Form
    {
        public List<string> AsmCodeLines = new List<string>();
        public List<XEXFunction> Functions = new List<XEXFunction>();

        public Form1()
        {
            InitializeComponent();
        }

        private void openASMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.asm|*.asm";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                status.Text = "Counting Assembler Code Lines...";
                Application.DoEvents();
                int lineCount = File.ReadLines(d.FileName).Count();
                status.Text = "Loading Assembler Code Lines...";
                Application.DoEvents();
                pb1.Maximum = lineCount;
                pb1.Value = 0;
                int counter = 0;
                StreamReader file = new StreamReader(d.FileName);
                AsmCodeLines = new List<string>();
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    AsmCodeLines.Add(line);
                    if ((counter++) % 1000 == 0)
                    {
                        Application.DoEvents();
                        pb1.Value = counter;
                    }                    
                }
                pb1.Value = 0;
                status.Text = "Finding Functions...";
                Application.DoEvents();
                Functions = new List<XEXFunction>();
                counter = 0;
                XEXFunction func = new XEXFunction();
                while (counter < AsmCodeLines.Count)
                {
                    line = AsmCodeLines[counter];
                    if (line.StartsWith("# =============== S U B\tR O U T\tI N E ======================================="))
                    {
                        AsmCodeLines[counter] = AsmCodeLines[counter].Replace("\t", " ");
                        func.AsmCodeStart = counter;
                    }
                    else if (line.StartsWith("# End of function"))
                    {
                        func.AsmCodeEnd = counter;
                        func.Name = line.Substring(18, line.Length - 18).Trim();
                        Functions.Add(func);                        
                        func = new XEXFunction();
                    }    
                    if (counter++ % 1000 == 0)
                    {
                        status.Text = "Functions found : " + Functions.Count();                        
                        pb1.Value = counter;
                        Application.DoEvents();
                    }
                }
                pb1.Value = 0;
                listBox1.Items.Clear();
                counter = 0;
                foreach (XEXFunction f in Functions)
                    listBox1.Items.Add((counter++).ToString("d8") + " : " + f.Name + " (" + (f.AsmCodeEnd - f.AsmCodeStart + 1) + " lines)");
                status.Text = "Ready";
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1)
                return;
            XEXFunction f = Functions[n];
            rtb1.Text = "";
            StringBuilder sb = new StringBuilder();
            for (int i = f.AsmCodeStart; i <= f.AsmCodeEnd; i++)
                sb.AppendLine(AsmCodeLines[i]);
            rtb1.Text = sb.ToString();
            if (midStepToolStripMenuItem.Checked)
            {
                treeView1.Nodes.Clear();
                List<string> code = new List<string>();
                for (int i = f.AsmCodeStart; i <= f.AsmCodeEnd; i++)
                    code.Add(AsmCodeLines[i]);
                treeView1.Nodes.Add(Decompiler.MidStep(code));
            }
            if (step1ToolStripMenuItem.Checked)
            {
                string s = "";
                f = Decompiler.Step1(AsmCodeLines, f, out s);
                rtb2.Text = s;
                if (step2ToolStripMenuItem.Checked)
                {
                    f = Decompiler.Step2(AsmCodeLines, f, ref s);
                    rtb3.Text = s;
                }
            }
        }

        private void step1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1)
                return;
            XEXFunction f = Functions[n];
            string s = "";
            f = Decompiler.Step1(AsmCodeLines, f, out s);
            Functions[n] = f;
            rtb2.Text = s;
        }

        private void step2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1)
                return;
            XEXFunction f = Functions[n];
            string s = rtb2.Text;
            f = Decompiler.Step2(AsmCodeLines, f, ref s);
            Functions[n] = f;
            rtb3.Text = s;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex + 1;
            string s = toolStripTextBox1.Text.Trim().ToLower();
            if (s == "")
                return;
            for(int i=n;i< Functions.Count;i++)
                if (Functions[i].Name.ToLower().Contains(s))
                {
                    listBox1.SelectedIndex = i;
                    return;
                }
        }

        private void exportSingleStep1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1)
                return;
            XEXFunction f = Functions[n];
            string s = "";
            f = Decompiler.Step1(AsmCodeLines, f, out s);
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.c|*.c";
            d.FileName = f.Name + ".c";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(d.FileName, s);
                MessageBox.Show("Done");
            }
        }

        private void exportSingleStep2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1)
                return;
            XEXFunction f = Functions[n];
            string s = "";
            f = Decompiler.Step1(AsmCodeLines, f, out s);
            f = Decompiler.Step2(AsmCodeLines, f, ref s);
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.c|*.c";
            d.FileName = f.Name + ".c";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(d.FileName, s);
                MessageBox.Show("Done");
            }
        }

        private void exportAllStep1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = d.SelectedPath + "\\";
                pb1.Maximum = Functions.Count();
                pb1.Value = 0;       
                for (int i = 0; i < Functions.Count; i++)
                {
                    XEXFunction f = Functions[i];
                    string s = "";
                    f = Decompiler.Step1(AsmCodeLines, f, out s);
                    File.WriteAllText(path + f.Name + ".c", s);
                    if (i % 77 == 0)
                    {
                        status.Text = "Decompiling(" + (i + 1) + "/" + Functions.Count + ")...";
                        pb1.Value = i;
                        Application.DoEvents();
                    }
                }
                pb1.Value = 0;
                status.Text = "Ready";
                Application.DoEvents();
                MessageBox.Show("Done");
            }
        }

        private void exportAllStep2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = d.SelectedPath + "\\";
                pb1.Maximum = Functions.Count();
                pb1.Value = 0;          
                for (int i = 0; i < Functions.Count; i++)
                {
                    XEXFunction f = Functions[i];
                    string s = "";
                    f = Decompiler.Step1(AsmCodeLines, f, out s);
                    f = Decompiler.Step2(AsmCodeLines, f, ref s);
                    File.WriteAllText(path + f.Name.Split('\t')[0] + ".c", s);
                    if (i % 77 == 0)
                    {
                        pb1.Value = i;
                        status.Text = "Decompiling(" + (i + 1) + "/" + Functions.Count + ")...";
                        Application.DoEvents();
                    }
                }
                pb1.Value = 0;
                status.Text = "Ready";
                Application.DoEvents();
                MessageBox.Show("Done");
            }
        }

        private void midStepToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
