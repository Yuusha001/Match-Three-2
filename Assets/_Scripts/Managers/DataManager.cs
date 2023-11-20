using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Application.targetFrameRate = 60;
        }
    }
}