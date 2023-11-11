using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class GameManager : Singleton<GameManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] 
        private static void Initialize() => Application.targetFrameRate = 60;
        public int currentPoint { get; private set; }
    }
}