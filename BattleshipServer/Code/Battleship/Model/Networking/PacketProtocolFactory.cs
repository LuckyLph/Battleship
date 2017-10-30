namespace BattleshipServer
{
  public class PacketProtocolFactory
  {
    /// <summary>
    /// Returns a packet protocol to be used for client-server communication
    /// </summary>
    /// <param name="maxMessageSize">The maximum message size supported by this protocol. This may be less than or equal to zero to indicate no maximum message size.</param>
    /// <returns></returns>
    public static PacketProtocolClass.PacketProtocol GetPacketProtocol(int maxMessageSize)
    {
      return new PacketProtocolClass.PacketProtocol(maxMessageSize);
    }
  }
}
