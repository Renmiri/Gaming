using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InjusticeReader
{
    public partial class Form1 : Form
    {
        public byte[] buffer;
        public string path, filename;

        public Form1()
        {
            InitializeComponent();
        }

        public struct BlockInfo
        {
            public uint csize;
            public uint ucsize;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.xxx;*.upk|*.xxx;*.upk";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rtb1.Text = "";
                FileStream fs = new FileStream(d.FileName, FileMode.Open, FileAccess.Read);
                path = Path.GetDirectoryName(d.FileName) + "\\";
                filename = d.FileName;
                int length = (int)fs.Length;
                buffer = new byte[length];         
                int count;                          
                int sum = 0;       
                while ((count = fs.Read(buffer, sum, length - sum)) > 0)
                    sum += count;
                fs.Close();
                println("File Loaded...");
                int flags = GetInt(0x1D);
                if ((flags & 0x02000000) != 0)
                {
                    ReadHeader();
                    println("File Written...");
                }
                else 
                    Package.memory = buffer;
                println("Opening Browser...");
                Browser bw = new Browser();
                bw.Show();
            }
        }

        public FileStream fileout;
        public MemoryStream temp;

        public void ReadHeader()
        {
            temp = new MemoryStream();
            fileout = new FileStream(filename.Substring(0, filename.Length - 4) + ".upk", FileMode.Create, FileAccess.Write);
            println("Magic : " + GetInt(0).ToString("X4"));
            println("Compression Flag : " + GetInt(0x6d).ToString());
            println("Count : " + GetInt(0x71).ToString());
            int count = GetInt(0x71);
            CreateHeader();
            for (int i = 0; i < count; i++)
            {
                uint offset = GetUInt((uint)(0x75 + i * 16 + 8));
                uint size = GetUInt((uint)(0x75 + i * 16 + 12));
                println(i + " - Offset : 0x" + offset.ToString("X4") + " Size : 0x" + size.ToString("X4"));                
                DumpBlocks(offset, i);                
            }
            fileout.Write(temp.ToArray(), 0, (int)temp.Length);
            fileout.Close();
            Package.memory = temp.ToArray();
        }

        public void CreateHeader()
        {
            MemoryStream m = new MemoryStream();
            int headersize = GetInt(0x25);
            int filler = headersize - 0x6D;
            int flags = GetInt(0x1D);
            flags = (flags % 0x02000000);
            for (int i = 0; i < 0x1D; i++)
                m.WriteByte(buffer[i]);
            byte[] buff = BitConverter.GetBytes(flags);
            m.WriteByte(buff[3]);
            m.WriteByte(buff[2]);
            m.WriteByte(buff[1]);
            m.WriteByte(buff[0]);
            for (int i = 0x21; i < 0x6D; i++)
                m.WriteByte(buffer[i]);
            for (int i = 0; i < filler; i++)
                m.WriteByte(0);
            //fileout.Write(m.ToArray(), 0, (int)m.Length);
            temp.Write(m.ToArray(), 0, (int)m.Length);
        }

        public void DumpBlocks(uint offset, int chunk)
        {
            uint magic = GetUInt(offset);
            uint bsize = GetUInt(offset + 4);
            uint compressed = GetUInt(offset + 8);
            uint uncompressed = GetUInt(offset + 12);
            uint blockc = (uncompressed % bsize == 0) ? uncompressed / bsize : uncompressed / bsize + 1;
            List<BlockInfo> blocks = new List<BlockInfo>();
            for (int i = 0; i < blockc; i++)
            {
                BlockInfo b = new BlockInfo();
                b.csize = GetUInt((uint)(offset + 16 + i * 8));
                b.ucsize = GetUInt((uint)(offset + 20 + i * 8));
                //println("\t" + i + " Compressed: 0x" + b.csize.ToString("X4") + " Uncompressed: 0x" + b.ucsize.ToString("X4"));
                blocks.Add(b);
            }
            uint off2 = offset + 16 + blockc * 8;
            string loc = Path.GetDirectoryName(Application.ExecutablePath);
            MemoryStream m = new MemoryStream();
            for (int i = 0; i < blockc; i++)
            {
                if (File.Exists(loc + "\\exec\\script.bms"))
                    File.Delete(loc + "\\exec\\script.bms");
                if (File.Exists(loc + "\\exec\\temp.bin"))
                    File.Delete(loc + "\\exec\\temp.bin");
                FileStream fs = new FileStream(loc + "\\exec\\temp.bin", FileMode.Create, FileAccess.Write);
                fs.Write(buffer, (int)off2, (int)blocks[i].csize);
                fs.Close();
                string[] lines = { "ComType lzo1x", "Clog \"out.bin\" 0  0x" + blocks[i].csize.ToString("X4") + "  0x" + blocks[i].ucsize.ToString("X4") };
                System.IO.File.WriteAllLines(loc + "\\exec\\script.bms", lines);
                RunShell(loc + "\\exec\\quickbms.exe", "-o script.bms temp.bin");
                if (File.Exists(loc + "\\exec\\out.bin"))
                {
                    fs = new FileStream(loc + "\\exec\\out.bin", FileMode.Open, FileAccess.Read);
                    byte[] buffout = new byte[(int)fs.Length];
                    int count;
                    int sum = 0;
                    while ((count = fs.Read(buffout, sum, (int)fs.Length - sum)) > 0)
                        sum += count;
                    fs.Close();
                    m.Write(buffout, 0, (int)buffout.Length);
                }
                off2 += blocks[i].csize;
            }
            temp.Write(m.ToArray(), 0, (int)m.Length);
        }

        private void RunShell(string cmd, string args)
        {
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(cmd, args);
            procStartInfo.WorkingDirectory = Path.GetDirectoryName(cmd) + "\\";
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();
        }

        public int GetInt(uint offset)
        {
            if (!(offset < buffer.Length - 4))
                return -1;
            uint i = (uint)buffer[offset] * 256 * 256 * 256 +
                     (uint)buffer[offset + 1] * 256 * 256 +
                     (uint)buffer[offset + 2] * 256 +
                     (uint)buffer[offset + 3];
            return (int)i;
        }

        public uint GetUInt(uint offset)
        {
            if (!(offset < buffer.Length - 4))
                return 0;
            uint i = (uint)buffer[offset] * 256 * 256 * 256 +
                     (uint)buffer[offset + 1] * 256 * 256 +
                     (uint)buffer[offset + 2] * 256 +
                     (uint)buffer[offset + 3];
            return i;
        }

        public void println(string s)
        {
            rtb1.Text += s + "\n";
            rtb1.SelectionStart = rtb1.Text.Length;
            rtb1.ScrollToCaret();
        }
    }
}
