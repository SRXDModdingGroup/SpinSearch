using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SRXD.SpinSearch.Plugin;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInProcess("SpinRhythm.exe")]
public class ModPlugin : BaseUnityPlugin
{
    public new static ManualLogSource Logger { get; private set; }

    private void Awake()
    {
        Logger = base.Logger;

        var harmony = new Harmony(PluginInfo.PLUGIN_NAME);
        harmony.PatchAll(typeof(Mod));
    }
}
