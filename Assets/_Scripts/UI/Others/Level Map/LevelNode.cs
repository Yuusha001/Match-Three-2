using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchThree
{
    public class LevelNode : MonoBehaviour
    {
        [SerializeField]
        private Image groundImg;

        [SerializeField]
        private Image decoImg;

        [SerializeField]
        private Transform levelBtnContainer;
        public void Initialize(Level[] levels, Theme theme, bool isNode_1 = true)
        {
            groundImg.sprite = isNode_1 ? theme.groundSprites[0]: theme.groundSprites[1];
            decoImg.sprite = isNode_1 ? theme.decoSprites[0]: theme.decoSprites[1];

            for (int i = 0; i < levels.Length; i++)
            {
                LevelBtn GO = Instantiate(theme.levelBtnPrefab, levelBtnContainer);
                GO.GetComponent<RectTransform>().localPosition = isNode_1? theme.levelBtnPositions[i]: theme.levelBtnPositions2[i];
                GO.Initialize(levels[i], () =>
                {
                    //Load level
                });
            }
        }
    }
}
