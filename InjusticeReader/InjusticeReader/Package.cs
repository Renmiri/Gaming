using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InjusticeReader
{
    public static class Package
    {
        public static byte[] memory;
        public struct Header
        {
            public int NameCount;
            public int NameOffset;
            public int ImportCount;
            public int ImportOffset;
            public int ExportCount;
            public int ExportOffset;
        }
        public struct NameEntry
        {
            public string Name;
            public int Unk1;
            public int Unk2;
            public int Offset;
        }
        public struct ExportEntry
        {
            public int Class;
            public int Link;
            public int Name;
            public int DataSize;
            public int DataOffset;
            public int Offset;
        }
        public struct ImportEntry
        {
            public int Package;
            public int Link;
            public int Name;
            public int Offset;
        }

        public static Header header;
        public static List<NameEntry> namelist;
        public static List<ExportEntry> exportlist;
        public static List<ImportEntry> importlist;

        public static void Init()
        {
            header.NameCount = GetInt(0x21);
            header.NameOffset = GetInt(0x25);
            header.ExportCount = GetInt(0x29);
            header.ExportOffset = GetInt(0x2D);
            header.ImportCount = GetInt(0x31);
            header.ImportOffset = GetInt(0x35);
            ReadNames();
            ReadExports();
            ReadImports();
        }

        public static void ReadNames()
        {
            int pos = header.NameOffset;
            namelist = new List<NameEntry>();
            for (int i = 0; i < header.NameCount; i++)
            {
                NameEntry e = new NameEntry();
                e.Offset = pos;
                int len = GetInt(pos) - 1;
                pos += 4;
                e.Name = "";
                for (int j = 0; j < len; j++)
                    e.Name += (char)memory[pos + j];
                pos += len + 1;
                e.Unk1 = GetInt(pos);
                e.Unk2 = GetInt(pos + 4);
                pos += 8;
                namelist.Add(e);
            }
        }

        public static void ReadExports()
        {
            int pos = header.ExportOffset;
            exportlist = new List<ExportEntry>();
            int unkc;
            for (int i = 0; i < header.ExportCount; i++)
            {
                ExportEntry e = new ExportEntry();
                e.Class = GetInt(pos);
                e.Link = GetInt(pos + 8);
                e.Name = GetInt(pos + 12);
                e.DataSize = GetInt(pos + 36);
                e.DataOffset = GetInt(pos + 40);
                e.Offset = pos;
                unkc = GetInt(pos + 44);
                pos += 0x44 + unkc * 12;
                exportlist.Add(e);
            }
        }

        public static void ReadImports()
        {
            int pos = header.ImportOffset;
            importlist = new List<ImportEntry>();
            for (int i = 0; i < header.ImportCount; i++)
            {
                ImportEntry e = new ImportEntry();
                e.Package = GetInt(pos);
                e.Link = GetInt(pos + 16);
                e.Name = GetInt(pos + 20);
                pos += 0x1C;
                importlist.Add(e);
            }
        }

        public static string GetName(int n)
        {
            if (n >= 0 && n < namelist.Count)
                return namelist[n].Name;
            else
                return "";
        }

        public static string GetFullName(int n)
        {
            if (n >= 0 && n < exportlist.Count)
            {
                ExportEntry e = exportlist[n];
                if(e.Link != 0)
                    return FollowLink(e.Link) + GetName(e.Name);
                else 
                    return GetName(e.Name);
            }
            else
                return "";
        }

        public static string GetClass(int Class)
        {
            if (Class == 0)
                return "Class";
            if (Class > 0)
            {
                return Package.GetName(exportlist[Class - 1].Name);
            }
            else
            {
                return Package.GetName(importlist[Class * -1 - 1].Name);
            }
        }

        public static string FollowLink(int Link)
        {
            string res = "";
            int tLink = Link - 1;
            while (tLink >= 0)
            {
                string name = GetName(exportlist[tLink].Name);
                res = name + "." + res;
                tLink = exportlist[tLink].Link - 1;
            }
            return res;
        }

        public static int GetInt(int offset)
        {
            if (!(offset < memory.Length - 4))
                return -1;
            uint i = (uint)memory[offset] * 256 * 256 * 256 +
                     (uint)memory[offset + 1] * 256 * 256 +
                     (uint)memory[offset + 2] * 256 +
                     (uint)memory[offset + 3];
            return (int)i;
        }
    }
}
