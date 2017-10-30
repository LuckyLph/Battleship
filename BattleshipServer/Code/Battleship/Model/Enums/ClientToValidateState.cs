using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public enum ClientToValidateState
  {
    VALID,
    INVALID,
    NAME_INVALID,
    TIMED_OUT,
  }
}
