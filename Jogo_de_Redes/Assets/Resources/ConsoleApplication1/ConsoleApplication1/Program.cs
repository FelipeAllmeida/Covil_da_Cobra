using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            FileReader __fileReader = new FileReader();
            SocketController __server = new SocketController();

            SocketInitializationData __socketData = __fileReader.GetSocketInitializationData();

            __server.Initialize(__socketData);
            __server.StartSocket(delegate
            {
                Console.WriteLine("TUDO OK");
                __server.StartAcceptTcpClientThread(delegate
                {
                    Console.WriteLine("Clientes conectados");
                    __server.StartWaitClientsStreamThread(delegate
                    {
                        Console.WriteLine("Todos os dados recebidos");
                        for (int i = 0; i < __server.listClients.Count;i++)
                        {
                            SocketController.ClientData __clientData = __server.listClients[i];
                            __clientData.clientToSendResponse = "VC É UM VIADAO";

                            __server.listClients[__server.listClients.FindIndex(x => x.id == __clientData.id)] = __clientData;
                        }
                        __server.StreamToClients();
                    });
                });
            }, delegate
            {
                Console.WriteLine("DEU RUIN");
            });
            while (true)
            {
            }
        }
    }
}