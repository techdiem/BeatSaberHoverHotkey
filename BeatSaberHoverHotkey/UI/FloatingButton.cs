using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using HoverHotkey.Configuration;

namespace HoverHotkey.UI
{
    [HotReload(@"FloatingButton.bsml")]
    public partial class FloatingButton : BSMLAutomaticViewController
    {
        internal FloatingButtonController ParentCoordinator;

        [UIComponent("hotkeybtn")]
        private TextMeshProUGUI hotkeybtntext;
        

        [UIAction("hotkeytrigger")]
        protected void ClickButtonAction()
        {
            Plugin.Log.Debug("Hotkey button pressed!");
            HotkeySender.PressHotkey(PluginConfig.Instance.Hotkey);
        }

        public void spawn()
        {
            hotkeybtntext.text = PluginConfig.Instance.ButtonText;
        }
    }
}
