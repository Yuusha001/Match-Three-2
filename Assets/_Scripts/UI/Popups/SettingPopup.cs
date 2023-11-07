using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace MatchThree
{
    public class SettingPopup : PopupUI
    {
        public TextMeshProUGUI title;
        public ToggleBtn musicBtn;
        public ToggleBtn sfxBtn;
        public override void Initialize(PopupManager popupManager, Action onClosed = null)
        {
            base.Initialize(popupManager, onClosed);
            title.text = "Pause";
            musicBtn.Initialize(() => { Debug.Log("Music Toggle"); });
            sfxBtn.Initialize(() => { Debug.Log("SFX Toggle"); });
        }
    }
}
