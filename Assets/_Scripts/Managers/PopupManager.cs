using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

namespace MatchThree
{
    public class PopupManager : Singleton<PopupManager>
    {
        [ReadOnly]
        public PopupUI[] popups;
        public T GetPopup<T>()
        {
            T popup = default;
            for (int i = 0; i < popups.Length; i++)
            {
                if (popups[i] is T)
                {
                    popup = popups[i].GetComponent<T>();
                }
            }
            return popup;
        }
        public T ShowPopup<T>()
        {
            T popup = default;
            for (int i = 0; i < popups.Length; i++)
            {
                if (popups[i] is T)
                {
                    popups[i].Show();
                    popup = popups[i].GetComponent<T>();
                }
            }
            return popup;
        }
        public T ClosePopup<T>()
        {
            T popup = default;
            for (int i = 0; i < popups.Length; i++)
            {
                if (popups[i] is T)
                {
                    popups[i].Close();
                    popup = popups[i].GetComponent<T>();
                }
            }
            return popup;
        }

        [Button("Get All Popups")]
        private void GetAllPopups()
        {
            popups = FindObjectsOfType<PopupUI>(true);
        }

        private void Start()
        {
            foreach (var popup in popups)
            {
                popup.Initialize(this);
            }
        }
    }
}