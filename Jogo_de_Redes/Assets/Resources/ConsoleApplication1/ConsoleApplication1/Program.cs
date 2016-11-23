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
                Console.WriteLine("SERVER ON");
                __server.StartAcceptTcpClientThread(delegate
                {
                    Console.WriteLine("ALL CLIENTS OK");
                    __server.StartWaitClientsStreamThread(delegate
                    {
                        Console.WriteLine("ALL DATA RECEIVED");
                    });
                });
            }, delegate
            {
                Console.WriteLine("DEU RUIM");
            });
            while (true)
            {
            }
        }
    }
}