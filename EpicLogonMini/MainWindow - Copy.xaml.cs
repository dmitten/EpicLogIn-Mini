using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            watch();
        }

        private void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            logonfromfilewatcher();
        }
      
        [STAThread]
        private void logonfromfilewatcher()
        {
            IntPtr hand = MakeeRecordActive();
            ScreenCapture sc = new ScreenCapture();
            Image img = sc.CaptureScreen();
            System.Windows.Forms.Clipboard.SetImage(img); Find f = new Find();
            f.FindBmp(img);
            Thread.Sleep(20);
            MySendKeys m = new MySendKeys();
            m.PressKeyArray(Ident);
            Thread.Sleep(10);
            m.PressKey(Keys.Tab);
            Thread.Sleep(100);
            m.PressKeyArrayPassword(Pass);
            Thread.Sleep(10);
            //m.AltO();
            //Thread.Sleep(200);
            //m.AltO();

            if (System.Windows.Forms.Application.MessageLoop)//http://stackoverflow.com/questions/554408/why-would-application-exit-fail-to-work
            {
                // Use this since we are a WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Use this since we are a console app
                System.Environment.Exit(1);
            }
        }
        private void watch()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "C:\\Users\\djmit_000\\AppData\\Local\\Packages\\2ac7d836-d159-47dc-90c5-0f42f5eb793a_4j5t8z38t883m\\LocalState";

            //watcher.Path = "C:\\Users\\dmitten\\AppData\\Local\\Packages\\48304DaveMitten.MittenLog_prf20yrc9cbyp\\LocalState";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;// kill on event
        }
        [STAThread]
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            // need to get back yo ui thread
            Dispatcher.BeginInvoke(new MethodInvoker(delegate
            {
                logonfromfilewatcher();
               
            }));
            var watcher = sender as FileSystemWatcher;
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
            }
          
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
