using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Chat_WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BackgroundWorker worker;
        public TcpClient tcpClnt;

        public MainWindow()
        {
            InitializeComponent();
          

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Fenetre_Chat_General_FRAME.Content += e.UserState.ToString();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            NetworkStream strm = tcpClnt.GetStream();
            int SizeBuffer = 4096;

            while (true)
            {
                byte[] rep = new byte[SizeBuffer];
                if (strm.DataAvailable)
                {

                    string reponse = "";
                    int size = strm.Read(rep, 0, SizeBuffer);
                    reponse = Encoding.Default.GetString(rep, 0, size);
                    (sender as BackgroundWorker).ReportProgress(0, reponse);
                }
            }
        }

        private void Bouton_SeConnecter_Click(object sender, RoutedEventArgs e)
        {
            Bouton_SeConnecter.Visibility = Visibility.Collapsed;
            Bouton_SeDeconnecter.Visibility = Visibility.Visible;
            Connexion c = new Connexion(this);
            c.Show();
        }

        private void Bouton_SeDeconnecter_Click(object sender, RoutedEventArgs e)
        {
            Bouton_SeDeconnecter.Visibility = Visibility.Collapsed;
            Bouton_SeConnecter.Visibility = Visibility.Visible;
        }


        private void Bouton_SeConnecter_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Fenetre_Chat_General_FRAME_Loaded(object sender, RoutedEventArgs e)
        {
            ((Chat_General)Fenetre_Chat_General_FRAME.Content).mw = this;
        }

        private void Bouton_EnvoyerMessage_Click(object sender, RoutedEventArgs e)
        {
            NetworkStream stm = tcpClnt.GetStream();
            byte[] EnvoiPseudo = Encoding.Default.GetBytes(User_MessageBox.Text);
            stm.Write(EnvoiPseudo, 0, EnvoiPseudo.Length);
            User_MessageBox.Text = "";
        }
    }
}
