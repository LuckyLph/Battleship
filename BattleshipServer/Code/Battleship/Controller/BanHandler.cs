using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
  public class BanHandler
  {
    private string banListPath;

    public BanHandler()
    {
      banListPath = System.IO.Directory.GetCurrentDirectory() + Constants.BAN_LIST_PATH_TEXT;
    }

    public string[] GetBanList()
    {
      if (File.Exists(banListPath))
      {
          return File.ReadAllLines(banListPath);
      }
      else
      {
        return null;
      }
    }

    
  }
}
