using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EpicLogonMini
{
    public class Find
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        public void FindBmp( Image screenImage )
        {
            Bitmap screen = (Bitmap)screenImage;
            string epicString = "ff0046b9ff047dd8ff047dd8ff047dd8ff047dd8ff047dd8ff047dd8ff047dd8ff808080ffffffff";
            string startingcolor = "ff0046b9";
            //string startingcolor = "ffffffff";// white
            Point curPos = new Point(Cursor.Position.X, Cursor.Position.Y);
            Size curSize = new Size();
            curSize.Height = Cursor.Current.Size.Height;
            Rectangle bounds = Screen.GetBounds(Screen.GetBounds(Point.Empty));
            int x = 800;// screen.Width-20;
            int y = 700;// screen.Height;
            int X = 35;int Y = 1;
            string tracker = "";
            string t = "";
            int count = 0;
            List<string> l = new List<string>();
            do// loop y
            {
                do
                {
                   // Cursor.Position = new Point(X, Y);
                    t= screen.GetPixel(X, Y).Name.ToString();
                    if (t.Equals(startingcolor, StringComparison.Ordinal))// found start
                    {
                        count++;
                       // Cursor.Position = new Point(X , Y );
                        string screenstring = "";
                        for (int i = 0; i < 10; i++)
                        {
                            screenstring += screen.GetPixel(X + i, Y).Name.ToString();
                        }
                        tracker = screenstring;
                        if (screenstring.Contains(epicString))
                        {
                            Cursor.Position = new Point(X +20, Y-5);
                            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new IntPtr());
                            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new IntPtr());
                            //Thread.Sleep(500);
                            X = 5000; Y = 5000; // stop
                        }
                     }
                    X++;
                } while (X <= x );
                l.Add(tracker); tracker = "";
                X = 35;
                Y++;
                //} while (Y <= y - 5);   
            } while (Y <= y);
            return;
        }
    }
}
