using System;

using SFML.Graphics;
using SFML.Window;

namespace Battleship
{
  class Program
  {
    static void Main()
    {
      //Ajoute une variable environnement pour permettre à l'assembleur de connaitre le chemin d'accès au dlls du projet
      string dllPath = System.IO.Directory.GetCurrentDirectory() + @"\dll";
      Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + dllPath);

      //Objet de type RenderWindow pour la fenêtre de rendu
      RenderWindow window = new RenderWindow(new VideoMode(Constants.WINDOW_WIDTH, Constants.WINDOW_HEIGHT), Constants.GAME_NAME, Styles.Titlebar);
      //Objet Game qui contient notre jeu
      Battleship battleship = new Battleship(window);

      battleship.InitializeBattleship();

      //Initialise la partie en s'assurant de gérer les exceptions
      //try { battleship.InitializeBattleship(); }
      //catch (Exception)
      //{
      //  HandleException(Constants.RESOURCES_ERROR_MESSAGE, Constants.RESOURCES_ERROR_TITLE);
      //}

      //On boucle la partie tant que celle-ci retourne vrai
      do
      {
        battleship.ResetGame();
      }
      while (battleship.Run());
    }

    /// <summary>
    /// Gére les exceptions en affichant un MessageBox avec le message et le titre prit en paramètre
    /// </summary>
    /// <param name="message">Le message sous forme de string</param>
    /// <param name="title">Le titre sous forme de string</param>
    public static void HandleException(string message, string title)
    {
      System.Windows.Forms.MessageBox.Show(message, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
      Environment.Exit(1);
    }
  }
}
