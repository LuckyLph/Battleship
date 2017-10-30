using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BattleshipServer
{
  public class Ban
  {
    public string BanReason { get; private set; }
    public IPAddress IpAddress { get; private set; }

    public Ban(IPAddress ipAddress, string banReason)
    {
      IpAddress = ipAddress;
      BanReason = banReason;
    }
  }
}
