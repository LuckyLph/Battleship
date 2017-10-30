using System.IO;

namespace BattleshipServer
{
  public class Debug
  {
    public static void Log(string message)
    {
      Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Debug");
      StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + "\\Debug\\debug.txt") { AutoFlush = true };
      streamWriter.WriteLine(System.DateTime.Now.ToString("h:mm:ss tt") + message);
      streamWriter.Close();
    }
  }
}
