// See https://aka.ms/new-console-template for more informat
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using ConsoleApp2.Help;
using System.Linq;
using static System.Console;
using System.Text.Json;

AsyncSocketListener socket = new AsyncSocketListener();

public struct StateObject
{
    public StateObject() { }

    public const int bufferSize = 1024;

    public byte[] buffer = new byte[bufferSize];

    // Received data string.
    public StringBuilder sb = new StringBuilder();

    public Socket listener = null;

}




class AsyncSocketListener
{
    public ICollection<Socket> connections = new List<Socket> { };
    public Help help                       = new Help();
    
  

    internal AsyncSocketListener() {

        StartListening();
    }

    private static ManualResetEvent allDone = new ManualResetEvent(false);

   

    private void ReadCallback(IAsyncResult ar) 
    {
        StateObject state = (StateObject)ar.AsyncState;

        try
        {

            

            int         bytesRead     = state.listener.EndReceive(ar); // Got message from client....


            if (bytesRead > 0){
                

        
        

                var message = help.Frombyte(state.buffer, 0, bytesRead);




                // logic for broadcast ....
        
                foreach(var each in connections) 
                {
                    if (each != state.listener) 
                    {
                        each.Send(help.Fromstring(message));
                        
                    }



                }



                state.listener.BeginReceive(state.buffer,
                    0,
                    StateObject.bufferSize,
                    0,
                    new AsyncCallback(ReadCallback), state);    
            }
            
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            connections.Remove(state.listener);
            Console.WriteLine("listener has been disconnected .");
        }

      


    }
    private void AcceptCallback(IAsyncResult ar)
    {
        allDone.Set();
        Console.WriteLine("New client connected ... ");

        Socket listener = (Socket)ar.AsyncState, handler = listener!.EndAccept(ar);

        
        
            

       StateObject state = new StateObject();

       state.listener = handler;


       connections.Add(state.listener);   
            
        handler.BeginReceive(

                state.buffer,
                0,
                StateObject.bufferSize,
                0,
                new AsyncCallback(ReadCallback),
                state

                );
        


        

    }

    private void StartListening()
    {
        Console.WriteLine("[INFO] : progress .... ");
        const string ip = "127.0.0.1";
        const int port = 8000;

        IPEndPoint IPendpoint = new IPEndPoint(IPAddress.Parse(ip), port);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        

        try
        {
            listener.Bind(IPendpoint);
            listener.Listen(100);

            while (true)
            {
                try
                {

                    allDone.Reset();

                    WriteLine("Waiting for connection .....");

                    // quick logic...
                    
                    

                    listener.BeginAccept(
                            new AsyncCallback(AcceptCallback),
                            listener);

                    allDone.WaitOne();
                }
                catch(Exception ex) 
                {
                    WriteLine("Exception heheheh ");
                }
            }
        }

        catch(Exception e)
        {
            Console.WriteLine(e);
        }

    }

}


















































































/*const string ip = "127.0.0.1";

TcpListener listener = new TcpListener(IPAddress.Parse(ip), 8000);
listener.Start();
Console.WriteLine("UP");

while (true)
{

    Console.WriteLine("Waiting for connection!...");
    TcpClient client = listener.AcceptTcpClient();
    Console.WriteLine("Client accepted.");

    NetworkStream stream = client.GetStream();
    StreamReader reader = new StreamReader(client.GetStream());
    StreamWriter writer = new StreamWriter(client.GetStream());

    byte[] buffer = new byte[1024];
    stream.Read(buffer, 0, buffer.Length);
    int recv = 0;

    foreach (byte b in buffer) {
        if (b != 0)
        {
            recv++;
        }
    }

    string req = Encoding.UTF8.GetString(buffer, 0, recv);
    Console.WriteLine(req);
    writer.WriteLine(req);
    writer.Flush();
    
    

}

*/



































/*using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

const string ip = "127.0.0.1";
const int port = 8000;


var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);



tcpSocket.Bind(tcpEndPoint);
Console.WriteLine("CHECK POINT BEFORE LISTEN METHOD !....");
tcpSocket.Listen(10);
Console.WriteLine("CHECK POINT AFTER LISTEN METHOD !....");



while (true)
{

    var buff = new byte[256];
    var size = 0;
    var builder = new StringBuilder();

    var listener = tcpSocket.Accept();
    
    Console.WriteLine("Message from client sent.....");


    do
    {


        size = listener.Receive(buff);
        builder.Append(Encoding.Unicode.GetString(buff, 0, size)); // got data -- message



    } while (listener.Available > 0);

    Console.WriteLine(builder.ToString());
    listener.Send(Encoding.Unicode.GetBytes(builder.ToString()));
    listener.Shutdown(SocketShutdown.Both);
    listener.Close();

}*/


