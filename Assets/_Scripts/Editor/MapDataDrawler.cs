using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MatchThree
{
    [CustomEditor(typeof(LevelDifficulty), false)]
    [CanEditMultipleObjects]
    [System.Serializable]
    public class MapDataDrawler : Editor
    {
        private LevelDifficulty instance => target as LevelDifficulty;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawBoardTable();
        }
        private void DrawBoardTable()
        {
            var tableStyle = new GUIStyle("box");
            tableStyle.padding = new RectOffset(10, 10, 10, 10);
            tableStyle.margin.left = 32;

            var headerColumnStyle = new GUIStyle();
            headerColumnStyle.alignment = TextAnchor.MiddleCenter;

            var rowStyle = new GUIStyle();
            rowStyle.alignment = TextAnchor.MiddleCenter;

            var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
            dataFieldStyle.normal.background = Texture2D.grayTexture;
            dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

            GUILayout.BeginVertical(tableStyle);

            for (var row = 0; row < instance.mapData.numberOfRows; row++)
            {
                GUILayout.BeginHorizontal(headerColumnStyle);

                for (var column = 0; column < instance.mapData.numberOfCols; column++)
                {
                    GUILayout.BeginHorizontal(rowStyle);

                    instance.mapData.Data[row, column] = (EMapType)EditorGUILayout.EnumPopup(
                        instance.mapData.Data[row, column],
                        dataFieldStyle
                    );

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }
    }
}
