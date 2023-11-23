using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace MatchThree
{
    public class TopBar : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private TextMeshProUGUI coinTxt;
        [ReadOnly]
        [SerializeField]
        private TextMeshProUGUI diamondTxt;
        [ReadOnly]
        [SerializeField]
        private TextMeshProUGUI lifeTxt;
        [ReadOnly]
        [SerializeField]
        private ParticleSystem coinVFX;
        [ReadOnly]
        [SerializeField]
        private ParticleSystem diamondVFX;
        [ReadOnly]
        public CollectVFX coinUI_VFX;
        [ReadOnly]
        public CollectVFX diamondUI_VFX;
        public void Initialize()
        {
            lifeTxt.text = DataManager.Life.ToString();
            coinTxt.text = DataManager.Coin.ToString();
        }
    }
}
