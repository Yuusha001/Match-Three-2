using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MatchThree
{
    public class LevelBtn : MonoBehaviour
    {
        [SerializeField]
        private Transform[] starVisuals;
        [SerializeField]
        private Transform starsContainer;
        [SerializeField]
        private TextMeshProUGUI levelTxt;
        [SerializeField]
        private Color unlockedColor;

        public void Initialize(Level level, UnityAction action)
        {
            this.GetComponent<Button>().onClick.AddListener(action);
            Settup(level);

        }
        public void Settup(Level level)
        {
            this.GetComponent<Image>().color = level.unlocked ? unlockedColor: Color.white;
            levelTxt.text = level.level.ToString();
            starsContainer.gameObject.SetActive(level.unlocked);
            if (level.unlocked)
            {
                for (int i = 0; i < starVisuals.Length; i++)
                {
                    starVisuals[i].gameObject.SetActive(i < level.numberOfStars);
                }
            }
        }
    }
}
