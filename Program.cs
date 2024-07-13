using System;
using System.Diagnostics;
using System.Threading;
using r6recoil.Utilities;

using System.Xml;
using System.Reflection.Emit;
using System.Timers;

namespace r6recoil
{
    

    internal class Program
    {
        public static string R6RVersion = DateTime.Now.ToString("MM/dd/yyyy");
        public static bool RecoilReductionStatus = false;
        public static bool SiegeActive = false;

        public static double XAxis = 0;
        public static double YAxis = 0;

        public static void RunAttackersPage(XmlDocument OperatorsXml)
        {
            PageSystem.AttackersPage(OperatorsXml);
        }

        public static void RunDefendersPage(XmlDocument OperatorsXml)
        {
            PageSystem.DefendersPage(OperatorsXml);
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            Console.Title = "r6recoil";
            Helper.SetMessageColor(94, 157, 255);
            Console.SetWindowSize(155, 37);

            /* Check for any updates */
            Helper.Message("Checking for updates...");
            Helper.Message("Up to date! (" + R6RVersion + ")\n");

            /* Wait for Rainbow 6 Siege */
            Console.Title = ("r6recoil");
            Helper.SetMessageColor(255, 135, 52);

            Helper.Message("Waiting for game processes (3)...");
            Helper.WaitForProcess("BEService");
            Helper.WaitForProcess("RainbowSix");
            Helper.WaitForProcess("RainbowSix_BE");
            
            /* Info */
            Console.WriteLine();
            Helper.SetMessageColor(255, 99, 208);
            Helper.Message("Menu Navigation:");
            Helper.Message("Use the 'Up'/'Down' arrow key to change the selected operator (highlighted with: '=>').", "   -");
            Helper.Message("Use the 'Left'/'Right' arrow key to change the X or Y axis value.", "   -");
            Helper.Message("Press the 'Enter' key to switch between X or Y axis customization.", "   -");
            Helper.Message("Press the '1'/'2' number key TWICE to switch between the Attacking or Defending operator page.", "   -");
            Helper.Message("Make sure to recalibrate any weapon anytime you change attachments.", "   -");
            Helper.Message("Press 'F2' to enable/disable recoil reduction. It will automatically disable itself after switching pages.", "   -");
            Helper.Message("Recoil reduction works only if: Siege is focused, recoil reduction is enabled, and you are AIMING and SHOOTING.\n", "   -");

            Helper.Message("Ready! Press any key to launch...", "[+]", true);
            Console.ReadKey(true);

            /* Load Config/Operators Xml Document */
            XmlDocument OperatorsXml = new XmlDocument();
            OperatorsXml.Load("Operators.xml");

            /* Running Loops*/
            Thread GameToggleLoop = new Thread(new ThreadStart(Game.ToggleLoop));
            GameToggleLoop.Start();

            Thread GameRecoilLoop = new Thread(new ThreadStart(Game.RecoilLoop));
            GameRecoilLoop.Start();

            RunAttackersPage(OperatorsXml);
        }
    }
}