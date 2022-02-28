using GameWer.SDK;
using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace GameWer
{
  public class UIManager
  {
    private static AppDomain UIDomain = null;
    private static Thread UIThread;
    private static GameWerUI GameWerUIInstance;
    internal static IGameWerForm ProxyForm;

    private static PermissionSet CreateDomainPermission()
    {
      var permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
      return permissionSet;
    }

    private static AppDomainSetup CreateDomainSetup()
    {
      return new AppDomainSetup()
      {
        ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
      };
    }

    private static void InitThread()
    {
      try
      {
        var type = typeof (GameWerUI);
        GameWerUIInstance = (GameWerUI) UIDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
        ProxyForm = new GameWerProxy(GameWerUIInstance, UIDomain);
        GameWerUIInstance.InitUI();
      }
      catch (Exception ex)
      {
        OutputManager.Log(nameof (UIManager), $"Exception in CreateInstanceAndUnwrap: {ex}");
      }
      ApplicationManager.Shutdown();
    }

    internal static void Init()
    {
      OutputManager.Log("UI", "UIManager.Init()");
      try
      {
        CreateDomainPermission();
        UIDomain = AppDomain.CreateDomain("UIDomain", null, CreateDomainSetup());
        UIThread = new Thread(InitThread);
        UIThread.SetApartmentState(ApartmentState.STA);
        UIThread.Start();
      }
      catch (Exception ex)
      {
        OutputManager.Log(nameof (UIManager), $"Exception in UIManager.Init(): {ex}");
        ApplicationManager.Shutdown();
      }
    }

    internal static void Shutdown()
    {
      OutputManager.Log("UI", "UIManager.Shutdown()");
      try
      {
        UIThread?.Abort();
      }
      catch
      {
      }
      try
      {
        AppDomain.Unload(UIDomain);
      }
      catch
      {
      }
    }
  }
}
