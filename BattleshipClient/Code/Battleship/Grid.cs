using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;

namespace Battleship
{
  public class Grid
  {
    #region Variables

    GridContent[,] gameBoard;                                             //Grille de jeu Contenant des objets GridContent
    FloatRect[,] gridHitboxes;                                            //Tableau de floatrect pour gèrer les collisions avec la grille

    bool isActive;                                                        //Booléen qui indique si la grille est active

    #endregion

    #region Properties

    public GridContent[,] GameBoard
    {
      get { return gameBoard; }
      set { gameBoard = value; }
    }

    public FloatRect[,] GameBoardHitBoxes
    {
      get { return gridHitboxes; }
      set { gridHitboxes = value; }
    }

    public bool IsActive
    {
      get { return isActive; }
      set { isActive = value; }
    }

    #endregion

    /// <summary>
    /// Constructeur de Grid
    /// </summary>
    public Grid()
    {

    }

    /// <summary>
    /// Initialise la grille actuelle
    /// </summary>
    /// <param name="resources"></param>
    public void InitializeGrid(Resources resources)
    {
      isActive = false;
      gridHitboxes = new FloatRect[Constants.GAME_SIZE, Constants.GAME_SIZE];
      gameBoard = new GridContent[Constants.GAME_SIZE, Constants.GAME_SIZE];

      for (int i = 0; i < Constants.GAME_SIZE; i++)
      {
        for (int j = 0; j < Constants.GAME_SIZE; j++)
        {
          gameBoard[i, j] = new GridContent();
          Vector2f position = new Vector2f(i * Constants.SQUARE_SIZE * Constants.WINDOW_WIDTH / Constants.WINDOW_WIDTH_DEFAULT,
                                           j * Constants.SQUARE_SIZE * Constants.WINDOW_HEIGHT / Constants.WINDOW_HEIGHT_DEFAULT);
          gameBoard[i, j].InitializeGridContent(resources, position);
          gridHitboxes[i, j] = gameBoard[i, j].GetGlobalBounds();
        }
      }
    }

    /// <summary>
    /// Met à jour la grille
    /// </summary>
    public void UpdateGrid()
    {
      for (int i = 0; i < Constants.GAME_SIZE; i++)
      {
        for (int j = 0; j < Constants.GAME_SIZE; j++)
        {
          gameBoard[i, j].UpdateGridContent();
        }
      }
    }

    /// <summary>
    /// Change la couleur des carrés au dessus desquels se trouve le bateau sélectionné
    /// </summary>
    public void HighlightSquares(RectangleShape activeSpriteHitbox, bool isActiveSpriteNull)
    {
      FloatRect activeBounds = activeSpriteHitbox.GetGlobalBounds();
      for (int i = 0; i < Constants.GAME_SIZE; i++)
      {
        for (int j = 0; j < Constants.GAME_SIZE; j++)
        {
          if (!isActiveSpriteNull && gridHitboxes[i, j].Intersects(activeBounds))
            gameBoard[i, j].Status = SquareStatus.Highlight;
          else
            gameBoard[i, j].Status = SquareStatus.Water;
        }
      }
    }

    /// <summary>
    /// Ajoute le bateau placé dans la grille logique
    /// </summary>
    public bool AddPlacedBoatToBoard(RectangleShape activeSpriteHitbox, BoatType selectedBoat)
    {
      FloatRect boundsToCheck = activeSpriteHitbox.GetGlobalBounds();
      Func<int, bool> CheckCells = delegate(int index)
      {
        for (int i = 0; i < Constants.GAME_SIZE; i++)
        {
          if (GameBoardHitBoxes[index, i].Intersects(boundsToCheck))
          {
            if (GameBoard[index, i].SquareType == BoatType.None)
            {
              GameBoard[index, i].SquareType = selectedBoat;
            }
            else
            {
              ResetPlacedBoat(selectedBoat);
              return false;
            }
          }
        }
        return true;
      };

      for (int i = 0; i < Constants.GAME_SIZE; i++)
      {
        if (!CheckCells(i))
          return false;
      }
      return true;
    }

    /// <summary>
    /// Réinitialise le bateau placé
    /// </summary>
    public void ResetPlacedBoat(BoatType selectedBoat)
    {
      for (int i = 0; i < Constants.GAME_SIZE; i++)
      {
        for (int j = 0; j < Constants.GAME_SIZE; j++)
        {
          if (GameBoard[i, j].SquareType == selectedBoat)
          {
            GameBoard[i, j].SquareType = BoatType.None;
          }
        }
      }
    }

    /// <summary>
    /// Parse le GameBoard sous forme de string
    /// </summary>
    public string ParseGameBoard()
    {
      string boatPlacement = string.Empty;
      Action<BoatType> GetBoatLocation = delegate(BoatType type)
      {
        boatPlacement += "[";
        for (int i = 0; i < Constants.GAME_SIZE; i++)
        {
          for (int j = 0; j < Constants.GAME_SIZE; j++)
          {
            if (GameBoard[i, j].SquareType == type)
            {
              boatPlacement += i + "," + j + ";";
            }
          }
        }
        boatPlacement += "]";
      };

      for (int i = 1; i < Constants.BOATTYPE_AMOUNT; i++)
      {
        GetBoatLocation((BoatType)i);
      }
      return boatPlacement;
    }
  }
}