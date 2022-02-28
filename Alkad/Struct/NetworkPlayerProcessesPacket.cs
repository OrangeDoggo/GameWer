using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameWer.Struct
{
  public class NetworkPlayerProcessesPacket : BaseNetworkPacket
  {
    [JsonProperty("processes")]
    internal PlayerProcess[] Processes;

    public NetworkPlayerProcessesPacket()
    {
      Method = "playerProcesses";
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
          "processes",
          Processes
        }
      });
    }
  }
}
