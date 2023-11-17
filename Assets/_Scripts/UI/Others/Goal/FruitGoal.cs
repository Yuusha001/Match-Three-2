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
        [ReadOnly]
        public int fruitID;

        public void Initialize(CollectType collectType)
        {
            fruitID = collectType.tileType.id;
            fruitImg.sprite = collectType.tileType.sprite;
            fruitTxt.text = collectType.numberOfCollects.ToString();
        }

        public void Collect(int num)
        {
            fruitTxt.text = num < 0 ? "0" : num.ToString();
        }

        public void DeInitialize()
        {
            fruitImg.sprite = null;
            fruitTxt.text = "";
        }
    }
}
