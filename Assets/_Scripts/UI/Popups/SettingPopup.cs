using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utils;

namespace MatchThree
{
    public class SettingPopup : PopupUI
    {

        [SerializeField]
        private TextMeshProUGUI titleTxt;
        [SerializeField]
        private ToggleBtn musicBtn;
        [SerializeField]
        private ToggleBtn sfxBtn;
        [SerializeField]
        private Button replayBtn;
        [SerializeField]
        private Button quitBtn;
        [SerializeField]
        private Button closeBtn;
        [SerializeField]
        private Button creditBtn;
        public override void Initialize(PopupManager popupManager, System.Action onClosed = null)
        {
            base.Initialize(popupManager, CloseHandler);
            musicBtn.Initialize(AudioManager.MusicSetting == 1,MusicHandler);
            sfxBtn.Initialize(AudioManager.SoundSetting == 1, SFXHandler);
            replayBtn.onClick.AddListener(ReplayHander);
            quitBtn.onClick.AddListener(QuitHandler);
            closeBtn.onClick.AddListener(Close);
            creditBtn.onClick.AddListener(CreditHandler);
            titleTxt.text = "Setting";
        }

        

        public void Show(bool inGame = false)
        {
            base.Show();
            titleTxt.text = inGame ? "Pause" : "Setting";
            quitBtn.gameObject.SetActive(inGame);
            replayBtn.gameObject.SetActive(inGame);
            creditBtn.gameObject.SetActive(!inGame);
        }

        private void CloseHandler()
        {
            titleTxt.text = "Setting";
        }

        private void MusicHandler()
        {
            if (musicBtn == null) return;
            LogManager.Instance.Log("Music Toggle",this);
            AudioManager.Instance.MusicHandler();
        }

        private void SFXHandler()
        {
            if (sfxBtn == null) return;
            LogManager.Instance.Log("SFX Toggle",this);
            AudioManager.Instance.SFXHandler();
        }

        private void ReplayHander()
        {
            LogManager.Instance.Log("Replay Handler",this);
            Close();
            GameManager.Instance.ReloadLevel();
        }

        private void QuitHandler()
        {
            LogManager.Instance.Log("Quit Handler", this);
            Close();
            GameManager.Instance.QuitGame();
        }

        private void CreditHandler()
        {
            _popupManager.ShowPopup<CreditPopup>();
        }
    }
}
