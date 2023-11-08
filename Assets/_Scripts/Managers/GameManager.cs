using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class GameManager : Singleton<GameManager>
    {
        public int currentPoint { get; private set; }
    }
}