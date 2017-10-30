using System;

namespace BattleshipServer
{
  public class ConsoleView
  {
    public int ConsoleCursorTop { get => Console.CursorTop; }

    public int ConsoleBufferWidth { get => Console.BufferWidth; } 


    public void WriteToConsole(object messageToWrite, IOType outputType)
    {
      try
      {
        string outputToSend;
        if (!(messageToWrite is String))
        {
          outputToSend = messageToWrite.ToString();
        }
        else
        {
          outputToSend = (string)messageToWrite;
        }

        if (outputType == IOType.NEXT_LINE)
        {
          Console.WriteLine(outputToSend);
        }
        else
        {
          Console.Write(outputToSend);
        }
      }
      catch (System.IO.IOException e)
      {
        Program.HandleException(Constants.SERVER_OUTPUT_ERROR_MESSAGE_TEXT, Constants.SERVER_IO_ERROR_TEXT, e);
      }
    }

    public string ReadFromConsole(IOType inputType)
    {
      try
      {
        if (inputType == IOType.NEXT_LINE)
        {
          return Console.ReadLine();
        }
        else
        {
          return Console.Read().ToString();
        }
      }
      catch (Exception e)
      {
        Program.HandleException(Constants.SERVER_INPUT_ERROR_MESSAGE_TEXT, Constants.SERVER_IO_ERROR_TEXT, e);
        return null;
      }
    }

    public void ClearConsole()
    {
      Console.Clear();
    }

    public void SetCursorPosition(int left, int top)
    {
      Console.SetCursorPosition(left, top);
    }
  }
}
