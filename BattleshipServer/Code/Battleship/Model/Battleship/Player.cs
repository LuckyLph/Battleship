using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class Player
  {
    public Grid Grid { get; set; }

    private Client client;

    public Player()
    {
      Grid = new Grid();
    }
  }
}
