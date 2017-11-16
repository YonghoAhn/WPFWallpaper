using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstallWallpaper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        public MainWindow()
        {
            InitializeComponent();
            PathText.Text = folder;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = folder;
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folder = fbd.SelectedPath;
                    PathText.Text = folder;
                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (folder != null && folder != "")
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", "WPFWallpaper.exe", 11000);
                if (Environment.Is64BitOperatingSystem)
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", "WPFWallpaper.exe", 11000);

                //Make Dir Uri from Appliction Startup Path and Dir name  
                DirectoryInfo di = new DirectoryInfo(folder + @"\Wallpaper");  //Create Directoryinfo value by sDirPath  

                if (di.Exists == false)   //If New Folder not exits  
                {
                    di.Create();             //create Folder  
                }
                else
                {
                    di.Delete(true);
                    di.Create();
                }
                ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "Install.zip", folder);
            }
        }


    }
}
