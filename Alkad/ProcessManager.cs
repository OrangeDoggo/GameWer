using GameWer.CustomSystem.Process32;
using GameWer.Data;
using GameWer.Helper;
using GameWer.Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GameWer
{
  public class ProcessManager
  {
    internal static HashSet<string> ListSendPath = null;

    internal static void Init()
    {
      OutputManager.Log("Process", "ProcessManager.Init()");
      PreStartCheck();
      Interface.OnProcess = OnProcessIncoming;
      Interface.Init();
      Timer.Interval(DoSendNewProcesses, exception => {}, 15f);
    }

    private static void PreStartCheck()
    {
      OutputManager.Log("Process", "ProcessManager.PreStartCheck()");
      var processes = Process.GetProcesses();
      foreach (var t in processes)
      {
        try
        {
          if (!File.Exists($"{new FileInfo(t.MainModule.FileName).Directory.FullName}/kprocesshacker.sys")) continue;
          
          t.Kill();
          Environment.Exit(0);
          break;
        }
        catch
        {
        }
      }
    }

    internal static void Shutdown()
    {
      OutputManager.Log("Process", "ProcessManager.Shutdown()");
      try
      {
        Interface.WorkerThread.Abort();
      }
      catch
      {
      }
    }

    private static void OnProcessIncoming(EntryItem process)
    {
      FindAndKillGame(process);
      if (AntiBanKiller(process) || ListSendPath == null)
        return;
      lock (ListSendPath)
      {
        if (!ListSendPath.Contains(process.FilePath))
        {
          ListSendPath.Add(process.FilePath);
          NetworkManager.Send(new NetworkPlayerProcessesPacket
          {
            Processes = new PlayerProcess[1]
            {
              new PlayerProcess()
              {
                Hash = string.IsNullOrEmpty(process.Info) ? Crypto.GetMD5FromLine(process.Name) : process.Info,
                Name = process.Name,
                Path = string.IsNullOrEmpty(process.FilePath) ? process.Name : process.FilePath,
                Secure = process.Secure,
                Size = (int) (process.Length / 1024L),
                Class = process.Class,
                Title = process.Title,
                Origin = process.Origin
              }
            }
          }.ParseJSON());
        }
      }
    }

    internal static void DoSendNewProcesses()
    {
      if (ListSendPath == null)
        return;
      var processesList = Interface.GetProcessesList();
      var playerProcessList = new List<PlayerProcess>();
      for (var index = 0; index < processesList.Length; ++index)
      {
        try
        {
          lock (ListSendPath)
          {
            if (!ListSendPath.Contains(processesList[index].FilePath))
            {
              ListSendPath.Add(processesList[index].FilePath);
              playerProcessList.Add(new PlayerProcess()
              {
                Hash = processesList[index].Info,
                Name = processesList[index].Name,
                Path = processesList[index].FilePath,
                Secure = processesList[index].Secure,
                Size = (int) (processesList[index].Length / 1024L),
                Class = processesList[index].Class,
                Title = processesList[index].Title,
                Origin = processesList[index].Origin
              });
            }
          }
        }
        catch (Exception ex)
        {
          OutputManager.Log(nameof (ProcessManager), $"Exception in ProcessManager.DoSendNewProcesses::Tick: {ex}");
        }
      }
      if (playerProcessList.Count > 0)
        NetworkManager.Send(new NetworkPlayerProcessesPacket()
        {
          Processes = playerProcessList.ToArray()
        }.ParseJSON());
    }

    private static bool AntiBanKiller(EntryItem process)
    {
      var lower = process.Name.ToLower();
      if (Processes.AntiBanProcesses.Any(t => lower.StartsWith(t)))
      {
        OutputManager.Log("Process", $"ProcessManager.AntiBanKiller::Prevention: {process.Name}");
        //return KillProcess(process);
      }
      return false;
    }

    private static bool KillProcess(EntryItem process)
    {
      if (process.ID <= 0U) return false;
      
      OutputManager.Log("Process", $"ProcessManager.KillProcess({process.Name})");
      try
      {
        Process.GetProcessById((int) process.ID).Kill();
        return true;
      }
      catch
      {
        try
        {
          Process.Start("cmd", $"/C taskkill /F /PID {process.ID}");
          return true;
        }
        catch (Exception)
        {
        }
      }
      return false;
    }

    private static void FindAndKillGame(EntryItem process)
    {
      if ((int) DateTime.Now.Subtract(ApplicationManager.StartApplicationTime).TotalSeconds >= int.Parse("10") || string.IsNullOrEmpty(process.DirectoryPath) != (int.Parse("0") == 1) || !Directory.Exists(
        $"{process.DirectoryPath}/{process.Name}_Data/"))
        return;
      OutputManager.Log("Process", $"ProcessManager.FindAndKillGame::DetectedGame: {process.DirectoryPath}");
      KillProcess(process);
    }
  }
}
