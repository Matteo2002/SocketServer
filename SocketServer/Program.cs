using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //endoPoint: coppia Ip + Porta

            //Socket vuole 3 parametri:
            // 1) protocollo a livello network (IPv4)
            // 2) socket type (stream)
            // 3) protocollo di trasporto
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Configurazione del ServerSocket
            //Server programma che sta in ascolto su un EndPoint
            //EndPoint IP + Porta
            IPAddress ipaddr = IPAddress.Any;

            IPEndPoint ipep = new IPEndPoint(ipaddr, 23000);

            //Collegamento tra il listenerSocket e EndPoint
            //Bind: io ho impostato il mio server ad ascoltare su endpoint IP.Any:23000
            listenerSocket.Bind(ipep);

            //Di metterlo effettivamente in ascolto

            listenerSocket.Listen(5);
            Console.WriteLine("Server in ascolto...");
            //Istruzione bloccante
            //Un client si connette
            Socket client = listenerSocket.Accept();
            Console.WriteLine("Client connesso. Client Info:" + client.RemoteEndPoint.ToString());

            //Voglio ricevere un messaggio da parte del client

            //Mi preparo un buffer da 128 byte
            byte[] buff = new byte[128]; //byte array
            int numberByteReceived = 0;

            //Posso ricevere il messaggio
            numberByteReceived = client.Receive(buff);

            //Adesso io ho solo i byte ricevuti
            //Devo tradurli in ASCII
            string mexRicevuto = Encoding.ASCII.GetString(buff, 0, numberByteReceived);
            Console.WriteLine("Il client dice: " + mexRicevuto);

            //Rispondo al client
            string mexDaInviare = "Benvenuto. Mi hai appenna scritto: " + mexRicevuto;

            //Pulisco il mio buffer
            Array.Clear(buff, 0, buff.Length);

            //A questo punto devo tradurre la stringa in byte array
            buff = Encoding.ASCII.GetBytes(mexDaInviare);

            //Invio il messaggio al client
            int sendedBytes = 0;
            sendedBytes = client.Send(buff);
            Console.WriteLine("Risposta inviata. Termino.");
        }
    }
}
