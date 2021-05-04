using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = "acme.com";
            var uri = "/";
            Int32 port = 80;
            var tcpClient = new TcpClient(host, port);
            var stream = tcpClient.GetStream();
            var request = Encoding.ASCII.GetBytes("GET / HTTP/1.1\r\nHost: acme.com\r\n\r\n");
            stream.Write(request, 0, request.Length);
            var reader = new StreamReader(stream);
            var readToEnd = reader.ReadToEnd();
            Console.WriteLine(readToEnd);
            tcpClient.Close();
            stream.Close();
        }
    }
}