using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Net;
using System.Diagnostics;

namespace sarakosta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            richtextbox1.Visibility = Visibility.Hidden;

            //method to Display Pc Specifications
            pcname.Text = PCinformations();
            
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
          
     
        }

        // method get Pc specs
        public string PCinformations()
        {
            string q1 = Environment.MachineName;
            string q2 = Environment.UserName;
            string q3 = Environment.OSVersion.Platform.ToString(); ;

            int q5 = Environment.SystemPageSize;
            string info = "Machine Name : " + q1 + " \n" + "PC- Username :  " + q2 + " \n" + "Opearation System : " + q3 + " \n" + "Operation System Size : " + q5;
            return info;
        }

        private void btnhistory_Click(object sender, RoutedEventArgs e)
        {

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Youcode\source\repos\sarakosta\history2.txt");

            

            MessageBox.Show(string.Join("\n", lines));









        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tmpPath = System.IO.Path.GetTempPath();
            var files = Directory.GetFiles(tmpPath, "*.*", SearchOption.AllDirectories);


            foreach (var file in files)
            {


                try
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine(a.Message);

                    string path = @"C:\Users\Youcode\source\repos\sarakosta\history2.txt";


                    using (StreamWriter sw = File.CreateText(path))
                    {

                        sw.WriteLine(a);
                       
                    }



                }

            }


        }

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

       
       
       

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var tmpPath = System.IO.Path.GetTempPath();
            var files = Directory.GetFiles(tmpPath, "*.*", SearchOption.AllDirectories);

         
            var Listbox = new List<string>();
            Listbox.AddRange(files);

            for (int i = 0; i < files.Length; i++)
            {
                backgroundWorker1.ReportProgress(Convert.ToInt32(CalcPer(i, files.Length)));
                

                Dispatcher.Invoke(new Action(() => { richtextbox1.Text += files[i]; }), DispatcherPriority.ContextIdle);

                if (backgroundWorker1.CancellationPending)
                {

                    this.Dispatcher.Invoke(() =>
                    {
                        
                        btnanalyse1.IsEnabled = true;
                        btnhistory1.IsEnabled = true;
                        btnupdate.IsEnabled = true;
                        
                    });
                    e.Cancel = true;
                    break;
                    
                }



            }

           



        }

        //progress bar thingy & percentage
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbar1.Value = e.ProgressPercentage;
            prclabel.Text = e.ProgressPercentage.ToString() + "%";
        }


        // backgournd worker completed

        //private void backgroundWorker1_RunWorkerCompleted(object sender, ProgressChangedEventArgs e)
        //{
        //    btnDone.Content = "Done";
        //}

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // return value   from 


            if (e.Cancelled)
            {
                btnDone.Content = "Stopped";
                
            }
            else
            {
                btnDone.Content = "Completed";
                btnanalyse1.IsEnabled = true;
                btnhistory1.IsEnabled = true;
            }

        }
        
        //calculate percentage
        private double CalcPer(double newNum, double originalNum)
        {
            var increase = originalNum - newNum;
            var incPer = increase / originalNum * 100;
            return 100 - incPer;
        }

       
        private void analyse_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
               
               
            }

            else
            {
                backgroundWorker1.CancelAsync();
                
                Scan_text.Text = "Operation has been stopped";
            }

            Scan_text.Text = "Test has been started :)";

            btnanalyse.Visibility = Visibility.Hidden;
            btnupdate.Visibility = Visibility.Hidden;
            btnhistory.Visibility = Visibility.Hidden;
            progressbar1.Visibility = Visibility.Visible;
            richtextbox1.Visibility = Visibility.Visible;
            btnDone.Visibility = Visibility.Visible;
            prclabel.Visibility = Visibility.Visible;
            btnanalyse1.IsEnabled = false;
            btnhistory1.IsEnabled = false;
            pcname.Visibility = Visibility.Hidden;
            update_button.IsEnabled = false;


        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void pbStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void btndashboard_Click(object sender, RoutedEventArgs e)
        {
            btnanalyse.Visibility = Visibility.Visible;
            btnupdate.Visibility = Visibility.Visible;
            btnhistory.Visibility = Visibility.Visible;
            progressbar1.Visibility = Visibility.Hidden;
            richtextbox1.Visibility = Visibility.Hidden;
            btnDone.Visibility = Visibility.Hidden;
            prclabel.Visibility = Visibility.Hidden;
            btnanalyse1.IsEnabled = true;
            btnhistory1.IsEnabled = true;
            pcname.Visibility = Visibility.Visible;
            update_button.IsEnabled = true;

        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            btnanalyse.Visibility = Visibility.Visible;
            btnupdate.Visibility = Visibility.Visible;
            btnhistory.Visibility = Visibility.Visible;
            progressbar1.Visibility = Visibility.Hidden;
            richtextbox1.Visibility = Visibility.Hidden;
            btnDone.Visibility = Visibility.Hidden;
            prclabel.Visibility = Visibility.Hidden;
            btnanalyse1.IsEnabled = true;
            btnhistory1.IsEnabled = true;
            update_button.IsEnabled = true;

        }

       

       

        


        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            try
            {

                if (!webClient.DownloadString("http://www.weirdof.com/version.txt").Contains("1.1.1"))
                {
                    if (MessageBox.Show("there is an update! available Do you want to download it?", "Cleaner",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        using (var client = new WebClient())
                        {
                            Process.Start(@".\Updater.exe");
                            Close();
                        }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


    }
    
    
    
}