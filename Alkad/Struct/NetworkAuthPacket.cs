using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameWer.Struct
{
  public class NetworkAuthPacket : BaseNetworkPacket
  {
    [JsonProperty("version")]
    internal string Version;
    [JsonProperty("steamid")]
    internal string SteamID;
    [JsonProperty("hwid")]
    internal string HWID;
    [JsonProperty("pcid")]
    internal string PCID;
    [JsonProperty("dsid")]
    internal string DSID;
    [JsonProperty("lastSessionKey")]
    internal string LastSessionKey;
    [JsonProperty("publicKey")]
    internal string PublicKey;
    [JsonProperty("publicKeyHash")]
    internal string PublicKeyHash;

    public NetworkAuthPacket()
    {
      Method = "client";
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
          "version",
          Version
        },
        {
          "steamid",
          SteamID
        },
        {
          "hwid",
          HWID
        },
        {
          "pcid",
          PCID
        },
        {
          "dsid",
          DSID
        },
        {
          "lastSessionKey",
          LastSessionKey
        },
        {
          "publicKey",
          PublicKey
        },
        {
          "publicKeyHash",
          PublicKeyHash
        }
      });
    }
  }
}
