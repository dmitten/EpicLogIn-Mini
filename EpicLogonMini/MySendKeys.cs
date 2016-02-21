using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpicLogonMini
{
    class MySendKeys
    {
        //Keys pagedn = Keys.PageDown;
        Keys ctl = Keys.ControlKey;
        // Keys lshift = Keys.LShiftKey;
        Keys alt = Keys.Menu;
        //Keys p = Keys.P;
        Keys o = Keys.O;
        Keys r = Keys.R;
        Keys a = Keys.A;
        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void keybd_event(byte bVk, uint bScan, uint dwFlags, int dwExtraInfo);

        public void down(Keys key)
        {
            uint i = (ushort)MapVirtualKey((UInt16)(byte)key, 0);//http://stackoverflow.com/questions/5028872/sendkeys-alternative-that-works-on-citrix

            keybd_event((byte)key, i, 0, 0); // down 
        }
        public void up(Keys key)
        {
            uint i = (ushort)MapVirtualKey((UInt16)(byte)key, 0);//http://stackoverflow.com/questions/5028872/sendkeys-alternative-that-works-on-citrix

            keybd_event((byte)key, i, 2, 0); // up 
        }
        public void downold(Keys key)
        {
            uint i = (ushort)MapVirtualKey((UInt16)(byte)key, 0);//http://stackoverflow.com/questions/5028872/sendkeys-alternative-that-works-on-citrix

            keybd_event((byte)key, 0, 0, 0); // down 
        }
        public void upold(Keys key)
        {
            uint i = (ushort)MapVirtualKey((UInt16)(byte)key, 0);//http://stackoverflow.com/questions/5028872/sendkeys-alternative-that-works-on-citrix

            keybd_event((byte)key, 0, 2, 0); // up 
        }
        public void PressKey(Keys key)

        //    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        //public void PressKey(Keys key)
        {
            uint i = (ushort)MapVirtualKey((UInt16)(byte)key, 0);//http://stackoverflow.com/questions/5028872/sendkeys-alternative-that-works-on-citrix

            keybd_event((byte)key, i, 0, 0); // presses 
            Thread.Sleep(5);
            keybd_event((byte)key, i, 2, 0); //releases 
        }
        // {

        //    keybd_event((byte)key, 0, 0, 0); // presses 
        //    Thread.Sleep(10);
        //    keybd_event((byte)key,0, 2, 0); //releases 
        //}
        public void PressKeyOld(Keys key)
        {
            uint i = (ushort)MapVirtualKey((UInt16)(byte)key, 0);//http://stackoverflow.com/questions/5028872/sendkeys-alternative-that-works-on-citrix

            keybd_event((byte)key, i, 0, 0); // presses 
            keybd_event((byte)key, i, 2, 0); //releases 
        }

        public void PressKeyArray(Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                PressKey(key);

            }
        }
        public void PressKeyArrayPassword(Keys[] keys)//first letter is caps
        {
            int count = 1;
            foreach (Keys key in keys)
            {

                if (count == 1)//default password first key is caps
                {
                    down(Keys.ShiftKey);
                    PressKey(key);
                    up(Keys.ShiftKey);
                    Thread.Sleep(10);
                }
                else { PressKey(key); }
                count++;
            }
        }
        public void AltL()
        {

        }
        public void TypeString(string text)
        {
            bool first = false;
            string textUPPER = text.ToUpper();
            for (int i = 0; i < text.Length; i++)
            {
                string list = ".*";
                char period = list[0];
                char c = textUPPER[i];
                char cc = text[i];
                //Regex regex = new Regex("[A-Za-z]");
                //if (regex.IsMatch(text[i].ToString()))
                //{ 
                //    Letter = true;
                //}
                //  if (cc == c && c!=period) { caps = true; }
                // Keys k = (Keys)(byte)c;
                Keys k = (Keys)Enum.Parse(typeof(Keys), ((int)c).ToString());
                if (c == period) { k = Keys.OemPeriod; }
                if (first)
                {
                    Keys k1 = (Keys)(byte)c;
                    down(Keys.ShiftKey);
                    PressKeyOld(k1);
                    up(Keys.ShiftKey);
                    first = false;
                }
                else { PressKeyOld(k); }
            }
        }
        public void PageDown()
        {
            Keys key = Keys.PageDown;
            {
                uint i = (ushort)MapVirtualKey((UInt16)(byte)key, 0);//http://stackoverflow.com/questions/5028872/sendkeys-alternative-that-works-on-citrix

                keybd_event((byte)key, i, 0, 0); // presses 
                Thread.Sleep(10);
                keybd_event((byte)key, i, 2, 0); //releases 
            }
        }
        public void AltA()
        {
            down(alt);
            down(a);
            Thread.Sleep(10);
            up(a);
            up(alt);

        }
        public void AltC()
        {
            down(alt);
            down(Keys.C);
            Thread.Sleep(10);
            up(Keys.C);
            up(alt);

        }
        public void CntrlAltL()//log off
        {
            down(ctl);
            down(alt);
            down(Keys.L);
            Thread.Sleep(10);
            up(Keys.L);
            up(alt);
            up(ctl);

        }
        public void AltO()
        {
            down(alt);
            down(o);
            Thread.Sleep(10);
            up(o);
            up(alt);

        }
        public void AltW()
        {
            down(alt);
            down(Keys.W);
            Thread.Sleep(10);
            up(Keys.W);
            up(alt);
        }
        public void Alt1()
        {
            down(alt);
            down(Keys.D1);
            Thread.Sleep(10);
            up(Keys.D1);
            up(alt);
        }
        public void Alt3()
        {
            down(alt);
            down(Keys.D3);
            Thread.Sleep(10);
            up(Keys.D3);
            up(alt);
        }
        public void CtlShiftA()//opens allergies
        {
            down(ctl);
            down(Keys.ShiftKey);
            down(Keys.A);
            Thread.Sleep(10);
            up(Keys.A);
            up(Keys.ShiftKey);
            up(ctl);
        }
        public void CtlShiftO()//opens orders
        {
            down(ctl);
            down(Keys.ShiftKey);
            down(Keys.O);
            Thread.Sleep(10);
            up(Keys.O);
            up(Keys.ShiftKey);
            up(ctl);
        }
        public void CtlShiftL()// opens level of service
        {
            down(ctl);
            down(Keys.ShiftKey);
            down(Keys.L);
            Thread.Sleep(10);
            up(Keys.L);
            up(Keys.ShiftKey);
            up(ctl);
        }
        public void CtlA()// 
        {
            down(Keys.LControlKey);
            down(Keys.A);
            Thread.Sleep(10);
            up(Keys.A);
            up(Keys.LControlKey);
        }
        public void CtlC()// 
        {
            down(Keys.LControlKey);
            down(Keys.C);
            Thread.Sleep(10);
            up(Keys.C);
            up(Keys.LControlKey);
        }
        public void CtlV()// opens level of service
        {
            down(Keys.LControlKey);
            down(Keys.V);
            Thread.Sleep(10);
            up(Keys.V);
            up(Keys.LControlKey);
        }
        public void OldCtlV()// paste
        {

            downold(Keys.LControlKey);
            down(Keys.V);
            Thread.Sleep(10);
            up(Keys.V);
            upold(Keys.LControlKey);
        }
        public void CtlShiftP()//opens problem list
        {
            down(ctl);
            down(Keys.ShiftKey);
            down(Keys.P);
            Thread.Sleep(10);
            up(Keys.P);
            up(Keys.ShiftKey);
            up(ctl);
        }
        public void CtlShiftMNewNote()
        {
            down(Keys.ControlKey);
            down(Keys.ShiftKey);
            down(Keys.N);
            Thread.Sleep(10);
            up(Keys.N);
            up(Keys.ShiftKey);
            up(Keys.ControlKey);
        }
        public void CtlHome()
        {
            down(ctl);
            down(Keys.Home);
            Thread.Sleep(10);
            up(Keys.Home);
            up(ctl);
        }
        public void AltR()
        {
            down(alt);
            down(r);
            Thread.Sleep(10);
            up(r);
            up(alt);
        }

        public void CTRLG()
        {

        }


    }
}