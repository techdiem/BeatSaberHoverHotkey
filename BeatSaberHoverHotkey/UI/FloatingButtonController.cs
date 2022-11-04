using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.FloatingScreen;
using BS_Utils.Utilities;
using HoverHotkey.Configuration;

namespace HoverHotkey.UI
{
    class FloatingButtonController
    {
        private static FloatingButtonController _instance;
        public static FloatingButtonController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FloatingButtonController();
                return _instance;
            }
        }

        public FloatingScreen HotkeyScreen;
        protected FloatingButton FloatingButton;
        protected FloatingButtonController()
        {
            BSEvents.earlyMenuSceneLoadedFresh += BSEvents_earlyMenuSceneLoadedFresh;
        }

        public void Cleanup()
        {
            BSEvents.earlyMenuSceneLoadedFresh -= BSEvents_earlyMenuSceneLoadedFresh;
            BSEvents.menuSceneActive -= OnSongExited;
            BSEvents.gameSceneActive -= OnSongStarted;
            BSEvents.songPaused -= OnGamePause;
            BSEvents.songUnpaused -= OnGameResume;

            if (HotkeyScreen != null)
            {
                GameObject.Destroy(HotkeyScreen.gameObject);
                HotkeyScreen = null;
            }
        }

        private void BSEvents_earlyMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {
            if (HotkeyScreen != null)
            {
                GameObject.Destroy(HotkeyScreen.gameObject);
                HotkeyScreen = null;
            }
        }

        public void ShowHotkeyWindow()
        {
            if (HotkeyScreen == null)
            {
                HotkeyScreen = CreateFloatingScreen();
                FloatingButton = BeatSaberUI.CreateViewController<FloatingButton>();
                FloatingButton.ParentCoordinator = this;
                HotkeyScreen.SetRootViewController(FloatingButton, HMUI.ViewController.AnimationType.None);
                AttachEvents();
                FloatingButton.spawn();
            }
            HotkeyScreen.gameObject.SetActive(true);
        }

        public FloatingScreen CreateFloatingScreen()
        {
            int textwidth = PluginConfig.Instance.ButtonText.Length;
            FloatingScreen screen = FloatingScreen.CreateFloatingScreen(
                new Vector2(20+textwidth, 10), false,
                PluginConfig.Instance.ScreenPos,
                PluginConfig.Instance.ScreenRot);

            screen.HandleReleased -= OnRelease;
            screen.HandleReleased += OnRelease;

            GameObject.DontDestroyOnLoad(screen.gameObject);
            return screen;
        }

        private void AttachEvents()
        {
            BSEvents.menuSceneActive += OnSongExited;
            BSEvents.gameSceneActive += OnSongStarted;
            BSEvents.songPaused += OnGamePause;
            BSEvents.songUnpaused += OnGameResume;
        }

        private void OnRelease(object _, FloatingScreenHandleEventArgs posRot)
        {
            Vector3 newPos = posRot.Position;
            Quaternion newRot = posRot.Rotation;

            PluginConfig.Instance.ScreenPos = newPos;
            PluginConfig.Instance.ScreenRot = newRot;
        }

        private void SetVisibility(bool visibility)
        {
            if (HotkeyScreen != null)
            {
                HotkeyScreen.gameObject.SetActive(visibility);
            }
        }

        private void OnSongExited()
        {
            SetVisibility(true);
        }
        private void OnSongStarted()
        {
            SetVisibility(false);
        }
        private void OnGamePause()
        {
            SetVisibility(true);
        }
        private void OnGameResume()
        {
            SetVisibility(false);
        }

    }
}
