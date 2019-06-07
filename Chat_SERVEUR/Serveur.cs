using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Chat_SERVEUR
{
    class Serveur
    {
        TcpListener tcpServeur;
        List<Thread> ListeThread;
        List<Ecoute_Server> ListeClient;
        private bool ServerON;

        public Serveur()
        {
            this.tcpServeur = new TcpListener(IPAddress.Any, 5035);
            ServerON = true;
            ListeClient = new List<Ecoute_Server>();
            ListeThread = new List<Thread>();
        }

        public void Run()
        {
            tcpServeur.Start();
            
            while (ServerON)
            {
                if (tcpServeur.Pending()) //si je vois que quelqu'un veut se co
                {
                    Socket s = tcpServeur.AcceptSocket();
                    Ecoute_Server clnt = new Ecoute_Server(s, ListeClient, "TMP");
                    ListeClient.Add(clnt);
                    Thread th = new Thread(clnt.Ecoute);
                    th.Start();
                } 
            }
        }
    }
}
