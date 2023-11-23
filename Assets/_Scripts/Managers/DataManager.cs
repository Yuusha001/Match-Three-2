using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchThree
{
    public class DataManager : Singleton<DataManager>
    {
        [ReadOnly]
        public Sprite[] boardSprites;
        [ReadOnly]
        public Sprite[] borderSprites;
        [ReadOnly]
        public Sprite[] blockSprites;
        [ReadOnly]
        public Sprite[] rockSprites;
        [ReadOnly]
        public Sprite iceSprites;
        [ReadOnly] 
        public Theme[] themes;
        [ReadOnly]
        public UserData userData;
        public static int Coin
        {
            get { return PlayerPrefs.GetInt("coin"); }
            set { PlayerPrefs.SetInt("coin", value); }
        }

        public static int Life
        {
            get { return PlayerPrefs.GetInt("life"); }
            set { PlayerPrefs.SetInt("life", value); }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Application.targetFrameRate = 60;
        }

        [Button("Load All Themes")]
        private void LoadThemes()
        {
            themes = Resources.LoadAll<Theme>("_SO/Themes");
        }
    }
}