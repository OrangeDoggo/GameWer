using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameWer.Struct
{
  public class BannedPlayerResultPacket : BaseNetworkPacket
  {
    [JsonProperty("reason")]
    internal string Reason;
    [JsonProperty("finis_at")]
    internal uint FinishAt;

    public BannedPlayerResultPacket()
    {
      Method = "bannedResult";
    }

    internal override string ParseJSON()
    {
      return JsonConvert.SerializeObject(new Dictionary<string, object>()
      {
        {
          "method",
          Method
        },
        {
          "reason",
          Reason
        },
        {
          "finis_at",
          FinishAt
        }
      });
    }

    internal static BannedPlayerResultPacket ParseObject(string content)
    {
      return ParseObject(JsonConvert.DeserializeObject<Dictionary<string, object>>(content));
    }

    internal static BannedPlayerResultPacket ParseObject(
      Dictionary<string, object> json)
    {
      return new BannedPlayerResultPacket()
      {
        Reason = json["reason"].ToString(),
        FinishAt = (uint) double.Parse(json["finis_at"].ToString())
      };
    }
  }
}
