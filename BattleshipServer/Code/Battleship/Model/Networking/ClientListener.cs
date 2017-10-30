using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class ClientListener
  {
    public event EventHandler AllClientsConnected;
    public event EventHandler<ClientConnectedEventArg> ClientConnected;

    public bool MustStopListening { get; set; }

    private Socket listenerSocket;
    private IPHostEntry ipHostInfo;
    private IPAddress ipAddress;
    private IPEndPoint localEndPoint;
    private ClientAuthenticator clientAuthenticator;
    private int maxClientAmount;

    public ClientListener(int maxClientAmount)
    {
      listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
      ipAddress = GetLocalIPAddress();
      localEndPoint = new IPEndPoint(ipAddress, Constants.DEFAULT_SERVER_PORT);
      listenerSocket.Bind(localEndPoint);
      listenerSocket.Listen(Constants.BACKLOG_LENGTH);
      this.maxClientAmount = maxClientAmount;
    }

    public void AcceptClients(ClientList clients)
    {
      Task.Run(() =>
      {
        while (clients.Count < maxClientAmount && !MustStopListening)
        {
          Client clientBuffer = new Client(listenerSocket.Accept());
          Task.Run(() =>
          {
            clientAuthenticator = new ClientAuthenticator(clientBuffer, clients);
            clientAuthenticator.ClientConnected += OnClientConnected;
            clientAuthenticator.ValidateClient();
          });
        }

        if (!MustStopListening)
        {
            OnAllClientsConnected(EventArgs.Empty);
        }
      });
    }

    private IPAddress GetLocalIPAddress()
    {
      if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
      {
        throw new Exception();
      }

      ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
      foreach (var ip in ipHostInfo.AddressList)
      {
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
          return ip;
        }
      }
      throw new Exception();
    }

    private void OnClientConnected(object sender, ClientConnectedEventArg e)
    {
      OnClientConnected(e);
    }

    protected virtual void OnAllClientsConnected(EventArgs e)
    {
      AllClientsConnected.Invoke(this, e);
    }

    protected virtual void OnClientConnected(ClientConnectedEventArg e)
    {
      ClientConnected.Invoke(this, e);
    }
  }
}
