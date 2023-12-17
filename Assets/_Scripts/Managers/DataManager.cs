using NaughtyAttributes;
using UnityEngine;

namespace MatchThree
{
    public class DataManager : Singleton<DataManager>
    {
        [ReadOnly]
        public Sprite[] boardSprites;
        [ReadOnly]
        public Sprite[] blockSprites;
        [ReadOnly]
        public Sprite[] rockSprites;
        [ReadOnly]
        public Sprite iceSprites;
        [ReadOnly]
        public Sprite thrivingSprites;
        [ReadOnly] 
        public Theme[] themes;
        [ReadOnly]
        public SpecialTileTypeAsset[] specialTileTypeAssets;
        [ReadOnly]
        public UserData userData;
        public static int Coin
        {
            get { return PlayerPrefs.GetInt("coin",0); }
            set { PlayerPrefs.SetInt("coin", value); }
        }

        public static int Diamond
        {
            get { return PlayerPrefs.GetInt("diamond",0); }
            set { PlayerPrefs.SetInt("diamond", value); }
        }

        public static int Life
        {
            get { return PlayerPrefs.GetInt("life",0); }
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