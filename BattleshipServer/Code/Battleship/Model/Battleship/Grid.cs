using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  /// <summary>
  /// This class represents a game bord
  /// </summary>
  public class Grid
  {
    private GridSquare[,] grid;

    public bool Active { get; set; }

    public Grid()
    {
      grid = new GridSquare[Constants.GAME_SIZE, Constants.GAME_SIZE];
      for (int i = 0; i < Constants.GAME_SIZE; i++)
      {
        for (int j = 0; j < Constants.GAME_SIZE; j++)
        {
          grid[i, j] = new GridSquare();
        }
      }
    }

    public bool AddBoatToBoard()
    {
      return true;
    }

    public string ParseGameBoard()
    {
      return null;
    }
  }
}
