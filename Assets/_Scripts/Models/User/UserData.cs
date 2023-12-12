using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    [System.Serializable]
    public class UserData 
    {
        public List<UserLevelData> userLevelDatas;
        public UserData(int totalLV)
        {
            userLevelDatas = new List<UserLevelData>();
            for (int i = 0; i < totalLV; i++)
            {
                UserLevelData data = new UserLevelData(i);
                if (i == 0)
                {
                    data.UnlockLevel();
                }
                userLevelDatas.Add(data);
            }
        }

        public UserLevelData GetUserLevelData(int ID)
        {
            return userLevelDatas[ID];
        }

        public int TotalLevels()
        {
            return userLevelDatas.Count;
        }

        public  void LoadData(int totalLV)
        {
            string pref = PlayerPrefs.GetString("user_data");
            if (!string.IsNullOrEmpty(pref))
            {
                Debug.Log(pref);
                DataManager.Instance.userData = JsonUtility.FromJson<UserData>(pref);
            }
            else
            {
                DataManager.Instance.userData = new UserData(totalLV);
            }
        }
        public void SaveUserData()
        {
            string dataJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("user_data", dataJson);
        }
    }
}
