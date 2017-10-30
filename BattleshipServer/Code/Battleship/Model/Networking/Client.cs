using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class Client
  {
    public IPAddress Address { get; private set; }
    public int Port { get; private set; }
    public String Name { get; private set; }
    public Socket Socket { get; private set; }

    public Client(Socket clientSocket)
    {
      IPEndPoint ipEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
      Address = ipEndPoint.Address;
      Port = ipEndPoint.Port;
      Socket = clientSocket;
    }
  }
}
