using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class MenuScreenUI : ScreenUI
    {
        [ReadOnly]
        [SerializeField]
        private UnityEngine.UI.Button settingBtn;
        [ReadOnly]
        public LevelNode levelNodePrefab;
        [ReadOnly]
        [SerializeField]
        private Transform levelHolder;
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
                if (i > 0 && (i + 1) % 5 == 0)
                {
                    var node = Instantiate(levelNodePrefab, levelHolder);
                    node.transform.SetAsFirstSibling();
                    node.Initialize(datas, DataManager.Instance.themes[0]);
                    levelNodes.Add(node);
                    datas.Clear();
                }
            }
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
