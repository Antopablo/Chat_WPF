using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Chat_SERVEUR
{
    class Ecoute_Server
    {
        private bool _estConnecte;
        private string _nom;
        Socket s;
        private List<Ecoute_Server> ListeClient;

        public Ecoute_Server(Socket s, List<Ecoute_Server> listeClient, string nom)
        {
            this.s = s;
            EstConnecte = true;
            ListeClient = listeClient;
            Nom = nom;
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }


        public bool EstConnecte
        {
            get { return _estConnecte; }
            set { _estConnecte = value; }
        }

        public void Envoyer(string msg)
        {
            byte[] tmp = Encoding.Default.GetBytes(msg);
            s.Send(tmp);
        }

        public void Ecoute()
        {
            while (EstConnecte)
            {
                if (s.Available > 0)
                {
                    string msg = "";
                    byte[] tmp = new byte[4096];
                    int size = s.Receive(tmp, 4096, SocketFlags.None);
                    msg += Encoding.Default.GetString(tmp, 0, size);

                    if ((msg[0] == '&'))
                    {
                        string[] msgNom = msg.Split(new char[] { '&' });
                        Nom = msgNom[1];
                    }

                    foreach (Ecoute_Server item in ListeClient)
                    {
                        
                            item.Envoyer("\r\n" + (DateTime.Now.ToString("[HH:mm:ss] ") + Nom + " : " + msg));
                        
                    } 
                }
            }
            Thread.Sleep(1);
        }
    }
}
