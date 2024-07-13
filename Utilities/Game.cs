using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Drawing;

namespace r6recoil.Utilities
{
    internal class Game
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        // Token: 0x06000012 RID: 18
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(ushort virtualKeyCode);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, double dx, double dy, int dwData, int dwExtraInfo);

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }

            return null;
        }

        public static bool bGetAsyncKeyState(Keys vKey)
        {
            int asyncKeyState = (int)GetAsyncKeyState(vKey);
            return asyncKeyState == 1 || asyncKeyState == -32768;
        }

        public static bool IsSiegeActive()
        {
            if (GetActiveWindowTitle() == "Rainbow Six")
            {
                return true;
            }

            return false;
        }

        public static void MoveMouse(double DeltaX, double DeltaY)
        {
            mouse_event(1, DeltaX, DeltaY, 0, 0);
        }

        public static void ToggleLoop()
        {
            while (true)
            {
                short KeyState = GetAsyncKeyState(0x71);

                bool F2Pressed = ((KeyState >> 15) & 0x0001) == 0x0001;

                if (F2Pressed)
                {
                    Program.RecoilReductionStatus = !Program.RecoilReductionStatus;
                    Thread.Sleep(500);
                }

            }
          
        }

        public static void RecoilLoop()
        {
            while (true)
            {
                Program.SiegeActive = IsSiegeActive();
                Helper.UpdateTitle();

                if (Program.SiegeActive && Program.RecoilReductionStatus && bGetAsyncKeyState(Keys.LButton) && bGetAsyncKeyState(Keys.RButton))
                {
                    Debug.Write(Program.XAxis);
                    Debug.Write(" ");
                    Debug.Write(Program.YAxis);
                    Debug.Write("\n");

                    MoveMouse(Program.XAxis, Program.YAxis);
                    Thread.Sleep(1);
                }
            }
        }
    }
}
