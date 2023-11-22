using DG.Tweening;
using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree
{
    public class LevelBtn : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private Transform[] starVisuals;
        [ReadOnly]
        [SerializeField]
        private Transform starsContainer;
        [ReadOnly]
        [SerializeField]
        private TMPro.TextMeshProUGUI levelTxt;
        [ReadOnly]
        [SerializeField]
        private Button levelBtn;
        [ReadOnly]
        [SerializeField]
        private Image levelBtnSprite;
        [ReadOnly]
        [SerializeField]
        private Transform lockContainer;
        [ReadOnly]
        [SerializeField]
        private Transform rayContainer;

        private int ID;
        private int themeID;
        public void Initialize(int _levelID, int _themeID)
        {
            ID = _levelID;
            themeID = _themeID;
            levelBtn.onClick.AddListener(LevelBtnHandler);
            rayContainer.DORotate(new Vector3(0, 0, 360), 3f).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            lockContainer.GetChild(1).DORotate(new Vector3(0, 0, 5), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
            Unlock();
        }

        private void LevelBtnHandler()
        {
            if (levelBtn == null) return;
            UserLevelData levelData = DataManager.Instance.userData.GetUserLevelData(this.ID);
            if (levelData == null || !levelData.isUnlocked()) return;
            LevelManager.Instance.LoadLevel(ID);
        }
        public void Unlock()
        {
            Theme theme = DataManager.Instance.themes.FirstOrDefault(x => x.ID == themeID);
            UserLevelData levelData = DataManager.Instance.userData.GetUserLevelData(this.ID);
            levelTxt.text = levelData.id.ToString();
       
            switch (levelData.levelState)
            {
                case ELevelState.Locked:
                    //levelBtn.interactable = false;
                    rayContainer.gameObject.SetActive(false);
                    lockContainer.gameObject.SetActive(true);
                    starsContainer.gameObject.SetActive(false);
                    levelBtnSprite.sprite = theme.lockBtn;
                    break;
                case ELevelState.Unlocked:
                    //levelBtn.interactable = true;
                    rayContainer.gameObject.SetActive(true);
                    lockContainer.gameObject.SetActive(false);
                    starsContainer.gameObject.SetActive(true);
                    levelBtnSprite.sprite = theme.unlockBtn;
                    break;
                case ELevelState.Played:
                    //levelBtn.interactable = true;
                    rayContainer.gameObject.SetActive(false);
                    lockContainer.gameObject.SetActive(false);
                    starsContainer.gameObject.SetActive(true);
                    levelBtnSprite.sprite = theme.playedBtn;
                    break;
            }
            for (int i = 0; i < starVisuals.Length; i++)
            {
                starVisuals[i].gameObject.SetActive(i < levelData.numberOfStars);
            }
        }
    }
}
