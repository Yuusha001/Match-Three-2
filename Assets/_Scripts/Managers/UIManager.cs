using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree
{
    public class UIManager : Singleton<UIManager>
    {
        [ReadOnly]
        public ScreenUI[] screens;
        public T GetScreen<T>()
        {
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i] is T)
                {
                    return screens[i].GetComponent<T>();
                }
            }
            return default;
        }
        public T ActiveScreen<T>()
        {
            T screen = default;
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i] is T)
                {
                    screens[i].Active();
                    screen = screens[i].GetComponent<T>();
                }
                else
                {
                    screens[i].Deactive();
                }
            }
            return screen;
        }

        public T DeactiveScreen<T>()
        {
            T screen = default;
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i] is T)
                {
                    screens[i].Deactive();
                    screen = screens[i].GetComponent<T>();
                }
                else
                {
                    screens[i].Deactive();
                }
            }
            return screen;
        }

        private void Start()
        {
            foreach (var screen in screens)
            {
                screen.Initialize(this);
            }
        }


        [Button("Get All Screen")]
        private void GetAllScreens()
        {
            screens = FindObjectsOfType<ScreenUI>(true);
        }
    }
}