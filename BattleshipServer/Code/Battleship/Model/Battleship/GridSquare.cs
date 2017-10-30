using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class GridSquare
  {
    public BoatType Content { get; set; }
    public bool Available { get; set; }

    public GridSquare()
    {
      Content = BoatType.None;
      Available = true;
    }
  }
}
