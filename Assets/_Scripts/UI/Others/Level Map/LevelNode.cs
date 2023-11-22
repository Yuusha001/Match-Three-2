using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree
{
    public class LevelNode : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private Image groundImg;
        [ReadOnly]
        [SerializeField]
        private Transform levelBtnContainer;
        [ReadOnly]
        [SerializeField]
        private List<LevelBtn> levelBtns;
        public void Initialize(List<UserLevelData> levels, Theme theme)
        {
            groundImg.sprite = theme.levelGround;
            levelBtns = new List<LevelBtn>();
            for (int i = 0; i < levels.Count; i++)
            {
                LevelBtn GO = Instantiate(theme.levelBtnPrefab, levelBtnContainer);
                GO.name = levels[i].id.ToString();
                GO.GetComponent<RectTransform>().localPosition = theme.btnPositions[i];
                GO.Initialize(levels[i].id, theme.ID);
                levelBtns.Add(GO);
            }
        }

        public void Unlock()
        {
            foreach (var item in levelBtns)
            {
                item.Unlock();
            }
        }
    }
}
