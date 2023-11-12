using NaughtyAttributes;
using UnityEngine;

namespace MatchThree
{
    public class FruitGoal : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private UnityEngine.UI.Image fruitImg;
        [ReadOnly]
        [SerializeField]
        private TMPro.TextMeshProUGUI fruitTxt;

        public void Initialize(CollectType collectType)
        {
            fruitImg.sprite = collectType.tileType.sprite;
            fruitTxt.text = collectType.numberOfCollects.ToString();
        }

        public void DeInitialize()
        {
            fruitImg.sprite = null;
            fruitTxt.text = "";
        }
    }
}
