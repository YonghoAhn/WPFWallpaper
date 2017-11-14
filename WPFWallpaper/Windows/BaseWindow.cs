using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Threading;
using WPFWallpaper.Common;
using static WPFWallpaper.Common.WinApi;

namespace WPFWallpaper.Windows
{
    public class BaseWindow : Window
    {
        #region BasicEventHandler
        public BaseWindow()
        {
            ScreenOwnerIndex = 0;
            PinToBackground();
            timer_check.Tick += Timer_check_Tick;
            Loaded += BaseWindow_Loaded;
            Closing += BaseWindow_Closing;
        }
        public BaseWindow(int startOwnerScreenIndex)
        {
            ScreenOwnerIndex = startOwnerScreenIndex;
            PinToBackground();
            timer_check.Tick += Timer_check_Tick;
            Loaded += BaseWindow_Loaded;
            Closing += BaseWindow_Closing;
        }

        private void BaseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;


            timer_check.Stop();


            if (checkParent != null)
            {
                isRunning = false;
                waitHandle.Set();
                checkParent.Wait(TimeSpan.FromSeconds(10.0));
                checkParent = null;

                waitHandle.Dispose();
            }
            
        }

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            ScreenUtility.Initialize();
            if (PinToBackground())
            {
                waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
                isRunning = true;
                checkParent = Task.Factory.StartNew(CheckParent, new WindowInteropHelper(this).Handle);


                timer_check.Start();
            }
            else
            {
                Close();
            }
        }

        private void Timer_check_Tick(object sender, EventArgs e)
        {
            
            lock (lockFlag)
            {
            //    //needUpdate = base.needUpdate;
                needUpdate = false;
            }

            if (needUpdate)
            {
                PinToBackground();
            }
            
        }
#endregion
        #region Pin_Background
        public DispatcherTimer timer_check = new DispatcherTimer();

        public bool isFixed = false;
        public bool IsFixed
        { get { return isFixed; } }
        //################################################//
        //################################################//
        /// <summary>
        /// 현재 모니터
        /// </summary>
        public MONITORINFO ScreenOwner
        {
            get
            {
                if (ScreenOwnerIndex < ScreenUtility.Screens.Length)
                    return ScreenUtility.Screens[ScreenOwnerIndex];
                return new MONITORINFO()
                {
                    rcMonitor = Screen.PrimaryScreen.Bounds,
                    rcWork = Screen.PrimaryScreen.WorkingArea,
                };
            }
        }
        //################################################//
        public int ownerScreenIndex = 0;
        /// <summary>
        /// 스크린 어디있는지 인덱스
        /// </summary>
        public int ScreenOwnerIndex
        {
            get { return ownerScreenIndex; }
            set
            {
                if (value < 0) value = 0;
                else if (value >= Screen.AllScreens.Length) value = 0;
                if (ownerScreenIndex != value)
                {
                    ownerScreenIndex = value;
                    PinToBackground();
                }
            }
        }

        /// <summary>
        /// 다음 화면으로 넘깁니다. Screen Index를 초과하면, 0번 화면으로 돌아갑니다.
        /// </summary>
        public void NextScreen()
        {
            ScreenOwnerIndex++;
        }

        /// <summary>
        /// 이전 화면으로 넘깁니다. Screen Index를 초과하면, 0번 화면으로 돌아갑니다.
        /// </summary>
        public void BeforeScreeen()
        {
            ScreenOwnerIndex--;
        }

        //################################################//
        public Task checkParent = null;
        public bool isRunning = false;
        public EventWaitHandle waitHandle = null;
        //################################################//
        public readonly object lockFlag = new object();
        public bool needUpdate = false;

        /// <summary>
        /// 바탕화면에 고정합니다.
        /// </summary>
        /// <returns>고정 완료되었는지 확인</returns>
        protected bool PinToBackground()
        {
            isFixed = DrawDesktpBackgroundSupporter.FixBehindDesktopIcon(new WindowInteropHelper(this).Handle);
            if (isFixed) ScreenUtility.FillScreen(this, ScreenOwner);
            return isFixed;
        }

        /// <summary>
        /// Parent 프로세스를 찾아온다.
        /// </summary>
        /// <param name="thisHandle">hWnd 넘겨받기</param>
        protected void CheckParent(object thisHandle)
        {
            IntPtr me = (IntPtr)thisHandle;
            while (isRunning)
            {
                bool isChildOfProgman = false;
                var progman = WinApi.FindWindow("Progman", null);
                WinApi.EnumChildWindows(progman, new WinApi.EnumWindowsProc((handle, lparam) =>
                {
                    if (handle == me)
                    {
                        isChildOfProgman = true;
                        return false;
                    }
                    return true;
                }), IntPtr.Zero);
                if (isChildOfProgman == false)
                {
                    lock (lockFlag)
                    {
                        needUpdate = true;
                    }
                }
                waitHandle.WaitOne(2000);
            }
        }
        public void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            lock (lockFlag)
            {
                needUpdate = true;
            }
        }
        #endregion
        #region General_Settings
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        public int Volume { get { return volume; } set { ChangeVolume((int)value); } }
        private int volume { get; set; }
        private void ChangeVolume(int value)
        {
            int NewVolume = ((ushort.MaxValue / 10) * value);
            // Set the same volume for both the left and the right channels
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            // Set the volume
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }
        #endregion
    }
}
