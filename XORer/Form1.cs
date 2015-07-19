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
using Be.Windows.Forms;

namespace XORer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hb1.ByteProvider = new DynamicByteProvider(new byte[1]); 
            hb2.ByteProvider = new DynamicByteProvider(new byte[1]);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.*|*.*";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                hb1.ByteProvider = new DynamicByteProvider(File.ReadAllBytes(d.FileName));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.*|*.*";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                hb2.ByteProvider = new DynamicByteProvider(File.ReadAllBytes(d.FileName));
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.*|*.*";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MemoryStream m = new MemoryStream();
                for (int i = 0; i < hb3.ByteProvider.Length; i++)
                    m.WriteByte(hb3.ByteProvider.ReadByte(i));
                File.WriteAllBytes(d.FileName, m.ToArray());
                MessageBox.Show("Done.");
            }
        }

        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryStream m = new MemoryStream();
            for (int i = 0; i < hb1.ByteProvider.Length; i++)
                m.WriteByte((byte)(hb1.ByteProvider.ReadByte(i) ^ hb2.ByteProvider.ReadByte(i % hb2.ByteProvider.Length)));
            hb3.ByteProvider = new DynamicByteProvider(m.ToArray());
        }
    }
}
