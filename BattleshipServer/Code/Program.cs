using System;

namespace BattleshipServer
{
  public class Program
  {
    static void Main(string[] args)
    {
      char userInput = GetActionFromUser();
      switch (userInput)
      {
        case '1':
          Battleship battleship = new Battleship();
          //try { Battleship battleship = new Battleship(); }
          //catch (Exception e) { HandleException(Constants.SERVER_INITIALIZATION_ERROR_TEXT, Constants.SERVER_ERROR_TEXT, e); }
          do { battleship.ResetBattleship(); } while (battleship.Run());
          break;

        case '2':
          //BanHandler banHandler = new BanHandler();
          //try { BanHandler banHandler = new BanHandler(); }
          //catch (Exception e) { HandleException(Constants.SERVER_INITIALIZATION_ERROR_TEXT, Constants.SERVER_ERROR_TEXT, e); }
          break;

        case '3':
          break;

        case '4':
          break;
      }
    }

    /// <summary>
    /// Obtient le BootOption désiré par l'usager
    /// </summary>
    /// <returns>Choix de l'usager sous forme de char</returns>
    private static char GetActionFromUser()
    {
      char userInput;
      do
      {
        Console.Clear();
        Console.WriteLine(Constants.SERVER_BOOT_OPTION_TEXT);
        userInput = (char)Console.Read();
        userInput = Char.ToUpper(userInput);
      }
      while (userInput < 49 || userInput > 52);
      return userInput;
    }

    /// <summary>
    /// Gére les exceptions en affichant un MessageBox avec le message et le titre prit en paramètre
    /// </summary>
    /// <param name="message">Le message sous forme de string</param>
    /// <param name="title">Le titre sous forme de string</param>
    public static void HandleException(string message, string title, Exception e)
    {
      Console.WriteLine(title + " : " + message);
      Console.WriteLine(e.ToString());
      Console.WriteLine(Constants.PRESS_ANY_KEY_TO_CONTINUE);
      Console.Read();
      Environment.Exit(1);
    }
  }
}
