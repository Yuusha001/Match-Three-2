
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class LogManager : Singleton<LogManager>
    {
        public List<Logger> Logger;
#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var item in Logger)
            {
                item._hexColor = "#" + ColorUtility.ToHtmlStringRGB(item.prefixColor);
            }
        }
#endif

        public void Log(object message, Object sender = null)
        {
#if UNITY_EDITOR
            Logger logger = Logger.Find((e) => e.sender.name.Equals(sender.name));
            if (logger == null)
            {
                Debug.Log(message, sender);
                return;
            }
            if (!logger.showLogs) return;
            Debug.Log($"<color={logger._hexColor}>{message}</color>", sender);
#endif
        }

    }
    [System.Serializable]
    public class Logger
    {
        public bool showLogs;
        public Color prefixColor;
        public Object sender;
        public string _hexColor;
    }
}
