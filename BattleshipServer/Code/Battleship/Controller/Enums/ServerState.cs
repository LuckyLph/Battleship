using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public enum ServerState
  {
    STARTING,
    WAITING_ON_CLIENTS,
    STARTING_GAME,
    PLAYING,
  }
}
