using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class MenuScreenUI : ScreenUI
    {
        [ReadOnly]
        [SerializeField]
        private TopBar topBar;
        [ReadOnly]
        [SerializeField]
        private UnityEngine.UI.Button settingBtn;
        [ReadOnly]
        public LevelNode levelNodePrefab;
        [ReadOnly]
        public Transform endNodePrefab;
        [ReadOnly]
        [SerializeField]
        private Transform levelHolder;
        [ReadOnly]
        [SerializeField]
        private List<LevelNode> levelNodes = new List<LevelNode>();
        
        public override void Initialize(UIManager uiManager)
        {
            base.Initialize(uiManager);
            settingBtn.onClick.AddListener(SettingHandler);
            GameManager.OnStartGame += Deactive;
            GameManager.OnQuitGame += Active;
            LevelCreate();
        }

        private void LevelCreate()
        {
            var userData = DataManager.Instance.userData;
            List<UserLevelData> datas = new List<UserLevelData>();     
            for (int i = 0; i < userData.TotalLevels(); i++)
            {
                datas.Add(userData.userLevelDatas[i]);
                if (i > 0 && (i + 1) % 8 == 0)
                {
                    var node = Instantiate(levelNodePrefab, levelHolder);
                    node.transform.SetAsFirstSibling();
                    node.Initialize(datas, DataManager.Instance.themes[Random.Range(0,3)]);
                    levelNodes.Add(node);
                    datas.Clear();
                }
            }
            var endNode = Instantiate(endNodePrefab, levelHolder);
            endNode.SetAsFirstSibling();
        }

        public override void Active()
        {
            base.Active();
            foreach (var item in levelNodes)
            {
                item.Unlock();
            }
        }

        private void SettingHandler()
        {
            PopupManager.Instance.GetPopup<SettingPopup>().Show();
        }

        private void OnDestroy()
        {
            GameManager.OnStartGame -= Deactive;
            GameManager.OnQuitGame -= Active;
        }
    }
}
