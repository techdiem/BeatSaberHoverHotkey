
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;
using HoverHotkey.UI;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace HoverHotkey.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual string ButtonText { get; set; } = "Hotkey";
        //List of keycodes: https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        public virtual short Hotkey { get; set; } = 0x71;
        public virtual Vector3 ScreenPos { get; set; } = new Vector3(-1.74f, 0.7f, 2f);
        public virtual Quaternion ScreenRot { get; set; } = Quaternion.Euler(25f, 330f, 6.5f);

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            if (Plugin.startupReady)
            {
                FloatingButtonController.Instance.ShowHotkeyWindow();
            }
            else if (FloatingButtonController.Instance.HotkeyScreen != null)
            {
                FloatingButtonController.Instance.Cleanup();
            }
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}
