using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using HoverHotkey.Configuration;
using HoverHotkey.UI;
using BS_Utils.Utilities;
using BeatSaberMarkupLanguage.Settings;

namespace HoverHotkey
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        public static bool startupReady = false;

        [Init]
        public void Init(IPALogger logger, IPA.Config.Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            BSEvents.lateMenuSceneLoadedFresh += BSEvents_lateMenuSceneLoadedFresh;
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            new GameObject("HoverHotkeyController").AddComponent<HoverHotkeyController>();

            //Register mod settings menu button
            BSMLSettings.instance.AddSettingsMenu("HoverHotkey", "HoverHotkey.UI.ConfigView.bsml", ConfigView.instance);
        }

        private void BSEvents_lateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {
            startupReady = true;
            FloatingButtonController.Instance.ShowHotkeyWindow();
        }
    }
}
