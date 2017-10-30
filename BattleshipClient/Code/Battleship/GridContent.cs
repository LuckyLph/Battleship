using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Battleship
{
  public class GridContent : Sprite
  {
    #region Variables

    bool hasStatusChanged = false;                                  //Booléen qui indique si le status de la case a changé

    BoatType type;                                                  //Type du bateau présent sur la case
    SquareStatus status;                                            //Status du carré

    Texture waterTexture;                                           //Texture si la case est de l'eau
    Texture highlightTexture;                                       //Texture si la case est en mode highlight
    Texture hitTexture;                                             //Texture si la case est en mode hit
    Texture missTexture;                                            //Texture si la case est en mode miss
    Texture targetedTexture;                                        //Texture si la case est ciblée

    #endregion

    #region Properties

    public BoatType SquareType
    {
      get { return type; }
      set { type = value; }
    }

    public SquareStatus Status
    {
      get { return status; }
      set { status = value; hasStatusChanged = true; }
    }

    #endregion

    /// <summary>
    /// Constructeur de GridContent
    /// </summary>
    public GridContent()
    {

    }

    public bool InitializeGridContent(Resources resources, Vector2f position)
    {
      this.waterTexture = new Texture(resources.WaterTexture);
      this.highlightTexture = new Texture(resources.HighlightTexture);
      this.hitTexture = new Texture(resources.HitTexture);
      this.missTexture = new Texture(resources.MissTexture);
      this.targetedTexture = new Texture(resources.TargetedTexture);
      this.Texture = new Texture(waterTexture);
      this.type = BoatType.None;
      this.status = SquareStatus.Water;
      this.Position = position;

      return true;
    }

    /// <summary>
    /// Met à jour le contenu de la grille
    /// </summary>
    public void UpdateGridContent()
    {
      if (hasStatusChanged)
      {
        switch (status)
        {
          case SquareStatus.Water:
            Texture = waterTexture;
            break;
          case SquareStatus.Highlight:
            Texture = highlightTexture;
            break;
          case SquareStatus.Hit:
            Texture = hitTexture;
            break;
          case SquareStatus.Miss:
            Texture = missTexture;
            break;
          case SquareStatus.Targeted:
            Texture = targetedTexture;
            break;
        }
        hasStatusChanged = false;
      }
    }
  }
}
