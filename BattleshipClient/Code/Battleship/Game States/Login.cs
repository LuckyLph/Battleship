using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Battleship
{
  public class Login
  {
    #region Variables

    #region Objects

    LoginTextField selectedLoginText = LoginTextField.Ip;                 //État de la saisi de texte dans le Login screen

    #endregion

    #region SFML

    RenderWindow window;                                                  //Objet de type RenderWindow pour la fenêtre de rendu
    Text loginText;                                                       //Text pour le login
    Text ipText;                                                          //Text pour l'ip du serveur
    Text portText;                                                        //Text pour le port du serveur
    Text ipToEnterText;                                                   //Text pour l'ip saisie
    Text portToEnterText;                                                 //Text pour le port saisi
    Text usernameText;                                                    //Text pour le nom d'utilisateur
    Text passwordText;                                                    //Text pour le mot de passe
    Text usernameToEnterText;                                             //Text pour le nom d'utilisateur saisi
    Text passwordToEnterText;                                             //Text pour le mot de passe saisi
    Text activeText;                                                      //Text actif (pour le login screen)
    Sprite loginArrow;                                                    //Sprite pour la flèche du menu de login

    #endregion

    #region Base types

    bool isDefaultName = true;                                            //Booléen pour déterminer si le champ d'entrée du nom d'utilisateur est à la valeur par défaut
    bool isDefaultPassword = true;                                        //Booléen pour déterminer si le champ d'entrée du mot de passe est à la valeur par défaut
    bool isDefaultIp = true;                                              //Booléen pour déterminer si le champ d'entrée de l'ip du serveur est à la valeur par défaut
    bool isDefaultPort = true;                                            //Booléen pour déterminer si le champ d'entrée du port du serveur est à la valeur par défaut

    string serverPassword = Constants.DEFAULT_PASSWORD;                   //String qui contient le mot de passe entré par l'utilisateur

    #endregion

    #endregion

    #region Properties

    public string ServerInfo
    {
      get
      {
        return ipToEnterText.DisplayedString + "," + portToEnterText.DisplayedString + "," + usernameToEnterText.DisplayedString + "," + serverPassword;
      }
    }

    #endregion

    /// <summary>
    /// Constructeur de la classe Login
    /// </summary>
    public Login()
    {

    }

    /// <summary>
    /// Initialise le Login screen en chargant tous les objets à risque d'exception
    /// </summary>
    /// <param name="resources"></param>
    /// <param name="window"></param>
    public void InitializeLogin(Resources resources, RenderWindow window)
    {
      this.window = window;

      loginText = new Text(Constants.LOGIN_TEXT, resources.TextFont, Constants.LOGIN_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      ipText = new Text(Constants.IP_TEXT, resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      portText = new Text(Constants.PORT_TEXT, resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      ipToEnterText = new Text(Constants.DEFAULT_SERVER_ADDRESS, resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      portToEnterText = new Text(Constants.DEFAULT_PORT.ToString(), resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      usernameText = new Text(Constants.USERNAME_TEXT, resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      passwordText = new Text(Constants.PASSWORD_TEXT, resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      usernameToEnterText = new Text(Constants.DEFAULT_NAME, resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      passwordToEnterText = new Text(Constants.DEFAULT_PASSWORD, resources.TextFont, Constants.LOGIN_INFO_TEXT_SIZE * Constants.WINDOW_X_RATIO);
      loginArrow = new Sprite(resources.ArrowTexture);

      loginText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2 - loginText.GetGlobalBounds().Width / 2, Constants.WINDOW_HEIGHT / 10);
      ipText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8, Constants.WINDOW_HEIGHT / 4);
      portText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8, Constants.WINDOW_HEIGHT / 3);
      usernameText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8, Constants.WINDOW_HEIGHT / 2);
      passwordText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8, Constants.WINDOW_HEIGHT / 2 + Constants.WINDOW_HEIGHT / 12);
      ipToEnterText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8 + ipText.GetGlobalBounds().Width, Constants.WINDOW_HEIGHT / 4);
      portToEnterText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8 + portText.GetGlobalBounds().Width, Constants.WINDOW_HEIGHT / 3);
      usernameToEnterText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8 + usernameText.GetGlobalBounds().Width, Constants.WINDOW_HEIGHT / 2);
      passwordToEnterText.Position = new Vector2f(Constants.WINDOW_WIDTH / 8 + passwordText.GetGlobalBounds().Width,
                                                  Constants.WINDOW_HEIGHT / 2 + Constants.WINDOW_HEIGHT / 12);

      loginArrow.Scale = new Vector2f(Constants.ARROW_SCALE * Constants.WINDOW_X_RATIO,
                                  Constants.ARROW_SCALE * Constants.WINDOW_Y_RATIO);
    }

    /// <summary>
    /// Met à jour le Login screen
    /// </summary>
    public void Update()
    {
      switch (selectedLoginText)
      {
        case LoginTextField.Ip:
          activeText = ipToEnterText;
          loginArrow.Position = new Vector2f(ipToEnterText.Position.X + ipToEnterText.GetGlobalBounds().Width + loginArrow.GetGlobalBounds().Width,
                                        ipToEnterText.Position.Y + Constants.ARROW_GRID_VERTICAL_OFFSET * Constants.WINDOW_X_RATIO);
          break;
        case LoginTextField.Port:
          activeText = portToEnterText;
          loginArrow.Position = new Vector2f(portToEnterText.Position.X + portToEnterText.GetGlobalBounds().Width + loginArrow.GetGlobalBounds().Width,
                           portToEnterText.Position.Y + Constants.ARROW_GRID_VERTICAL_OFFSET * Constants.WINDOW_X_RATIO);
          break;
        case LoginTextField.Username:
          activeText = usernameToEnterText;
          loginArrow.Position = new Vector2f(usernameToEnterText.Position.X + usernameToEnterText.GetGlobalBounds().Width + loginArrow.GetGlobalBounds().Width,
                           usernameToEnterText.Position.Y + Constants.ARROW_GRID_VERTICAL_OFFSET * Constants.WINDOW_X_RATIO);
          break;
        default:
          activeText = passwordToEnterText;
          loginArrow.Position = new Vector2f(passwordToEnterText.Position.X + passwordToEnterText.GetGlobalBounds().Width + loginArrow.GetGlobalBounds().Width,
                           passwordToEnterText.Position.Y + Constants.ARROW_GRID_VERTICAL_OFFSET * Constants.WINDOW_X_RATIO);
          if (!isDefaultPassword)
          {
            serverPassword = activeText.DisplayedString;
            activeText.DisplayedString = string.Empty;
            for (int i = 0; i < serverPassword.Length; i++)
            {
              activeText.DisplayedString += "*";
            }
          }
          break;
      }
    }

    /// <summary>
    /// Dessine le login screen à l'écran
    /// </summary>
    public void Draw()
    {
      window.Draw(loginText);
      window.Draw(ipText);
      window.Draw(portText);
      window.Draw(usernameText);
      window.Draw(passwordText);
      window.Draw(ipToEnterText);
      window.Draw(portToEnterText);
      window.Draw(usernameToEnterText);
      window.Draw(passwordToEnterText);
      window.Draw(loginArrow);
    }

    /// <summary>
    /// Réinitialise le login screen
    /// </summary>
    public void ResetLogin()
    {
      isDefaultIp = true;
      isDefaultName = true;
      isDefaultPassword = true;
      isDefaultPort = true;
      ipToEnterText.DisplayedString = Constants.DEFAULT_SERVER_ADDRESS;
      portToEnterText.DisplayedString = Constants.DEFAULT_PORT.ToString();
      usernameToEnterText.DisplayedString = Constants.DEFAULT_NAME;
      passwordToEnterText.DisplayedString = Constants.DEFAULT_PASSWORD;
      serverPassword = Constants.DEFAULT_PASSWORD;
      selectedLoginText = LoginTextField.Ip;
    }

    /// <summary>
    /// Gère la première frappe pour les textes d'information pour la connexion au serveur
    /// </summary>
    public void HandleLoginFirstType(Keyboard.Key e)
    {
      if (((e >= Keyboard.Key.A && e<= Keyboard.Key.Num9) || (e == Keyboard.Key.Space))
              && activeText.DisplayedString.Length < Constants.MAX_CHARACTER_AMOUNT && (isDefaultIp || isDefaultName || isDefaultPassword || isDefaultPort))
      {
        switch (selectedLoginText)
        {
          case LoginTextField.Ip:
            if (isDefaultIp)
            {
              activeText.DisplayedString = string.Empty;
              isDefaultIp = false;
            }
            break;
          case LoginTextField.Port:
            if (isDefaultPort)
            {
              activeText.DisplayedString = string.Empty;
              isDefaultPort = false;
            }
            break;
          case LoginTextField.Username:
            if (isDefaultName)
            {
              activeText.DisplayedString = string.Empty;
              isDefaultName = false;
            }
            break;
          default:
            if (isDefaultPassword)
            {
              activeText.DisplayedString = string.Empty;
              isDefaultPassword = false;
            }
            activeText = passwordToEnterText;
            break;
        }
      }
    }

    /// <summary>
    /// Gère la sélection du texte
    /// </summary>
    public void HandleLoginTextSelection(Keyboard.Key e)
    {
      if (e == Keyboard.Key.Up)
      {
        int currentTextField = (int)selectedLoginText;
        currentTextField--;
        selectedLoginText = (LoginTextField)currentTextField;
        if ((int)selectedLoginText < 0)
          selectedLoginText = (LoginTextField)0;
      }
      else
      {
        int currentTextField = (int)selectedLoginText;
        currentTextField++;
        selectedLoginText = (LoginTextField)currentTextField;
        if ((int)selectedLoginText > Constants.TEXTFIELD_AMOUNT - 1)
          selectedLoginText = (LoginTextField)Constants.TEXTFIELD_AMOUNT - 1;
      }
    }

    /// <summary>
    /// Gère les frappes de touches sur le clavier
    /// </summary>
    /// <param name="e"></param>
    public void HandleLoginKeyboardInput(Keyboard.Key e)
    {
      if (((e >= Keyboard.Key.A && e <= Keyboard.Key.Num9) || (e == Keyboard.Key.Space) || e == Keyboard.Key.BackSpace)
              && activeText.DisplayedString.Length < Constants.MAX_CHARACTER_AMOUNT)
      {
        if (e >= Keyboard.Key.Num0 && e <= Keyboard.Key.Num9)
        {
          string[] numbers = e.ToString().Split('m');
          activeText.DisplayedString += numbers[1];
        }
        else if (e >= Keyboard.Key.A && e <= Keyboard.Key.Z)
        {
          if (Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift))
            activeText.DisplayedString += e.ToString().ToUpper();
          else
            activeText.DisplayedString += e.ToString().ToLower();
        }
        else if (e == Keyboard.Key.BackSpace)
        {
          if (activeText.DisplayedString.Length > 0)
            activeText.DisplayedString = activeText.DisplayedString.Substring(0, activeText.DisplayedString.Length - 1);
        }
        else
        {
          activeText.DisplayedString += " ";
        }
      }
    }
  }
}
