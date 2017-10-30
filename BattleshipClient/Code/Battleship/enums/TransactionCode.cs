using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
  public enum TransactionCode
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
