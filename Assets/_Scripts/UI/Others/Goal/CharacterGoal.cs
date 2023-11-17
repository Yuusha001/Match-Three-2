using NaughtyAttributes;
using System.Linq;
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
        private FruitGoal[] fruitGoals;

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
                    fruitGoals = new FruitGoal[levelDifficulty.collectTypes.Length];
                    for (int i = 0; i < fruitGoals.Length; i++)
                    {
                        fruitGoals[i] = Instantiate(fruitGoalPrefab, container);
                        fruitGoals[i].Initialize(collectTypes[i]);
                    }
                    if (collectTypes.Length == 2)
                    {
                        Instantiate(textGoalPrefab, container).text = "+";
                        fruitGoals[fruitGoals.Length - 1].transform.SetAsLastSibling();
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
            var collect = GameManager.Instance.collects.FirstOrDefault(Item => Item.tileType == asset);
            var own = fruitGoals.FirstOrDefault(Item => Item.fruitID == asset.id);
            if (own != null)
            {
                own.Collect(collect.numberOfCollects);
            }
        }
    }
}
