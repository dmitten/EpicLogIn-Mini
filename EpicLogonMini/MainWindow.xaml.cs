using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace EpicLogonMini
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Keys[] Ident = new Keys[7] { Keys.D, Keys.M, Keys.I, Keys.T, Keys.T, Keys.E, Keys.N };
        Keys[] Pass = new Keys[9] { Keys.P, Keys.E, Keys.P, Keys.S, Keys.I, Keys.O, Keys.D0, Keys.D0, Keys.P };
     
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;

        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hand = MakeeRecordActive();
            ScreenCapture sc = new ScreenCapture();
            Image img= sc.CaptureScreen();
            System.Windows.Forms.Clipboard.SetImage(img); Find f = new Find();
            f.FindBmp(img);


        }
        private IntPtr MakeeRecordActive()
        {
            List<WindowProcesses> wpl = new List<WindowProcesses>();
            ActiveWindow a = new ActiveWindow();
            wpl = a.GetAllProcesses();
            IntPtr hand = new IntPtr();
            foreach (var item in wpl)
            {
                if (item.caption.Contains("Hyperspace"))
                {
                    hand = item.handlePtr;
                }
            }
            NativeWin32.SetForegroundWindow(hand.ToInt32());
            Thread.Sleep(250);
            return hand;
        }
    }
}
