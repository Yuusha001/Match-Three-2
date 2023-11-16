using NaughtyAttributes;
using System;
using UnityEngine;

namespace MatchThree
{
    public class CharacterGoal : MonoBehaviour
    {
        [ReadOnly]
        [SerializeField]
        private TMPro.TextMeshProUGUI textGoalPrefab;
        [ReadOnly]
        [SerializeField]
        private FruitGoal fruitGoalPrefab;
        [ReadOnly]
        [SerializeField]
        private Transform container;

        public void Initialize(LevelDifficulty levelDifficulty)
        {
            var collectTypes = levelDifficulty.collectTypes;
            var starsRequiredPoints = levelDifficulty.starsRequiredPoints;
            switch (levelDifficulty.gameType)
            {
                case EGameType.None:
                    Instantiate(textGoalPrefab, container).text = starsRequiredPoints[starsRequiredPoints.Length-1].ToString();
                    break;
                case EGameType.Collect:
                    if (collectTypes.Length == 1)
                    {
                        Instantiate(fruitGoalPrefab, container).Initialize(collectTypes[0]);
                    }
                    if (collectTypes.Length == 2)
                    {
                        Instantiate(fruitGoalPrefab, container).Initialize(collectTypes[0]);
                        Instantiate(textGoalPrefab, container).text = "+";
                        Instantiate(fruitGoalPrefab, container).Initialize(collectTypes[1]);
                    }
                    /*if (collectTypes.Length == 3)
                    {
                        foreach (var item in collectTypes)
                        {
                            Instantiate(fruitGoalPrefab, container).Initialize(item);
                        }
                    }*/
                    break;
                default:
                    break;
            }
            GameManager.OnMatch += MachingHandler;
        }

        [Button("Clear")]
        public void DeInitialize()
        {
            GameManager.OnMatch -= MachingHandler;

            for (int i = container.childCount - 1; i >= 0; i--)
            {
                Destroy(container.GetChild(i).gameObject);
            }

        }

        private void MachingHandler(TileTypeAsset asset, int arg2)
        {
           
        }

        
    }
}
