using System;
using System.Timers;

namespace BattleshipServer
{
  public class ClientAuthenticator
  {
    public event EventHandler<ClientConnectedEventArg> ClientConnected;

    private int elapsedTimeInSeconds;
    private Timer timer;
    private Client clientToValidate;
    private ClientList clients;
    private ClientToValidateState state;

    public ClientAuthenticator(Client clientToValidate, ClientList clients)
    {
      timer = new Timer(Constants.TIMER_INTERVAL_AMOUNT) { AutoReset = true };
      timer.Elapsed += OnTimerElapsed;
      state = ClientToValidateState.NAME_INVALID;
      this.clients = clients;
      this.clientToValidate = clientToValidate;
    }

    public void ValidateClient()
    {
      timer.Start();

      try
      {
        string validationNumber = ClientCommunicationHandler.HandleClientCommunication(clientToValidate, PacketProtocolFactory.GetPacketProtocol(0),
                                                                                      Constants.REQUEST_VALIDATION.ToString(), true).ToString();
        string versionNumber = ClientCommunicationHandler.HandleClientCommunication(clientToValidate, PacketProtocolFactory.GetPacketProtocol(0),
                                                                                    Constants.REQUEST_VERSION.ToString(), true).ToString();
        if (validationNumber != Constants.VALIDATION_NUMBER.ToString() || versionNumber != Constants.VERSION.ToString())
        {
          state = ClientToValidateState.INVALID;
        }

        while (state == ClientToValidateState.NAME_INVALID)
        {
          string playerName = ClientCommunicationHandler.HandleClientCommunication(clientToValidate, PacketProtocolFactory.GetPacketProtocol(0),
                                                                                Constants.REQUEST_LOGIN.ToString(), true).ToString();
          if (CheckIfNameIsAvailable(playerName))
          {
            state = ClientToValidateState.VALID;
          }
        }

      }
      catch (TimeoutException e)
      {
        state = ClientToValidateState.TIMED_OUT;
      }
      catch (Exception e)
      {
        state = ClientToValidateState.INVALID;
      }
      finally
      {
        timer.Close();
      }

      if (state == ClientToValidateState.VALID)
      {
        clients.Add(clientToValidate);
        ClientConnectedEventArg clientConnectedArgs = new ClientConnectedEventArg(clientToValidate);
        OnClientConnected(clientConnectedArgs);
      }
      else if (state == ClientToValidateState.INVALID)
      {
        ClientCommunicationHandler.HandleClientCommunication(clientToValidate, PacketProtocolFactory.GetPacketProtocol(0), Constants.INVALID_CLIENT_TYPE_TEXT, false);
        clientToValidate.Socket.Close();
      }
      else if (state == ClientToValidateState.TIMED_OUT)
      {
        ClientCommunicationHandler.HandleClientCommunication(clientToValidate, PacketProtocolFactory.GetPacketProtocol(0), Constants.REQUEST_CLIENT_TIMED_OUT.ToString(), false);
        clientToValidate.Socket.Close();
      }
    }

    private bool CheckIfNameIsAvailable(string nameToCheck)
    {
      for (int i = 0; i < clients.Count; i++)
      {
        if (clients[i].Name == nameToCheck)
        {
          return false;
        }
      }
      return true;
    }

    private void OnTimerElapsed(object sender, EventArgs arg)
    {
      if (elapsedTimeInSeconds >= Constants.VALIDATION_TIMEOUT_DELAY)
      {
        state = ClientToValidateState.TIMED_OUT;
      }
      else
      {
        elapsedTimeInSeconds++;
      }
    }

    protected virtual void OnClientConnected(ClientConnectedEventArg e)
    {
      ClientConnected.Invoke(this, e);
    }
  }

  public class ClientConnectedEventArg : EventArgs
  {
    public Client Client { get; private set; }

    public ClientConnectedEventArg(Client client)
    {
      Client = client;
    }
  }
}
