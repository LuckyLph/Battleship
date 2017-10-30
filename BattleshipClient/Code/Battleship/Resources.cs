using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Battleship
{
  public class Resources
  {
    #region Variables

    Font textFont;                                                      //Font pour les textes du jeu
    Texture battleshipTexture;                                          //Texture pour le logo de battleship
    Texture arrow;                                                      //Texture pour la flèche de sélection
    Texture carrier;                                                    //Texture pour le carrier
    Texture destroyer;                                                  //Texture pour le destroyer
    Texture frigate;                                                    //Texture pour la frégate
    Texture submarine;                                                  //Texture pour le sous-marin
    Texture corvette;                                                   //Texture pour la corvette
    Texture checkmark;                                                  //Texture pour le checkmark
    Texture water;                                                      //Texture d'eau pour la grille de jeu
    Texture highlight;                                                  //Texture pour le highlight de la grille de jeu
    Texture hit;                                                        //Texture pour le hit de la grille de jeu
    Texture miss;                                                       //Texture pour le miss de la grille de jeu
    Texture targeted;                                                   //Texture pour quand le joueur cible une case de la grille de jeu
    Texture explosionTexture;                                           //Texture pour l'explosion de la cible

    #endregion

    #region Properties

    public Font TextFont
    {
      get { return textFont; }
    }

    public Texture BattleshipTexture
    {
      get { return battleshipTexture; }
    }

    public Texture ArrowTexture
    {
      get { return arrow; }
    }

    public Texture CarrierTexture
    {
      get { return carrier; }
    }

    public Texture DestroyerTexture
    {
      get { return destroyer; }
    }

    public Texture FrigateTexture
    {
      get { return frigate; }
    }

    public Texture SubmarineTexture
    {
      get { return submarine; }
    }

    public Texture CorvetteTexture
    {
      get { return corvette; }
    }

    public Texture CheckmarkTexture
    {
      get { return checkmark; }
    }

    public Texture WaterTexture
    {
      get { return water; }
    }

    public Texture HighlightTexture
    {
      get { return highlight; }
    }

    public Texture HitTexture
    {
      get { return hit; }
    }

    public Texture MissTexture
    {
      get { return miss; }
    }

    public Texture TargetedTexture
    {
      get { return targeted; }
    }

    public Texture ExplosionTexture
    {
      get { return explosionTexture; }
    }

    #endregion

    /// <summary>
    /// Constructeur de la classe resources
    /// </summary>
    public Resources()
    {

    }

    /// <summary>
    /// Initialise les resources à risque de causer une exception
    /// </summary>
    /// <returns>Un booléen qui indique si l'initialisation a réussi</returns>
    public void InitializeResources()
    {
      textFont = new Font(Constants.TEXTFONT_PATH);
      battleshipTexture = new Texture(Constants.BATTLESHIP_LOGO_PATH);
      arrow = new Texture(Constants.ARROW_PATH);
      carrier = new Texture(Constants.CARRIER_PATH);
      destroyer = new Texture(Constants.DESTROYER_PATH);
      frigate = new Texture(Constants.FRIGATE_PATH);
      submarine = new Texture(Constants.SUBMARINE_PATH);
      corvette = new Texture(Constants.CORVETTE_PATH);
      checkmark = new Texture(Constants.CHECKMARK_PATH);
      water = new Texture(Constants.WATER_PATH);
      highlight = new Texture(Constants.HIGHLIGHT_PATH);
      hit = new Texture(Constants.HIGHLIGHT_PATH);
      miss = new Texture(Constants.MISS_PATH);
      targeted = new Texture(Constants.TARGETED_PATH);
      explosionTexture = new Texture(Constants.EXPLOSION_PATH);
    }
  }
}
