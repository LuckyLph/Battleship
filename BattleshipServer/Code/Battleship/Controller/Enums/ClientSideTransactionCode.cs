using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public enum ClientSideTransactionCode
  {
    PlayerTurn,
    PlayerBoatMissed,
    PlayerBoatHit,
    PlayerBoatDestroyed,
    EnemyTurn,
    EnemyBoatMissed,
    EnemyBoatHit,
    EnemyBoatDestroyed,
    EnemyEliminated,
    GameReady,
    Victory,
    Defeat,
  }
}
