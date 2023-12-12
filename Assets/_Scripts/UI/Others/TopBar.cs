using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace MatchThree
{
    public class TopBar : Singleton<TopBar>
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
            lifeTxt.text = $"{DataManager.Life}/5";
            diamondTxt.text = DataManager.Diamond.ToString();
            coinTxt.text = DataManager.Coin.ToString();
        }

        public void AddCoin(int coin)
        {
            DataManager.Coin += coin;
            coinUI_VFX.PlayFx(() => {
                coinVFX.Play();
                coinTxt.text = DataManager.Coin.ToString();
            });
           
        }

        public void AddDiamond(int diamond)
        {
            DataManager.Diamond += diamond;
            diamondUI_VFX.PlayFx(() => { 
                diamondVFX.Play();
                diamondTxt.text = DataManager.Diamond.ToString();
            });
            
        }
    }
}
