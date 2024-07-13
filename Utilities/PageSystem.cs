using Colorful;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;

using Console = Colorful.Console;

namespace r6recoil.Utilities
{
    internal class PageSystem
    {

        public static void AttackersPage(XmlDocument OperatorsXml)
        {
            Console.Clear();
            Helper.SetMessageColor(255, 255, 255);
            Helper.Message("Operators - Use keys to navigate menu. (Page: 1/2) //\n", "\\\\");

            int CurrentLine = 3;
            int TableLine = 0;
            int LastLine = 3;
            int CurrentPairSlot = 2;
            bool EnterPressed = false;
            bool WasFirst = false;

            Helper.SetMessageColor(34, 143, 223);
            Helper.Message("Attacking:");

            XmlNodeList AttackingTable = OperatorsXml.SelectNodes("/root/Attacking/element");

            foreach (XmlNode Attacker in AttackingTable)
            {
                string OperatorName = Attacker["Name"].InnerText;
                string XAxis = Attacker["X"].InnerText;
                string YAxis = Attacker["Y"].InnerText;

                Helper.Message(String.Format("{0}: ({1}, {2})", OperatorName, XAxis, YAxis), "   -");
                LastLine += 1;

                if (!WasFirst)
                {
                    Program.XAxis = Int32.Parse(XAxis);
                    Program.YAxis = Int32.Parse(YAxis);
                    WasFirst = true;
                }

            }

            LastLine -= 1;
            Console.WriteLine("------------------------------------------------------------");
            
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, CurrentLine);
            Console.Write("=>", Color.FromArgb(255, 135, 52));


