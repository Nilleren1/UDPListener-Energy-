using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using EngergyLib;

namespace UDPListener_Energy_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UDP Broadcast Reciever: ");



            using (UdpClient socket = new UdpClient())
            {
                socket.Client.Bind(new IPEndPoint(IPAddress.Any, 23001));

                IPEndPoint clientEndPoint = null;

                while (true)
                {
                    byte[] data = socket.Receive(ref clientEndPoint);

                    string message = Encoding.UTF8.GetString(data);

                    string[] values = message.Split(' ');

                    double result = 0;
                    if (values.Length > 1)
                    {
                        double value = Convert.ToDouble(values[1]);
                        

                        if (values[0].ToLower() == "joule")
                        {
                            result = EnergyConverter.ToCalorie(value);
                        }
                        else if (values[0].ToLower() == "calorie")
                        {
                            result = EnergyConverter.ToJoule(value);
                        }
                        else
                        {
                            Console.WriteLine("You have to write something like this: joule 32");
                        }
                    }

                    string m = "Result is " + result;
                    byte[] reply = Encoding.UTF8.GetBytes(m);
                    socket.Send(reply, reply.Length, clientEndPoint);

                    Console.WriteLine("Client sent: " + message);

                    
                }


            }
        }
    }
}
