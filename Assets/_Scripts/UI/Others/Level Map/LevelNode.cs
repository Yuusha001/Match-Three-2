using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
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
        private Image decoImg;
        [ReadOnly]
        [SerializeField]
        private Transform levelBtnContainer;
        [ReadOnly]
        [SerializeField]
        private List<LevelBtn> levelBtns;
        public void Initialize(List<UserLevelData> levels, Theme theme, bool isNode_1 = true)
        {
            groundImg.sprite = isNode_1 ? theme.groundSprites[0]: theme.groundSprites[1];
            decoImg.sprite = isNode_1 ? theme.decoSprites[0]: theme.decoSprites[1];
            levelBtns = new List<LevelBtn>();
            for (int i = 0; i < levels.Count; i++)
            {
                LevelBtn GO = Instantiate(theme.levelBtnPrefab, levelBtnContainer);
                GO.name = levels[i].id.ToString();
                GO.GetComponent<RectTransform>().localPosition = isNode_1? theme.levelBtnPositions[i]: theme.levelBtnPositions2[i];
                GO.Initialize(levels[i]);
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
