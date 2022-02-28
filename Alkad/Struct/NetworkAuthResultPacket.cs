using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameWer.Struct
{
  public class NetworkAuthResultPacket : BaseNetworkPacket
  {
    [JsonProperty("result")]
    internal bool Result;
    [JsonProperty("privateKey")]
    internal string PrivateKey;
    [JsonProperty("sessionKey")]
    public string SessionKey;

    public NetworkAuthResultPacket()
    {
      Method = "authResult";
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
          "result",
          Result
        },
        {
          "privateKey",
          PrivateKey
        },
        {
          "sessionKey",
          SessionKey
        }
      });
    }

    internal static NetworkAuthResultPacket ParseObject(string content)
    {
      return ParseObject(JsonConvert.DeserializeObject<Dictionary<string, object>>(content));
    }

    internal static NetworkAuthResultPacket ParseObject(
      Dictionary<string, object> json)
    {
      return new NetworkAuthResultPacket()
      {
        Result = (bool) json["result"],
        PrivateKey = (string) json["privateKey"],
        SessionKey = (string) json["sessionKey"]
      };
    }
  }
}
