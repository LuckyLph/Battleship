using System;
using System.Timers;
using PacketProtocolClass;

namespace BattleshipServer
{
  public class ClientCommunicationHandler
  {
    public static byte[] HandleClientCommunication(Client client, PacketProtocol packetProtocol, string message, bool isExpectingResponse)
    {
      int elapsedTimeInSeconds = 0;
      Timer timer = new Timer(Constants.TIMER_INTERVAL_AMOUNT) { AutoReset = true };
      bool messageArrived = false;
      bool isExpired = false;
      byte[] buffer = new byte[Constants.BUFFER_SIZE];

      packetProtocol.MessageArrived += delegate (byte[] bytes)
      {
        bytes.CopyTo(buffer, 0);
        messageArrived = true;
      };
      timer.Elapsed += delegate (object sender, ElapsedEventArgs args)
      {
        if (elapsedTimeInSeconds >= Constants.TRANSACTION_TIMEOUT_DELAY)
        {
          isExpired = true;
        }
        else
        {
          elapsedTimeInSeconds++;
        }
      };
      timer.Start();

      try
      {
        client.Socket.Send(PacketProtocol.WrapMessage(System.Text.Encoding.ASCII.GetBytes(message)));
        if (isExpectingResponse)
        {
          while (!messageArrived && !isExpired)
          {
            client.Socket.Receive(buffer);
            packetProtocol.DataReceived(buffer);
          }
          if (isExpired)
          {
            throw new TimeoutException();
          }
        }
        Debug.Log(Constants.CLIENT_TRANSACTION_SUCCEEDED_TEXT);
      }
      catch (Exception e)
      {
        Debug.Log(Constants.PACKET_PROTOCOL_EXCEPTION_TEXT + e.ToString());
        throw e;
      }
      finally
      {
        timer.Close();
      }
      return buffer;
    }
  }
}
