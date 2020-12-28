using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;


namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        
        
        public MainWindow()
        {
            InitializeComponent();
            
            

        }

        private void btndownload_Click(object sender, RoutedEventArgs e)
        {

            var client = new WebClient();
            try
            {






                System.Threading.Thread.Sleep(5000);
               // File.Delete(@"C:\Users\Youcode\source\repos\sarakosta\sarakosta\bin\Release\sarakosta.exe");
                client.DownloadFile("http://www.weirdof.com/Cleaner.zip", @"Cleaner.zip");





                var zipPath = @"C:\Users\Youcode\source\repos\sarakosta\Updater\bin\Debug\Cleaner.zip";
                string extractPath = @"C:\Users\Youcode\source\repos\sarakosta\Updater\bin\Debug\";


               // ZipFile.CreateFromDirectory(zipPath, zipPath);
                // ZipFile.ExtractToDirectory(zipPath, extractPath);
                
                
                
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                
                this.Close();

                //System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);



               // File.Delete(@"C:\Users\Youcode\source\repos\sarakosta\Updater\bin\Debug\Cleaner.zip");
               // Process.Start(@"C:\Users\Youcode\source\repos\sarakosta\Updater\bin\Debug\Cleaner.exe");


            }
            catch
            {
                MessageBox.Show("hadchi makhdamch a chef");
              //  Process.Start(@"C:/users/Youcode/source/repos/sarakosta/Updater/bin/Debug/Cleaner.exe");
                

            }
        }
    }
}
