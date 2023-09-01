using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoverHotkey.Configuration;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Util;
using TMPro;

namespace HoverHotkey.UI
{
    public class ConfigView : PersistentSingleton<ConfigView>
    {

        private PluginConfig _settings = PluginConfig.Instance;

        [UIComponent("togglescreenhandlebtn")]
        private TextMeshProUGUI toggleScreenHandleBtnText;

        [UIAction("togglescreenhandle")]
        protected void ClickToggleButtonAction()
        {
            if (FloatingButtonController.Instance.HotkeyScreen != null)
            {
                Plugin.Log.Debug("Toggle screen handle visibility");
                bool oldstate = FloatingButtonController.Instance.HotkeyScreen.ShowHandle;
                FloatingButtonController.Instance.HotkeyScreen.ShowHandle = !oldstate;
                if (oldstate)
                {
                    toggleScreenHandleBtnText.text = "Show movement handle";
                }
                else
                {
                    toggleScreenHandleBtnText.text = "Hide movement handle";
                }
            }
        }
    }
}