            do
            {
                while (!Console.KeyAvailable)
                {
                    ConsoleKeyInfo KeyPressed = Console.ReadKey(true);

                    if (KeyPressed.Key == ConsoleKey.UpArrow)
                    {
                        EnterPressed = false;
                        CurrentPairSlot = 2;

                        Console.Write("\b \b");
                        Console.Write("\b \b");

                        XmlNode Attacker = AttackingTable[TableLine];
                        string OperatorName = Attacker["Name"].InnerText;
                        string XAxis = Attacker["X"].InnerText;
                        string YAxis = Attacker["Y"].InnerText;

                        Helper.Message(String.Format("{0}: ({1}, {2})", OperatorName, XAxis, YAxis), "   -");

                        CurrentLine -= 1;
                        TableLine -= 1;

                        if (3 > CurrentLine)
                        {
                            CurrentLine = LastLine;
                        }

                        if (0 > TableLine)
                        {
                            TableLine = (AttackingTable.Count - 1);
                        }

                        XmlNode Attacker2 = AttackingTable[TableLine]; ;
                        string XAxis2 = Attacker2["X"].InnerText;
                        string YAxis2 = Attacker2["Y"].InnerText;

                        Program.XAxis = Int32.Parse(XAxis2);
                        Program.YAxis = Int32.Parse(YAxis2);
                    }
                    else if (KeyPressed.Key == ConsoleKey.DownArrow)
                    {
                        EnterPressed = false;
                        CurrentPairSlot = 2;

                        Console.Write("\b \b");
                        Console.Write("\b \b");

                        XmlNode Attacker = AttackingTable[TableLine];
                        string OperatorName = Attacker["Name"].InnerText;
                        string XAxis = Attacker["X"].InnerText;
                        string YAxis = Attacker["Y"].InnerText;

                        Helper.Message(String.Format("{0}: ({1}, {2})", OperatorName, XAxis, YAxis), "   -");

                        CurrentLine += 1;
                        TableLine += 1;

                        if (CurrentLine > LastLine)
                        {
                            CurrentLine = 3;
                        }

                        if (TableLine > (AttackingTable.Count - 1))
                        {
                            TableLine = 0;
                        }

                        XmlNode Attacker2 = AttackingTable[TableLine];;
                        string XAxis2 = Attacker2["X"].InnerText;
                        string YAxis2 = Attacker2["Y"].InnerText;

                        Program.XAxis = Int32.Parse(XAxis2);
                        Program.YAxis = Int32.Parse(YAxis2);
                    }
                    else if (KeyPressed.Key == ConsoleKey.RightArrow && EnterPressed)
                    {
                        XmlNode Attacker = AttackingTable[TableLine];
                        string OperatorName = Attacker["Name"].InnerText;
                        double XAxis = Int32.Parse(Attacker["X"].InnerText);
                        double YAxis = Int32.Parse(Attacker["Y"].InnerText);


                        Console.WriteLine("                     ");
                        Console.SetCursorPosition(0, CurrentLine);

                        Helper.Message((OperatorName + ": "), "   -", true);

                        if (CurrentPairSlot == 1)
                        {
                            XAxis += 0.25;

                            if (XAxis > 20)
                            {
                                XAxis = -20;
                            }

                            Attacker["X"].InnerText = XAxis.ToString();

                            Console.Write("(");
                            Console.Write(XAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(", " + YAxis + ")");
                        }
                        else
                        {
                            YAxis += 0.25;

                            if (YAxis > 20)
                            {
                                YAxis = -20;
                            }

                            Attacker["Y"].InnerText = YAxis.ToString();

                            Console.Write("(" + XAxis + ", ");
                            Console.Write(YAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(")");
                        }

                        Program.XAxis = XAxis;
                        Program.YAxis = YAxis;

                        OperatorsXml.Save("Operators.xml");
                    }
                    else if (KeyPressed.Key == ConsoleKey.LeftArrow && EnterPressed)
                    {
                        XmlNode Attacker = AttackingTable[TableLine];
                        string OperatorName = Attacker["Name"].InnerText;
                        double XAxis = Int32.Parse(Attacker["X"].InnerText);
                        double YAxis = Int32.Parse(Attacker["Y"].InnerText);


                        Console.WriteLine("                     ");
                        Console.SetCursorPosition(0, CurrentLine);

                        Helper.Message((OperatorName + ": "), "   -", true);

                        if (CurrentPairSlot == 1)
                        {
                            XAxis -= 0.25;

                            if (XAxis < -20)
                            {
                                XAxis = 20;
                            }

                            Attacker["X"].InnerText = XAxis.ToString();

                            Console.Write("(");
                            Console.Write(XAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(", " + YAxis + ")");
                        }
                        else
                        {
                            YAxis -= 0.25;

                            if (YAxis < -20)
                            {
                                YAxis = 20;
                            }

                            Attacker["Y"].InnerText = YAxis.ToString();

                            Console.Write("(" + XAxis + ", ");
                            Console.Write(YAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(")");
                        }

                        Program.XAxis = XAxis;
                        Program.YAxis = YAxis;

                        OperatorsXml.Save("Operators.xml");
                    }
                    else if (KeyPressed.Key == ConsoleKey.Enter)
                    {
                        XmlNode Attacker = AttackingTable[TableLine];
                        string OperatorName = Attacker["Name"].InnerText;
                        string XAxis = Attacker["X"].InnerText;
                        string YAxis = Attacker["Y"].InnerText;

                        Console.SetCursorPosition(0, CurrentLine);
                        //Console.WriteLine();

                        Helper.Message((OperatorName + ": "), "   -", true);

                        if (CurrentPairSlot == 2)
                        {
                            CurrentPairSlot = 1;
                            Console.Write("(");
                            Console.Write(XAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(", " + YAxis + ")");
                        }
                        else
                        {
                            CurrentPairSlot = 2;
                            Console.Write("(" + XAxis + ", ");
                            Console.Write(YAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(")");
                        }

                        EnterPressed = true;
                    }
                    else if (KeyPressed.Key == ConsoleKey.D2)
                    {
                        break;
                    }

                    Console.SetCursorPosition(0, CurrentLine);
                    Console.Write("=>", Color.FromArgb(255, 135, 52));
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.D2);
        Program.RunDefendersPage(OperatorsXml);
        }

        /* ---------------------------------------------------------------------------- */
        /* ---------------------------------------------------------------------------- */
        /* ---------------------------------------------------------------------------- */

        public static void DefendersPage(XmlDocument OperatorsXml)
        {
            Console.Clear();
            Helper.SetMessageColor(255, 255, 255);
            Helper.Message("Operators - Use keys to navigate menu. (Page: 2/2) //\n", "\\\\");

            int CurrentLine = 3;
            int TableLine = 0;
            int LastLine = 3;
            int CurrentPairSlot = 2;
            bool EnterPressed = false;
            bool WasFirst = false;

            Helper.SetMessageColor(223, 65, 74);
            Helper.Message("Defending:");

            XmlNodeList DefendingTable = OperatorsXml.SelectNodes("/root/Defending/element");

            foreach (XmlNode Defender in DefendingTable)
            {
                string OperatorName = Defender["Name"].InnerText;
                string XAxis = Defender["X"].InnerText;
                string YAxis = Defender["Y"].InnerText;

                Helper.Message(String.Format("{0}: ({1}, {2})", OperatorName, XAxis, YAxis), "   -");
                LastLine += 1;

                if (!WasFirst)
                {
                    Program.XAxis = Int32.Parse(XAxis);
                    Program.YAxis = Int32.Parse(YAxis);
                    WasFirst = true;
                }
            }

            LastLine -= 1;
            Console.WriteLine("------------------------------------------------------------");

            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, CurrentLine);
            Console.Write("=>", Color.FromArgb(255, 135, 52));


            do
            {
                while (!Console.KeyAvailable)
                {
                    ConsoleKeyInfo KeyPressed = Console.ReadKey(true);
                    if (KeyPressed.Key == ConsoleKey.UpArrow)
                    {
                        EnterPressed = false;
                        CurrentPairSlot = 2;

                        Console.Write("\b \b");
                        Console.Write("\b \b");

                        XmlNode Defender = DefendingTable[TableLine];
                        string OperatorName = Defender["Name"].InnerText;
                        string XAxis = Defender["X"].InnerText;
                        string YAxis = Defender["Y"].InnerText;

                        Helper.Message(String.Format("{0}: ({1}, {2})", OperatorName, XAxis, YAxis), "   -");

                        CurrentLine -= 1;
                        TableLine -= 1;

                        if (3 > CurrentLine)
                        {
                            CurrentLine = LastLine;
                        }

                        if (0 > TableLine)
                        {
                            TableLine = (DefendingTable.Count - 1);
                        }

                        XmlNode Defender2 = DefendingTable[TableLine]; ;
                        string XAxis2 = Defender["X"].InnerText;
                        string YAxis2 = Defender["Y"].InnerText;

                        Program.XAxis = Int32.Parse(XAxis2);
                        Program.YAxis = Int32.Parse(YAxis2);
                    }
                    else if (KeyPressed.Key == ConsoleKey.DownArrow)
                    {
                        EnterPressed = false;
                        CurrentPairSlot = 2;

                        Console.Write("\b \b");
                        Console.Write("\b \b");

                        XmlNode Defender = DefendingTable[TableLine];
                        string OperatorName = Defender["Name"].InnerText;
                        string XAxis = Defender["X"].InnerText;
                        string YAxis = Defender["Y"].InnerText;

                        Helper.Message(String.Format("{0}: ({1}, {2})", OperatorName, XAxis, YAxis), "   -");

                        CurrentLine += 1;
                        TableLine += 1;

                        if (CurrentLine > LastLine)
                        {
                            CurrentLine = 3;
                        }

                        if (TableLine > (DefendingTable.Count - 1))
                        {
                            TableLine = 0;
                        }

                        XmlNode Defender2 = DefendingTable[TableLine]; ;
                        string XAxis2 = Defender["X"].InnerText;
                        string YAxis2 = Defender["Y"].InnerText;
                    }
                    else if (KeyPressed.Key == ConsoleKey.RightArrow && EnterPressed)
                    {
                        XmlNode Defender = DefendingTable[TableLine];
                        string OperatorName = Defender["Name"].InnerText;
                        int XAxis = Int32.Parse(Defender["X"].InnerText);
                        int YAxis = Int32.Parse(Defender["Y"].InnerText);


                        Console.WriteLine("                     ");
                        Console.SetCursorPosition(0, CurrentLine);

                        Helper.Message((OperatorName + ": "), "   -", true);

                        if (CurrentPairSlot == 1)
                        {
                            XAxis += 1;

                            if (XAxis > 10)
                            {
                                XAxis = -10;
                            }

                            Defender["X"].InnerText = XAxis.ToString();

                            Console.Write("(");
                            Console.Write(XAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(", " + YAxis + ")");
                        }
                        else
                        {
                            YAxis += 1;

                            if (YAxis > 10)
                            {
                                YAxis = -10;
                            }

                            Defender["Y"].InnerText = YAxis.ToString();

                            Console.Write("(" + XAxis + ", ");
                            Console.Write(YAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(")");
                        }

                        Program.XAxis = XAxis;
                        Program.YAxis = YAxis;

                        OperatorsXml.Save("Operators.xml");
                    }
                    else if (KeyPressed.Key == ConsoleKey.LeftArrow && EnterPressed)
                    {
                        XmlNode Defender = DefendingTable[TableLine];
                        string OperatorName = Defender["Name"].InnerText;
                        int XAxis = Int32.Parse(Defender["X"].InnerText);
                        int YAxis = Int32.Parse(Defender["Y"].InnerText);


                        Console.WriteLine("                     ");
                        Console.SetCursorPosition(0, CurrentLine);

                        Helper.Message((OperatorName + ": "), "   -", true);

                        if (CurrentPairSlot == 1)
                        {
                            XAxis -= 1;

                            if (XAxis < -10)
                            {
                                XAxis = 10;
                            }

                            Defender["X"].InnerText = XAxis.ToString();

                            Console.Write("(");
                            Console.Write(XAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(", " + YAxis + ")");
                        }
                        else
                        {
                            YAxis -= 1;

                            if (YAxis < -10)
                            {
                                YAxis = 10;
                            }

                            Defender["Y"].InnerText = YAxis.ToString();

                            Console.Write("(" + XAxis + ", ");
                            Console.Write(YAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(")");
                        }

                        Program.XAxis = XAxis;
                        Program.YAxis = YAxis;

                        OperatorsXml.Save("Operators.xml");
                    }
                    else if (KeyPressed.Key == ConsoleKey.Enter)
                    {
                        XmlNode Defender = DefendingTable[TableLine];
                        string OperatorName = Defender["Name"].InnerText;
                        string XAxis = Defender["X"].InnerText;
                        string YAxis = Defender["Y"].InnerText;

                        Console.SetCursorPosition(0, CurrentLine);
                        //Console.WriteLine();

                        Helper.Message((OperatorName + ": "), "   -", true);

                        if (CurrentPairSlot == 2)
                        {
                            CurrentPairSlot = 1;
                            Console.Write("(");
                            Console.Write(XAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(", " + YAxis + ")");
                        }
                        else
                        {
                            CurrentPairSlot = 2;
                            Console.Write("(" + XAxis + ", ");
                            Console.Write(YAxis, Color.FromArgb(255, 255, 255));
                            Console.Write(")");
                        }

                        EnterPressed = true;
                    }
                    else if (KeyPressed.Key == ConsoleKey.D1)
                    {
                        break;
                    }

                    Console.SetCursorPosition(0, CurrentLine);
                    Console.Write("=>", Color.FromArgb(255, 135, 52));
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.D1);
            Program.RunAttackersPage(OperatorsXml);
        }
    }
}
