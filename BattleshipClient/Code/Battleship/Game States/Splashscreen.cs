using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.Window;
using SFML.System;
using MotionNET;

namespace Battleship
{
  public class Splashscreen
  {
    #region Variables

    #region Objects

    Time splashScreenTime;

    #endregion

    #region SFML

    RenderWindow window;                                                  //Objet de type RenderWindow pour la fenêtre de rendu
    DataSource dataSource;                                                //Datasource pour la vidéo
    AudioPlayback audioPlayBack;                                          //AudioPlayback pour la vidéo
    VideoPlayback videoPlayBack;                                          //VideoPlayback pour la vidéo
    Sprite battleshipLogo;                                                //Sprite pour le logo de battleship
    Text splashScreenText;                                                //Text pour le splashscreen

    #endregion

    #region Base types

    bool splashscreenTextAnimDirection = true;                            //Booléen qui indique si le texte du splashscreen doit grossir

    float splashscreenElapsedTime = 0;                            //Temps écoulé depuis la dernière mise à jour

    #endregion

    #endregion

    #region Properties

    #endregion


    /// <summary>
    /// Constructeur de la classe splashscreen
    /// </summary>
    public Splashscreen()
    {
      
    }

    /// <summary>
    /// Initialise le splashscreen
    /// </summary>
    /// <returns></returns>
    public void InitializeSplashscreen(Resources resources, RenderWindow window)
    {
      this.window = window;
      dataSource = new DataSource();
      dataSource.LoadFromFile(Constants.MENU_VIDEO_PATH);
      dataSource.EndofFileReached += OnEndOfFileReached;
      audioPlayBack = new AudioPlayback(dataSource);
      videoPlayBack = new VideoPlayback(dataSource);
      battleshipLogo = new Sprite(resources.BattleshipTexture);
      splashScreenText = new Text(Constants.SPLASHSCREEN_TEXT, resources.TextFont, Constants.SPLASHSCREEN_TEXT_SIZE * Constants.WINDOW_X_RATIO);

      battleshipLogo.Position = new Vector2f(Constants.WINDOW_WIDTH / 2, Constants.WINDOW_HEIGHT / 4);
      splashScreenText.Position = new Vector2f(Constants.WINDOW_WIDTH / 2, Constants.WINDOW_HEIGHT / 2 + Constants.WINDOW_HEIGHT / 4);

      battleshipLogo.Origin = new Vector2f(battleshipLogo.Texture.Size.X / 2, battleshipLogo.Texture.Size.Y / 2);

      battleshipLogo.Scale = new Vector2f(Constants.BATTLESHIP_LOGO_SCALE * Constants.WINDOW_X_RATIO,
                                          Constants.BATTLESHIP_LOGO_SCALE * Constants.WINDOW_Y_RATIO);

      splashScreenText.Color = Color.Green;

    }

    /// <summary>
    /// Met à jour le splashscreen
    /// </summary>
    public void Update(Time gameTime)
    {
      dataSource.Update();
      #region Animation

      if (splashScreenText.CharacterSize > Constants.SPLASHSCREEN_TEXT_SIZE * Constants.WINDOW_X_RATIO + Constants.SPLASHSCREEN_TEXT_SIZE_OFFSET * Constants.WINDOW_X_RATIO)
        splashscreenTextAnimDirection = false;
      else if (splashScreenText.CharacterSize < Constants.SPLASHSCREEN_TEXT_SIZE * Constants.WINDOW_X_RATIO - Constants.SPLASHSCREEN_TEXT_SIZE_OFFSET * Constants.WINDOW_X_RATIO)
        splashscreenTextAnimDirection = true;

      splashScreenTime = gameTime;
      splashscreenElapsedTime += splashScreenTime.AsMilliseconds();
      //Si il s'est écoulé le nombre de millisecondes requises pour changer la grosseur des charactères du Text
      if (splashscreenElapsedTime > Constants.SPLASHSCREEN_TEXT_ANIMATION_DELAY)
      {
        splashscreenElapsedTime = 0;
        if (splashscreenTextAnimDirection)
          splashScreenText.CharacterSize++;
        else
          splashScreenText.CharacterSize--;
      }
      splashScreenText.Origin = new Vector2f(splashScreenText.GetGlobalBounds().Width / 2, splashScreenText.GetGlobalBounds().Height / 2);

      #endregion
    }

    /// <summary>
    /// Dessine le splashscreen
    /// </summary>
    public void Draw()
    {
      if (!dataSource.IsEndofFileReached)
        window.Draw(videoPlayBack);
      window.Draw(splashScreenText);
    }

    /// <summary>
    /// Réinitialise le splashscreen et tous ses attributs
    /// </summary>
    public void ResetSplashscreen()
    {
      splashscreenTextAnimDirection = true;
      splashscreenElapsedTime = 0;
      splashScreenTime = Time.Zero;
      try { dataSource.LoadFromFile(Constants.MENU_VIDEO_PATH); }
      catch (Exception) { Program.HandleException(Constants.RESOURCES_ERROR_MESSAGE, Constants.RESOURCES_ERROR_TITLE); }
      dataSource.Play();
    }

    /// <summary>
    /// Arrête le DataSource
    /// </summary>
    public void StopDataSource()
    {
      dataSource.Stop();
    }

    /// <summary>
    /// Méthode qui est appelée lorsque la vidéo atteint sa fin
    /// </summary>
    /// <param name="source"></param>
    private void OnEndOfFileReached(object source)
    {
      try { dataSource.LoadFromFile(Constants.MENU_VIDEO_PATH); }
      catch (Exception) { Program.HandleException(Constants.RESOURCES_ERROR_MESSAGE, Constants.RESOURCES_ERROR_TITLE); }
      dataSource.Play();
    }
  }
}
