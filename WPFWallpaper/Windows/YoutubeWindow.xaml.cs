using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPFWallpaper.Common;
using static WPFWallpaper.Common.WinApi;

namespace WPFWallpaper.Windows
{
    /// <summary>
    /// YoutubeWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class YoutubeWindow : BaseWindow
    {
        
        public YoutubeWindow(int ownerScreenIndex = 0)
        {
            InitializeComponent();
            WebBrowser1.Navigate("https://www.whatismybrowser.com/");
        }
    }
}
