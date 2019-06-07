using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
            TcpClient tcpClnt = new TcpClient();
            IPEndPoint EndPointServer = new IPEndPoint(IPAddress.Loopback, 5035);
            tcpClnt.Connect(EndPointServer);
            NetworkStream stm = tcpClnt.GetStream();
            Thread th = new Thread(ClientListener);
            th.Start(tcpClnt);
        }

        private void Bouton_SeConnecter_Click(object sender, RoutedEventArgs e)
        {
            Bouton_SeConnecter.Visibility = Visibility.Collapsed;
            Bouton_SeDeconnecter.Visibility = Visibility.Visible;
            Connexion c = new Connexion();
            c.Show();
        }

        private void Bouton_SeDeconnecter_Click(object sender, RoutedEventArgs e)
        {
            Bouton_SeDeconnecter.Visibility = Visibility.Collapsed;
            Bouton_SeConnecter.Visibility = Visibility.Visible;
        }

        public void ClientListener(object obj)
        {
            TcpClient tcpclnt = (TcpClient)obj;
            NetworkStream strm = tcpclnt.GetStream();
            byte[] rep = new byte[4096];
            string reponse = "";
            int size = strm.Read(rep, 0, 4096);
            reponse += Encoding.Default.GetString(rep, 0, size);
            User_MessageBox.Text = reponse;
        }
    }
}
