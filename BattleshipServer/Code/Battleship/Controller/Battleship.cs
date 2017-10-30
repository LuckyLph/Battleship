using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class Battleship
  {
    #region Variables

    private ClientHandler clientHandler;
    private Game game;
    private ConsoleView consoleView;
    private ServerState serverState;

    private bool serverMustRestart;
    private bool serverMustShutdown;
    private int maxClientAmount;

    #endregion

    /// <summary>
    /// Constructeur de la classe Battleship
    /// </summary>
    public Battleship()
    {
      serverState = ServerState.STARTING;
      serverMustRestart = false;
      serverMustShutdown = false;
      consoleView = new ConsoleView();
      string userInput;
      do
      {
        consoleView.ClearConsole();
        consoleView.WriteToConsole(Constants.ASK_PLAYER_AMOUNT_TEXT, IOType.SAME_LINE);
        userInput = consoleView.ReadFromConsole(IOType.NEXT_LINE);
        int.TryParse(userInput, out maxClientAmount);
      }
      while (maxClientAmount < Constants.MINIMUM_PLAYER_AMOUNT || maxClientAmount > Constants.MAXIMUM_PLAYER_AMOUNT);

      clientHandler = new ClientHandler(maxClientAmount);
      game = new Game(maxClientAmount);
      clientHandler.AllClientsConnected += OnAllClientsConnected;
      clientHandler.ClientConnected += new EventHandler<ClientConnectedEventArg>(OnClientConnected);
      clientHandler.ClientDisconnected += new EventHandler<ClientDisconnectedEventArg>(OnClientDisconnected);
    }

    /// <summary>
    /// Met en marche le serveur
    /// </summary>
    /// <returns></returns>
    public bool Run()
    {
      ResetBattleship();
      while (!serverMustShutdown)
      {
        if (serverState == ServerState.STARTING_GAME)
        {
          game.Play();
        }
      }
      
      return serverMustRestart;
    }

    /// <summary>
    /// Réinitialise le serveur
    /// </summary>
    public void ResetBattleship()
    {
      consoleView.WriteToConsole(Constants.NOTIFY_SERVER_READY_TEXT, IOType.SAME_LINE);
      clientHandler.AcceptClients();
      serverState = ServerState.WAITING_ON_CLIENTS;
    }



    #region Events

    private void OnAllClientsConnected(object sender, EventArgs e)
    {
      serverState = ServerState.STARTING_GAME;
      consoleView.WriteToConsole(Constants.ALL_CLIENTS_CONNECTED_TEXT, IOType.NEXT_LINE);
    }

    private void OnClientConnected(object sender, ClientConnectedEventArg e)
    {
      consoleView.WriteToConsole(Constants.CLIENT_CONNECTED_TEXT + e.Client.Address.ToString(), IOType.NEXT_LINE);
    }

    private void OnClientDisconnected(object sender, ClientDisconnectedEventArg e)
    {
      consoleView.WriteToConsole(Constants.CLIENT_DISCONNECTED_TEXT + e.Client.Address.ToString(), IOType.NEXT_LINE);
    }

    #endregion
  }
}
