using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class ClientConnectionVerifyer
  {
    public event EventHandler<ClientDisconnectedEventArg> ClientDisconnected;
    public bool MustStopVerifying { get; set; }

    private ClientList clients;

    public ClientConnectionVerifyer(ClientList clients)
    {
      this.clients = clients;
    }

    public void CheckIfClientsAreStillConnected()
    {
      Task.Run(() =>
      {
        while (!MustStopVerifying)
        {
          for (int i = 0; i < clients.Count; i++)
          {
            if (!IsConnected(clients[i].Socket))
            {
                ClientDisconnectedEventArg clientDisconnectedArgs = new ClientDisconnectedEventArg(clients.Get(i));
                OnClientDisconnected(clientDisconnectedArgs);
                clients.RemoveAt(i);
            }
          }
        }
      });
    }

    private bool IsConnected(Socket socket)
    {
      try
      {
        return !((socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0) || !socket.Connected);
      }
      catch (SocketException) { return false; }
    }

    protected virtual void OnClientDisconnected(ClientDisconnectedEventArg e)
    {
      ClientDisconnected.Invoke(this, e);
    }
  }

  public class ClientDisconnectedEventArg : EventArgs
  {
    public Client Client { get; private set; }

    public ClientDisconnectedEventArg(Client client)
    {
      Client = client;
    }
  }
}
