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
        public Theme[] themes;
        [ReadOnly]
        public UserData userData;
        public static int Coin
        {
            get { return PlayerPrefs.GetInt("coin"); }
            set { PlayerPrefs.SetInt("coin", value); }
        }

        public static int Diamond
        {
            get { return PlayerPrefs.GetInt("diamond"); }
            set { PlayerPrefs.SetInt("diamond", value); }
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