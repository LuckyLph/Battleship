using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

using SFML;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Battleship
{
  public class Battleship
  {
    #region Variables

    #region Objects

    ConnectionSocket socket;                                              //Objet de type ConnectionSocket
    Resources resources;                                                  //Objet de type Resources pour accéder au resources
    WindowState windowState = WindowState.SplashScreen;                   //État de la fenêtre de rendu
    Splashscreen splashscreen;                                            //Objet Splashscreen pour gérer le splashscreen
    Login loginscreen;                                                    //Objet de Login pour gérer le loginscreen
    Game game;                                                            //Objet de Game pour gèrer la partie
    Endscreen endscreen;                                                  //Objet de Endscreen pour gèrer l'écran de fin de partie

    Time gameTime = new Time();                                           //Objet Time pour compter les fps

    #endregion

    #region SFML

    RenderWindow window;                                                  //Objet de type RenderWindow pour la fenêtre de rendu
    Color backgroundColor;                                                //Couleur de fond de la fenêtre de rendu
    Clock clock;                                                          //Clock pour compter les fps

    #endregion

    #region Base types

    bool gameMustEnd = false;                                             //Booléen pour déterminer si la partie doit terminer
    bool gameMustRestart = false;                                         //Booléen pour déterminer si la partie doit recommencer

    int fps = 0;                                                          //Nombre de fps par update
    float timeElapsed = 0;                                                //Temps depuis la dernière mise à jour

    #endregion

    #endregion

    /// <summary>
    /// Constructeur de la classe Game, initialise la fenêtre de rendu
    /// </summary>
    /// <param name="window">Objet de type RenderWindow qui sert de fenêtre de rendu</param>
    public Battleship(RenderWindow window)
    {
      this.window = window;
      this.window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
      this.window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMousePressed);
      this.window.MouseWheelMoved += new EventHandler<MouseWheelEventArgs>(OnMouseWheelMoved);
      this.window.SetFramerateLimit(Constants.FRAME_LIMIT);
    }

    /// <summary>
    /// Boucle de jeu de la partie, appele les fonctions clés au déroulement de la partie
    /// </summary>
    /// <returns>Booléen indiquant si la partie doit être recomencée</returns>
    public bool Run()
    {
      window.SetActive();
      clock.Restart();
      while (window.IsOpen && !gameMustEnd)
      {
        #region FPS counter

        gameTime = clock.Restart();
        timeElapsed += gameTime.AsSeconds();
        //Si une seconde s'est écoulée dans la partie
        if (timeElapsed > 1)
        {
          Console.WriteLine("FPS: {0}", fps);
          fps = 0;
          timeElapsed = 0;
        }
        fps++;

        #endregion
        window.Clear(backgroundColor);
        window.DispatchEvents();
        Update();
        Draw();
        window.Display();
      }
      return gameMustRestart;
    }

    #region Update/Draw

    /// <summary>
    /// Gère la mise à jour de l'application
    /// </summary>
    public void Update()
    {
      switch (windowState)
      {
        case WindowState.SplashScreen:
          splashscreen.Update(gameTime);
          break;
        case WindowState.Login:
          loginscreen.Update();
          break;
        case WindowState.Ending:
          endscreen.Update(game.HasWon);
          break;
        default:
          if (game.Update())
            windowState = WindowState.Ending;
          break;
      }
    }

    /// <summary>
    /// Gère l'affichage à l'écran de l'application
    /// </summary>
    private void Draw()
    {
      switch (windowState)
      {
        case WindowState.SplashScreen:
          splashscreen.Draw();
          break;
        case WindowState.Login:
          loginscreen.Draw();
          break;
        case WindowState.Ending:
          break;
        default:
          game.Draw();
          break;
      }
    }

    #endregion

    #region Game Management

    /// <summary>
    /// Met fin à la partie
    /// </summary>
    public void EndGame(bool mustRestart)
    {
      if (socket != null)
        socket.DisconnectSocket();
      gameMustEnd = true;
      gameMustRestart = mustRestart;
      game.EndGame();
    }

    /// <summary>
    /// Réinitialise la partie
    /// </summary>
    public void ResetGame()
    {
      gameMustEnd = false;
      windowState = WindowState.SplashScreen;
      splashscreen.ResetSplashscreen();
      loginscreen.ResetLogin();
      game.ResetGame();
    }

    /// <summary>
    /// Charge tous les objets à risques d'exceptions en mémoire
    /// </summary>
    public void InitializeBattleship()
    {
      resources = new Resources();
      splashscreen = new Splashscreen();
      loginscreen = new Login();
      game = new Game();
      endscreen = new Endscreen();
      clock = new Clock();

      resources.InitializeResources();
      splashscreen.InitializeSplashscreen(resources, window);
      loginscreen.InitializeLogin(resources, window);
      game.InitializeGame(resources, window, socket);
      endscreen.InitializeEndscreen(resources, window);

      backgroundColor = Color.Black;
    }

    #endregion

    #region Networking

    /// <summary>
    /// Initialise la connection au serveur
    /// </summary>
    public void InitializeSocket()
    {
      string serverInfo = loginscreen.ServerInfo;
      string[] serverInfoArr = serverInfo.Split(',');
      int serverPort = 0;
      int.TryParse(serverInfoArr[1], out serverPort);

      socket = new ConnectionSocket();
      if (!socket.InitializeSocket(serverInfoArr[0], serverPort, serverInfoArr[2], serverInfoArr[3]))
        Program.HandleException(Constants.SERVER_ERROR_MESSAGE, Constants.SERVER_ERROR_TITLE);
      else
        windowState = WindowState.Playing;
    }

    #endregion

    #region Events

    /// <summary>
    /// Gère les frappes de touche de clavier
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnKeyPressed(object sender, KeyEventArgs e)
    {
      #region Login

      //Gestion des entrées du login
      if (windowState == WindowState.Login)
      {
        if (e.Code == Keyboard.Key.Down || e.Code == Keyboard.Key.Up || e.Code == Keyboard.Key.Tab)
        {
          loginscreen.HandleLoginTextSelection(e.Code);
        }
        else if (e.Code == Keyboard.Key.Return)
        {
          //InitializeSocket();
          windowState = WindowState.Playing;
        }
        else if (e.Code == Keyboard.Key.Escape)
        {
          EndGame(true);
        }

        //Gestion de la première frappe
        loginscreen.HandleLoginFirstType(e.Code);
        //Gestion des entrées de texte
        loginscreen.HandleLoginKeyboardInput(e.Code);
      }

      #endregion

      #region SplashScreen

      //Gestion des entrées pour le splashscreen
      else if (windowState == WindowState.SplashScreen)
      {
        if (e.Code == Keyboard.Key.Return)
        {
          splashscreen.StopDataSource();
          windowState = WindowState.Login;
        }

        if (e.Code == Keyboard.Key.Escape)
        {
          splashscreen.StopDataSource();
          EndGame(false);
        }
      }

      #endregion

      #region Playing

      //Gestion des entrées du jeu
      else if (windowState == WindowState.Playing)
      {
        if (e.Code == Keyboard.Key.Escape)
        {
          EndGame(true);
        }
        else
        {
          game.OnKeyPressed(e.Code);
        }
      }

      #endregion

      #region Ending

      //Gestion des entrées de l'écran de fin
      else if (windowState == WindowState.Ending)
      {
        if (e.Code == Keyboard.Key.Escape || e.Code == Keyboard.Key.Return)
        {
          EndGame(true);
        }
      }

      #endregion
    }

    /// <summary>
    /// Gère le click de souris
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMousePressed(object sender, MouseButtonEventArgs e)
    {
      #region Playing

      if (windowState == WindowState.Playing)
      {
        game.OnMousePressed(e);
      }
      #endregion
    }

    /// <summary>
    /// Gère l'entrée de la roulette de souris
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMouseWheelMoved(object sender, MouseWheelEventArgs e)
    {
      #region Playing

      if (windowState == WindowState.Playing)
      {
        game.OnMouseWheelMoved(e);
      }

      #endregion
    }

    #endregion
  }
}
