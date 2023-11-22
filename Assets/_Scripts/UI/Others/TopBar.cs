using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class TopBar : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private TMPro.TextMeshProUGUI coinTxt;
        [ReadOnly]
        [SerializeField]
        private TMPro.TextMeshProUGUI diamondTxt;
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
            diamondTxt.text = DataManager.Diamond.ToString();
            coinTxt.text = DataManager.Coin.ToString();
        }
    }
}
