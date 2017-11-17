using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFWallpaper.Pages
{
    /// <summary>
    /// SettingPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
        public void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if ((bool)chkStartup.IsChecked)
                rk.SetValue("WPFWallpaper", AppDomain.CurrentDomain.BaseDirectory+"WPFWallpaper.exe");
            else
                rk.DeleteValue("WPFWallpaper", false);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetStartup();
        }

        private void chkStartup_Unchecked(object sender, RoutedEventArgs e)
        {
            SetStartup();
        }
    }
}
