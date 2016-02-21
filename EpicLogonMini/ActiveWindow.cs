using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EpicLogonMini
{
    class ActiveWindow
    {
        public string WindowCaption { get; set; }
        public IntPtr WindowHandle { get; set; }
        // public List<WindowProcesses> ActiveProc { get; set; }
        // public List< Process> ActiveProcs;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        public struct Rect
        {
            public int Bottom { get; set; }
            public int Right { get; set; }
            public int Left { get; set; }
            public int Top { get; set; }
        }

        public List<WindowProcesses> GetAllProcesses()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            List<WindowProcesses> ActiveProc1 = new List<WindowProcesses>();
            Process[] procs = Process.GetProcesses();
            IntPtr hWnd;
            foreach (Process proc in procs)
            {
                if ((hWnd = proc.MainWindowHandle) != IntPtr.Zero)
                {
                    WindowProcesses wp = new WindowProcesses();
                    wp.name = proc.ProcessName.ToString();
                    wp.handlePtr = hWnd;
                    wp.caption = "";
                    // NativeWin32.SetForegroundWindow(hWnd.ToInt32());
                    // Thread.Sleep(1000);
                    if (GetWindowText(hWnd, Buff, nChars) > 0)
                    {
                        wp.caption = Buff.ToString();
                    }
                    ActiveProc1.Add(wp);
                }
            }
            return ActiveProc1;
        }

        public string GetActiveWindowCaption()
        {
            WindowCaption = "not found";
            const int nChars = 256;
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                WindowCaption = Buff.ToString();
                WindowHandle = handle;
            }
            return WindowCaption;
        }
        public string GetEpicUserName()//for some reason cant get name when not active window
        {
            string name = "not found";
            WindowCaption = "";
            const int nChars = 256;
            IntPtr handle;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                WindowCaption = Buff.ToString();
                WindowHandle = handle;
            }
            if (WindowCaption.Contains("Hyper"))// active window is erecord
            {
                Regex regex = new Regex(@"PRD - \w+ \w");
                name = (regex.Match(WindowCaption).ToString());
                if (name.Count() >= 4)
                {
                    string[] n = name.Split(' ');
                    // name = "*" + n[2].Substring(0, 1) + n[3] + "*";// this adds two stars
                    name = n[2].Substring(0, 1) + n[3];
                }
                else
                {
                    name = "not loged in";
                }

            }
            return name;
        }
    }
}
