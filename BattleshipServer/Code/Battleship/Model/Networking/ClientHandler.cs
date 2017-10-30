using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class ClientHandler
  {
    public event EventHandler AllClientsConnected;
    public event EventHandler<ClientConnectedEventArg> ClientConnected;
    public event EventHandler<ClientDisconnectedEventArg> ClientDisconnected;
    public bool IsGameOver { get { return isGameOver; } set { clientConnectionVerifyer.MustStopVerifying = clientListener.MustStopListening = IsGameOver = value; } }

    private ClientListener clientListener;
    private ClientConnectionVerifyer clientConnectionVerifyer;
    private ClientList clients;
    private bool isGameOver;

    public ClientHandler(int maxClientAmount)
    {
      clients = new ClientList();
      clientConnectionVerifyer = new ClientConnectionVerifyer(clients);
      clientListener = new ClientListener(maxClientAmount);
      clientListener.AllClientsConnected += OnAllClientsConnected;
      clientListener.ClientConnected += new EventHandler<ClientConnectedEventArg>(OnClientConnected);
    }

    public void AcceptClients()
    {
      clientListener.AcceptClients(clients);
      //clientConnectionVerifyer.CheckIfClientsAreStillConnected();
    }

    

    #region Events

    private void OnAllClientsConnected(object sender, EventArgs e)
    {
      OnAllClientsConnected(e);
    }

    private void OnClientConnected(object sender, ClientConnectedEventArg e)
    {
      OnClientConnected(e);
    }

    private void OnClientDisconnected(object sender, ClientDisconnectedEventArg e)
    {
      OnClientDisconnected(e);
    }


    protected virtual void OnAllClientsConnected(EventArgs e)
    {
      AllClientsConnected.Invoke(this, e);
    }

    protected virtual void OnClientConnected(ClientConnectedEventArg e)
    {
      ClientConnected.Invoke(this, e);
    }

    protected virtual void OnClientDisconnected(ClientDisconnectedEventArg e)
    {
      ClientDisconnected.Invoke(this, e);
    }

    #endregion
  }
}
