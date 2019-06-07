using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chat_WPF
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        private MainWindow mw;
        public Connexion()
        {
            InitializeComponent();
        }

        private void Valider_Connexion_Click(object sender, RoutedEventArgs e)
        {
            // envoyer son pseudo au serveur
            TcpClient tcpClnt = new TcpClient();
            IPEndPoint EndPointServer = new IPEndPoint(IPAddress.Loopback, 5035);
            tcpClnt.Connect(EndPointServer);
            NetworkStream stm = tcpClnt.GetStream();
            byte[] EnvoiPseudo = Encoding.Default.GetBytes("&&"+Champ_Pseudo.Text);
            stm.Write(EnvoiPseudo, 0, EnvoiPseudo.Length);

            this.Close();

        }
    }
}
