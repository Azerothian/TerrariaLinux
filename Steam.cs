// Type: Terraria.Steam
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

using System.Runtime.InteropServices;

namespace Terraria
{
  public class Steam
  {
    public static bool SteamInit = false;

    static Steam()
    {
    }

    //[DllImport("steam_api.dll")]
    //private extern static bool SteamAPI_Init();

    //[DllImport("steam_api.dll")]
    //private extern static bool SteamAPI_Shutdown();

    //public static void Init()
    //{
    //  Steam.SteamInit = Steam.SteamAPI_Init();
    //}

    //public static void Kill()
    //{
    //  Steam.SteamAPI_Shutdown();
    //}
  }
}
