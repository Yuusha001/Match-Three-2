using NaughtyAttributes;
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
        private Color unlockedColor;
        [ReadOnly]
        [SerializeField]
        private Button levelBtn;
        private int ID;
        public void Initialize(UserLevelData levelData)
        {
            ID = levelData.id;
            levelBtn.onClick.AddListener(LevelBtnHandler);
            Settup(levelData);

        }

        private void LevelBtnHandler()
        {
            if (levelBtn == null) return;
            LevelManager.Instance.LoadLevel(ID);
        }

        public void Settup(UserLevelData levelData)
        {
            this.GetComponent<Image>().color = levelData.unlocked ? unlockedColor: Color.white;
            levelTxt.text = levelData.id.ToString();
            Unlock();
        }
        public void Unlock()
        {
            UserLevelData levelData = DataManager.Instance.userData.GetUserLevelData(this.ID);
            starsContainer.gameObject.SetActive(levelData.unlocked);
            levelBtn.interactable = levelData.unlocked;
            if (levelData.unlocked)
            {
                for (int i = 0; i < starVisuals.Length; i++)
                {
                    starVisuals[i].gameObject.SetActive(i < levelData.numberOfStars);
                }
            }
        }
    }
}
