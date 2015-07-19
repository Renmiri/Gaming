using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace InjusticeReader
{
    public partial class Browser : Form
    {
        public int ViewMode; // 0 = names, 1 =imports, 2 = exports

        public Browser()
        {
            InitializeComponent();
        }

        private void Browser_Load(object sender, EventArgs e)
        {
            Package.Init();
            ViewMode = 0;
            RefreshLists();
        }

        public void RefreshLists()
        {
            listBox1.Items.Clear();
            int count = 0;
            if (ViewMode == 0)
                foreach (Package.NameEntry n in Package.namelist)
                    listBox1.Items.Add((count++)
                                        + " : @0x"
                                        + n.Offset.ToString("X8")
                                        + " Unk1 0x"
                                        + n.Unk1.ToString("X8")
                                        + " Unk2 0x"
                                        + n.Unk2.ToString("X8")
                                        + " String \""
                                        + n.Name
                                        + "\"");
            if (ViewMode == 1)
                foreach (Package.ImportEntry e in Package.importlist)
                    listBox1.Items.Add((count++)
                                        + " : @0x"
                                        + e.Offset.ToString("X8")
                                        + " Package \""
                                        + Package.GetName(e.Package )
                                        + "\" Link \""
                                        + Package.FollowLink(e.Link)
                                        + "\" Name \""
                                        + Package.GetName(e.Name)
                                        + "\"");
            if (ViewMode == 2)
                foreach (Package.ExportEntry e in Package.exportlist)
                    listBox1.Items.Add((count++)
                                        + " : @0x"
                                        + e.Offset.ToString("X8")
                                        + " Datasize 0x"
                                        + e.DataSize.ToString("X8")
                                        + " Dataoffset 0x"
                                        + e.DataOffset.ToString("X8")
                                        + " Name \""
                                        + Package.GetFullName(count - 1)
                                        + "\"");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ViewMode = 0;
            RefreshButtons();
            RefreshLists();
        }

        public void RefreshButtons()
        {
            toolStripButton1.Checked = (ViewMode == 0);
            toolStripButton2.Checked = (ViewMode == 1);
            toolStripButton3.Checked = (ViewMode == 2);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ViewMode = 2;
            RefreshButtons();
            RefreshLists();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ViewMode = 1;
            RefreshButtons();
            RefreshLists();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preview();
        }

        public void Preview()
        {
            int n = listBox1.SelectedIndex;
            if (n == -1)
                return;
            if (ViewMode == 2)
            {
                Package.ExportEntry e = Package.exportlist[n];
                byte[] buff = new byte[e.DataSize];
                for (int i = 0; i < e.DataSize; i++)
                    buff[i] = Package.memory[e.DataOffset + i];
                DynamicByteProvider db = new DynamicByteProvider(buff);
                hb1.ByteProvider = db;
                status.Text = "Class :" + Package.GetClass(e.Class);
            }
        }
    }
}
