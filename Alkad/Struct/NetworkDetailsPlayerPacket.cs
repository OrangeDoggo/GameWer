using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameWer.Struct
{
  public class NetworkDetailsPlayerPacket : BaseNetworkPacket
  {
    [JsonProperty("hwid_list")]
    internal string Hwid_list;
    [JsonProperty("modle")]
    internal string Modle;
    [JsonProperty("manufacturer")]
    internal string Manufacturer;
    [JsonProperty("productname")]
    internal string Productname;
    [JsonProperty("organization")]
    internal string Organization;
    [JsonProperty("owner")]
    internal string Owner;
    [JsonProperty("systemroot")]
    internal string Systemroot;
    [JsonProperty("machinename")]
    internal string Machinename;
    [JsonProperty("username")]
    internal string Username;
    [JsonProperty("isbit64")]
    internal bool IsBit64;
    [JsonProperty("memorysize")]
    internal int Memorysize;
    [JsonProperty("processorname")]
    internal string Processorname;
    [JsonProperty("processorid")]
    internal string Processorid;
    [JsonProperty("videoname")]
    internal string Videoname;
    [JsonProperty("videoid")]
    internal string Videoid;
    [JsonProperty("driversname")]
    internal string Driversname;
    [JsonProperty("driverssize")]
    internal int Driverssize;
    [JsonProperty("privateKeyHash")]
    internal string PrivateKeyHash;

    public NetworkDetailsPlayerPacket()
    {
      Method = "detailsPlayer";
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
          "hwid_list",
          Hwid_list
        },
        {
          "modle",
          Modle
        },
        {
          "manufacturer",
          Manufacturer
        },
        {
          "productname",
          Productname
        },
        {
          "organization",
          Organization
        },
        {
          "owner",
          Owner
        },
        {
          "systemroot",
          Systemroot
        },
        {
          "machinename",
          Machinename
        },
        {
          "username",
          Username
        },
        {
          "isbit64",
          IsBit64
        },
        {
          "memorysize",
          Memorysize
        },
        {
          "processorname",
          Processorname
        },
        {
          "processorid",
          Processorid
        },
        {
          "videoname",
          Videoname
        },
        {
          "videoid",
          Videoid
        },
        {
          "driversname",
          Driversname
        },
        {
          "driverssize",
          Driverssize
        },
        {
          "privateKeyHash",
          PrivateKeyHash
        }
      });
    }
  }
}
