using System;
using HarmonyLib;
using UnityEngine;

namespace SRXD.SpinSearch.Plugin;

public static class Mod
{
    private static string searchFilter = "";
    private static TrackList currentTrackList;

    [HarmonyPatch(typeof(TrackList))]
    [HarmonyPatch("ShouldBeFilteredIn")]
    [HarmonyPostfix]
    private static void TrackList_ShouldBeFilteredIn_Postfix(ref bool __result,
        MetadataHandle metadataHandle)
    {
        if (!__result)
            return;

        TrackInfoMetadata trackInfoMetadata = metadataHandle.TrackInfoMetadata;
        __result = MetadataFilter.ShouldFilterIn(trackInfoMetadata);
    }

    [HarmonyPatch(typeof(XDLevelSelectMenuBase))]
    [HarmonyPatch("OnGUI")]
    [HarmonyPrefix]
    public static bool XDLevelSelectMenuBase_OnGUI_Prefix(
        XDLevelSelectMenuBase __instance)
    {
        if (__instance.InAmount != 1f)
            return false;

        if (currentTrackList is null)
            return false;

        string previousValue = searchFilter;
        searchFilter = GUI.TextField(new Rect(10, 10, 200, 20), searchFilter);

        if (previousValue != searchFilter)
        {
            MetadataFilter.SetSearchFilter(searchFilter);
            currentTrackList.RequiresFilteredTrackListUpdate = true;
        }

        return false;
    }

    [HarmonyPatch(typeof(XDLevelSelectMenuBase))]
    [HarmonyPatch("OnEnable")]
    [HarmonyPostfix]
    private static void XDLevelSelectMenuBase_Start_Postfix(XDLevelSelectMenuBase __instance)
    {
        var instance = GameSystemSingleton<TrackListSystem, TrackListSystemSettings>.Instance;

        currentTrackList = __instance switch
        {
            XDCustomLevelSelectMenu => instance.CustomTrackList,
            //XDLevelSelectMenu => instance.LicensedTrackList,
            _ => null,
        };

        if (currentTrackList is null)
        {
            ModPlugin.Logger.LogError($"Unsupported MenuType: {__instance}");
            return;
        }

        searchFilter = "";
        MetadataFilter.SetSearchFilter(searchFilter);
        currentTrackList.RequiresFilteredTrackListUpdate = true;
        ModPlugin.Logger.LogDebug($"Switched to {__instance}");
    }
}
