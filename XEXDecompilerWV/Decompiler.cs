using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XEXDecompilerWV
{
    public static class Decompiler
    {
        private static string _asmend = "\t}\r\n";
        private static string _asmstart = "\t_asm\r\n\t{\r\n";
        private static string _asmstartend = "\t_asm\r\n\t{\r\n\t}\r\n";
        private static string[] mnemonics_branch = {"b", "ba", "bl", "bla", "bc", "bca", "bcl", "bcla", "bclr", "bclrl", "bcctr", "bcctrl", "blt", "blta", "bltlr", "bltctr", "bltl", "bltla", "bltlrl", "bltctrl", "ble", "blea", "blelr", "blectr", "blel", "blela", "blelrl", "blectrl", "beq", "beqa", "beqlr", "beqctr", "beql", "beqla", "beqlrl", "beqctrl", "bge", "bgea", "bgelr", "bgectr", "bgel", "bgela", "bgelrl", "bgectrl", "bgt", "bgta", "bgtlr", "bgtctr", "bgtl", "bgtla", "bgtlrl", "bgtctrl", "bnl", "bnla", "bnllr", "bnlctr", "bnll", "bnlla", "bnllrl", "bnlctrl", "bne", "bnea", "bnelr", "bnectr", "bnel", "bnela", "bnelrl", "bnectrl", "bng", "bnga", "bnglr", "bngctr", "bngl", "bngla", "bnglrl", "bngctrl", "bso", "bsoa", "bsolr", "bsoctr", "bsol", "bsola", "bsolrl", "bsoctrl", "bns", "bnsa", "bnslr", "bnsctr", "bnsl", "bnsla", "bnslrl", "bnsctrl", "bun", "buna", "bunlr", "bunctr", "bunl", "bunla", "bunlrl", "bunctrl", "bnu", "bnua", "bnulr", "bnuctr", "bnul", "bnula", "bnulrl", "bnuctrl" };


        public static XEXFunction Step1(List<string> AsmCode, XEXFunction f, out string code)
        {
            StringBuilder sb = new StringBuilder();
            List<string> lines = new List<string>();
            for (int i = f.AsmCodeStart; i <= f.AsmCodeEnd; i++)
                lines.Add(AsmCode[i]);
            sb.AppendLine("void " + f.Name + "()");
            sb.AppendLine("{");
            sb.AppendLine("\t_asm");
            sb.AppendLine("\t{");
            //skip header
            int start = 0;
            for(int i=0;i<lines.Count;i++)
                if (lines[i].StartsWith(f.Name))
                {
                    start = i+1;
                    break;
                }
            while (lines[start].Trim().StartsWith("#") || lines[start].Trim() == "")
                start++;
            //insert asm
            for (int i = start; i < lines.Count - 1; i++)
                sb.Append(SimplePseudoC(lines[i]));
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            string result = sb.ToString();
            result = result.Replace(_asmstartend, "");
            code = result;
            return f;
        }

        public static string SimplePseudoC(string input)
        {
            StringBuilder sb = new StringBuilder();
            if (input.Trim().StartsWith("#") || input.Trim() == "")
                return "";
            else if (input.StartsWith("\t\t"))
            {
                string[] parts = input.Trim().Split('\t');
                if (parts.Length > 0)
                    switch (parts[0].ToLower())
                    {
                        #region mflr
                        case "mflr":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                sb.Append("\t" + parts[1].Trim() + " = this*;\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region lbz
                        case "lbz":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                string[] part3 = part2[1].Split('(');
                                part3[1] = part3[1].Replace(")", "");
                                sb.Append("\t" + part2[0].Trim() + " = (BYTE) [" + part3[1].Trim() + " + " + part3[0].Trim() + "];\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region sth,stw,stwu
                        case "sth":
                        case "stw":
                        case "stwu":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                if (parts[1].Contains("("))
                                {
                                    string[] part3 = part2[1].Split('(');
                                    part3[1] = part3[1].Replace(")", "");
                                    sb.Append("\t" + part2[0].Trim() + " = (WORD) [" + part3[1].Trim() + " + " + part3[0].Trim() + "];\r\n");
                                    sb.Append(_asmstart);
                                    return sb.ToString();
                                }
                            }
                            break;
                        #endregion
                        #region std, lhz
                        case "std":
                        case "lhz":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                string[] part3 = part2[1].Split('(');
                                part3[1] = part3[1].Replace(")", "");
                                sb.Append("\t" + part2[0].Trim() + " = (DWORD) [" + part3[1].Trim() + " + " + part3[0].Trim() + "];\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region mr
                        case "mr":
                        case "lis":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                sb.Append("\t" + part2[0].Trim() + " = (DWORD) " + part2[1].Trim() + ";\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region b,bl
                        case "b":
                        case "bl":
                            if (parts.Length == 2 && !parts[1].Trim().StartsWith("loc"))
                            {
                                sb.Append(_asmend);
                                sb.Append("\t" + parts[1].Trim() + "();\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region lzw
                        case "lwz":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                string[] part3 = part2[1].Trim().Split('(');
                                sb.Append("\t" + part2[0].Trim() + " = (DWORD) [" + part3[1].Replace(")", "").Trim() + " + " + part3[0].Trim() + "];\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region addi,add
                        case "addi":
                        case "addis":
                        case "add":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                sb.Append("\t" + part2[0].Trim() + " = (DWORD) (" + part2[1].Trim() + " + " + part2[2].Trim() + ");\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region subf
                        case "subf":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                sb.Append("\t" + part2[0].Trim() + " = (DWORD) (" + part2[2].Trim() + " - " + part2[1].Trim() + ");\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region cmplwi
                        case "cmplwi":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                if (part2.Length == 3)
                                {
                                    sb.Append("\t" + part2[0].Trim() + " = (DWORD) (0xFFFFFFF >> " + part2[2].Trim() + ") & " + part2[1].Trim() + ";\r\n");
                                    sb.Append(_asmstart);
                                    return sb.ToString();
                                }
                            }
                            break;
                        #endregion
                        #region divwu
                        case "divwu":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                sb.Append("\t" + part2[0].Trim() + " = (DWORD) (" + part2[1].Trim() + " / " + part2[2].Trim() + ");\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region mullw
                        case "mullw":
                            if (parts.Length == 2)
                            {
                                sb.Append(_asmend);
                                string[] part2 = parts[1].Trim().Split(',');
                                sb.Append("\t" + part2[0].Trim() + " = (DWORD) (" + part2[1].Trim() + " * " + part2[2].Trim() + ");\r\n");
                                sb.Append(_asmstart);
                                return sb.ToString();
                            }
                            break;
                        #endregion
                        #region blr
                        case "blr":
                            sb.Append(_asmend);
                            sb.Append("\treturn;\r\n");
                            sb.Append(_asmstart);
                            return sb.ToString();
                        #endregion
                        default:
                            break;
                    }
            }
            else if (input.StartsWith(".set"))
            {
                string[] parts = input.Trim().Replace(",", "").Replace("  ", " ").Split(' ');
                if (parts.Length == 3)
                {
                    sb.Append(_asmend);
                    sb.Append("\t" + parts[1] + " = " + parts[2] + " ;\r\n");
                    sb.Append(_asmstart);
                    return sb.ToString();
                }
            }
            return input + "\r\n";
        }

        public static XEXFunction Step2(List<string> AsmCode, XEXFunction f, ref string code)
        {
            StringReader sr = new StringReader(code);
            List<string> lines = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
                lines.Add(line);
            // simple if(){...} search
            for (int i = 0; i < lines.Count; i++)
                if (lines[i].StartsWith("\t\t"))
                {
                    string[] parts = lines[i].Trim().Split('\t');
                    if (parts.Length > 0)
                        switch (parts[0].Trim().ToLower())
                        {
                            case "bne":
                            case "beq":
                                List<string> temp = new List<string>();
                                string[] part2 = parts[1].Trim().Split(',');
                                if (part2.Length == 2)
                                {
                                    int pos = -1;
                                    for (int j = i + 1; j < lines.Count; j++)
                                        if (lines[j].Trim().Contains(part2[1].Trim()))
                                        {
                                            pos = j;
                                            break;
                                        }
                                    if (pos != -1)
                                    {
                                        for (int j = 0; j < i; j++)
                                            temp.Add(lines[j]);
                                        temp.Add("\t}");
                                        string compare = parts[0].Trim().ToLower();
                                        switch (compare)
                                        {
                                            case "bne":
                                                temp.Add("\tif(" + part2[0].Trim() + " != 0)");
                                                break;
                                            case "beq":
                                                temp.Add("\tif(" + part2[0].Trim() + " == 0)");
                                                break;
                                        }
                                        temp.Add("\t{");
                                        temp.Add("\t_asm");
                                        temp.Add("\t{");
                                        for (int j = i + 1; j < pos; j++)
                                            temp.Add(lines[j]);
                                        temp.Add("\t}");
                                        temp.Add("\t}");
                                        temp.Add("\t_asm");
                                        temp.Add("\t{");
                                        for (int j = pos; j < lines.Count; j++)
                                            temp.Add(lines[j]);
                                        lines = temp;
                                    }
                                }
                                break;
                        }
                }
            StringBuilder sb = new StringBuilder();
            foreach (string l in lines)
                sb.AppendLine(l);
            string result = sb.ToString();
            result = result.Replace(_asmstartend, "");
            //tabbing in ->
            sr = new StringReader(result);
            lines = new List<string>();
            while ((line = sr.ReadLine()) != null)
                lines.Add(line);
            for(int i=0;i<lines.Count;i++)
                if (lines[i].Trim().StartsWith("if("))
                {
                    int start = i + 2;
                    int end = start;
                    int depth = 1;
                    for (int j = start; j < lines.Count; j++)
                    {
                        if (lines[j].Trim().StartsWith("{"))
                            depth++;
                        if (lines[j].Trim().StartsWith("}"))
                            depth--;
                        if (depth == 0)
                        {
                            end = j;
                            break;
                        }
                    }
                    for (int j = start; j < end; j++)
                        if (!lines[j].StartsWith("loc"))
                            lines[j] = '\t' + lines[j];
                }
            sb = new StringBuilder();
            foreach (string l in lines)
                sb.AppendLine(l);
            code = sb.ToString();
            return f;
        }

        public static TreeNode MidStep(List<string> AsmCode)
        {
            TreeNode result = null;
            int endheader = 0;
            while (true)
            {
                if (AsmCode[endheader].Trim().StartsWith("#") ||
                    AsmCode[endheader].Trim() == "")
                {
                    endheader++;
                    continue;
                }
                if (AsmCode[endheader].Trim().StartsWith("sub_"))
                {
                    string name = AsmCode[endheader].Trim().Split(':')[0];
                    result = new TreeNode(name);
                    endheader++;
                    continue;
                }
                break;
            }
            if (result == null)
                return null;
            TreeNode t = new TreeNode("Header");
            for (int i = 1; i < endheader; i++)
                if (AsmCode[i].Trim() != "")
                    t.Nodes.Add(AsmCode[i]);
            result.Nodes.Add(t);

            int endvars = endheader;
            while (AsmCode[endvars].StartsWith(".set ") || AsmCode[endvars].Trim() == "")
                endvars++;
            t = new TreeNode("Variables");
            for (int i = endheader; i < endvars; i++)
                if (AsmCode[i].Trim() != "")
                    t.Nodes.Add(AsmCode[i]);
            result.Nodes.Add(t);
            t = new TreeNode("Blocks");
            int pos = endvars;
            int blockcount = 0;
            TreeNode t2 = new TreeNode("Block " + blockcount);
            while (pos < AsmCode.Count)
            {
                string tline = AsmCode[pos].Trim();
                if (!tline.StartsWith("#") && !tline.StartsWith("loc_") && AsmCode[pos].StartsWith("\t\t"))
                {
                    t2.Nodes.Add(AsmCode[pos]);
                    string instruction = AsmCode[pos].Trim().Split('\t')[0];
                    bool isbranch = false;
                    foreach(string b in mnemonics_branch)
                        if (instruction == b)
                        {
                            isbranch = true;
                            break;
                        }
                    if (isbranch)
                    {
                        blockcount++;
                        t.Nodes.Add(t2);
                        t2 = new TreeNode("Block " + blockcount);
                    }
                }
                if (tline.StartsWith("loc_"))
                {
                    blockcount++;
                    if (t2.Nodes.Count != 0)
                        t.Nodes.Add(t2);
                    t2 = new TreeNode("Block " + blockcount);
                    t2.Nodes.Add(AsmCode[pos]);
                }
                pos++;
            }
            if (t2.Nodes.Count != 0)
                t.Nodes.Add(t2);
            result.Nodes.Add(t);
            result.ExpandAll();
            return result;
        }
    }
}
