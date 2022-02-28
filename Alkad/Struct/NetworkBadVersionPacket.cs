namespace GameWer.Struct
{
  public class NetworkBadVersionPacket : BaseNetworkPacket
  {
    public NetworkBadVersionPacket()
    {
      Method = "badVersion";
    }
  }
}
