using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GiveME2Entitlements
{
    

    class Program
    {
        private static string[] Marvin = new string[] { "  ,===.", 
                                                        "  !~`._`_", 
                                                       @"  != |;--\", 
                                                        "  !- |!  !         _.-==-._", 
                                                        "  !~ |!(`!     _.-'        `-.", 
                                                       @"  != |! `.`. .'.'      __.._  \", 
                                                        "   `.L!__!`.' /     .-'--.  `.|", 
                                                       @"          /   |   .' '\  /`.  /", 
                                                       @"          |   |  /  /`.`' /\`Y", 
                                                       @"          |   |_/  |  _\ / _||", 
                                                       @"          \   (_)   \(_| |(_/|", 
                                                       @"           `.    \   `-' `-' /`-.", 
                                                       @"             \    `._      .f___|", 
                                                        "             |`      |----'", 
                                                        "             `-------'" };

        private static string ComputeDLCHash(string basePath)
        {
            using (SHA1 sha = SHA1.Create())
            {
                sha.Initialize();
                byte[] buffer = new byte[0x1000];
                foreach (string str in from candidate in Directory.GetFiles(basePath)
                                       where ShouldHashPath(candidate)
                                       orderby candidate.ToUpperInvariant()
                                       select candidate)
                {
                    Stream stream = File.Open(str, FileMode.Open, FileAccess.Read, FileShare.Read);
                    int inputCount = stream.Read(buffer, 0, buffer.Length);
                    sha.TransformBlock(buffer, 0, inputCount, null, 0);
                    stream.Close();
                }
                sha.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(sha.Hash).Replace("-", "").ToLower();
            }
        }

        private static byte[] JoeGrafIsAHomo()
        {
            byte[] destinationArray = new byte[8];
            List<IpHelp.Native.IP_ADAPTER_INFO> adaptersInfo = IpHelp.GetAdaptersInfo();
            if (adaptersInfo == null)
            {
                return null;
            }
            foreach (IpHelp.Native.IP_ADAPTER_INFO ip_adapter_info in adaptersInfo)
            {
                if (ip_adapter_info.AddressLength <= destinationArray.Length)
                {
                    Array.Copy(ip_adapter_info.Address, 0, destinationArray, 0, (long)ip_adapter_info.AddressLength);
                }
            }
            destinationArray[0] = (byte)(destinationArray[0] ^ 0x65);
            destinationArray[1] = (byte)(destinationArray[1] ^ 0x6f);
            destinationArray[2] = (byte)(destinationArray[2] ^ 0x4a);
            destinationArray[4] = (byte)(destinationArray[4] ^ 0x66);
            destinationArray[5] = (byte)(destinationArray[5] ^ 0x61);
            destinationArray[6] = (byte)(destinationArray[6] ^ 0x72);
            destinationArray[7] = (byte)(destinationArray[7] ^ 0x47);
            return destinationArray;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("give me2 entitlements  ~ v2 ~");
            Console.WriteLine(" ~ brought to you buy that guy that brought you de dao drm ~");
            Console.WriteLine();
            string str = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\BioWare\Mass Effect 2", "Path", null);
            if (str == null)
            {
                MessageBox.Show("Could not find Mass Effect 2 directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string path = Path.Combine(Path.Combine(str, "BioGame"), "DLC");
                if (!Directory.Exists(path))
                {
                    MessageBox.Show("Could not find Mass Effect 2 DLC directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    Console.WriteLine("Looking for DLC (and computing MAGIC HASHES!!)...");
                    StringWriter writer = new StringWriter();
                    writer.WriteLine("[Hash]");
                    foreach (string str3 in Directory.GetDirectories(path))
                    {
                        string str4 = Path.Combine(str3, "CookedPC");
                        if (Directory.Exists(str4))
                        {
                            string str5 = Path.Combine(str4, "Mount.dlc");
                            if (File.Exists(str5))
                            {
                                Stream stream = File.Open(str5, FileMode.Open, FileAccess.Read, FileShare.Read);
                                stream.Seek(12, SeekOrigin.Begin);
                                byte[] buffer = new byte[4];
                                stream.Read(buffer, 0, 4);
                                uint num = BitConverter.ToUInt32(buffer, 0);
                                stream.Seek(0x2c, SeekOrigin.Begin);
                                stream.Read(buffer, 0, 4);
                                int num2 = BitConverter.ToInt32(buffer, 0);
                                string str6 = "";
                                if ((num2 > 0) && (num2 < 0x100))
                                {
                                    buffer = new byte[num2 - 1];
                                    stream.Read(buffer, 0, num2 - 1);
                                    str6 = Encoding.ASCII.GetString(buffer);
                                }
                                stream.Close();
                                string str7 = ComputeDLCHash(str4);
                                if (str6.Length > 0)
                                {
                                    Console.WriteLine(" + {1} ({0})", Path.GetFileName(str3), str6);
                                }
                                else
                                {
                                    Console.WriteLine(" + {0}", Path.GetFileName(str3));
                                }
                                writer.WriteLine("DirtyStinkingPirate.Mount{0}={1}", num, str7);
                            }
                        }
                    }
                    Console.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine("[Global]");
                    writer.WriteLine("LastNucleusID=DirtyStinkingPirate");
                    writer.WriteLine();
                    writer.WriteLine("[KeyValuePair]");
                    writer.WriteLine("DirtyStinkingPirate.Entitlement.ME2PCOffers.ONLINE_ACCESS=TRUE");
                    writer.WriteLine("DirtyStinkingPirate.Entitlement.ME2PCOffers.PC_CERBERUS_NETWORK=TRUE");
                    writer.WriteLine("DirtyStinkingPirate.Numeric.DaysSinceReg=0");
                    writer.WriteLine();
                    Console.WriteLine("Saving BioPersistentEntitlementCache.ini...");
                    byte[] bytes = Encoding.Unicode.GetBytes(writer.GetStringBuilder().ToString());
                    byte[] magic = JoeGrafIsAHomo();
                    if (magic == null)
                    {
                        MessageBox.Show("Could not get adapter entropy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        bytes = DataProtection.Encrypt(bytes, magic);
                        if (bytes == null)
                        {
                            MessageBox.Show("Could not encrypt entitlement cache.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            string str8 = Path.Combine(Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "BioWare"), "Mass Effect 2"), "BIOGame"), "Config");
                            if (!Directory.Exists(str8))
                            {
                                Directory.CreateDirectory(str8);
                            }
                            string str9 = Path.Combine(str8, "BioPersistentEntitlementCache.ini");
                            if (!File.Exists(str9) || (MessageBox.Show(string.Format("Overwrite \"{0}\"?", str9), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                            {
                                Stream stream2 = File.Open(str9, FileMode.Create, FileAccess.Write);
                                stream2.Write(bytes, 0, bytes.Length);
                                stream2.Close();
                                Console.WriteLine();
                                Console.WriteLine("FINALE!");
                                Console.WriteLine();
                                foreach (string str10 in Marvin)
                                {
                                    Console.WriteLine(str10);
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
        }

        private static bool ShouldHashPath(string filePath)
        {
            if (filePath.EndsWith(".pcc") || filePath.EndsWith(".ini"))
            {
                if (filePath.Length < 8)
                {
                    return true;
                }
                char ch = filePath[filePath.Length - 8];
                if (ch != '_')
                {
                    return true;
                }
                string str = filePath.Substring(filePath.Length - 7, 3).ToUpperInvariant();
                for (int i = 0; i < str.Length; i++)
                {
                    if ((str[i] < 'A') || (str[i] > 'Z'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
