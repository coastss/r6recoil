using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Console = Colorful.Console;

using r6recoil.Utilities;
using System.Runtime.InteropServices;

namespace r6recoil.Utilities
{
    internal class Helper
    {
        static int Red = 255;
        static int Blue = 255;
        static int Green = 255;

        public static void UpdateTitle()
        {
            string SiegeStatus = "inactive";
            string RecoilReductionStatus = "disabled";

            if (Program.SiegeActive)
            {
                SiegeStatus = "active";
            }

            if (Program.RecoilReductionStatus)
            {
                RecoilReductionStatus = "enabled";
            }

            Console.Title = String.Format("r6recoil | build: {0} | siege window focus: {1} | recoil reduction: {2}", Program.R6RVersion, SiegeStatus, RecoilReductionStatus);
            
        }

        public static void SetMessageColor(int R, int G, int B)
        {
            Red = R;
            Green = G;
            Blue = B;
        }

        public static void Message(string String, string Custom = "[+]", bool NoNewLine = false)
        {
            Console.Write(Custom + " ", Color.FromArgb(Red, Green, Blue));

            if (!NoNewLine)
            {
                Console.Write(String + "\n");
            }
            else 
            {
                Console.Write(String);
            }
        }

        public static void ProcessWait(string ProcessName, bool Debug = false)
        {
            Process[] Application = Process.GetProcessesByName(ProcessName);

            while (Application.Length < 1)
            {
                Application = Process.GetProcessesByName(ProcessName);
                if (Application.Length >= 1)
                {
                    break;
                }
            }

            if (Debug)
            {
                Helper.Message("Found process: " + ProcessName);
            }
        }

        public static void WaitForProcess(string ProcessName)
        {
            Message(ProcessName + ": ", "   -", true);
            ProcessWait(ProcessName);
            Console.WriteLine("Ok!");
        }
    }
}
