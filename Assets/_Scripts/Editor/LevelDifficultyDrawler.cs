using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.PlayerSettings;

namespace MatchThree
{
    [CustomEditor(typeof(LevelDifficulty), false)]
    [CanEditMultipleObjects]
    [System.Serializable]
    public class LevelDifficultyDrawler : Editor
    {
        private LevelDifficulty instance => target as LevelDifficulty;
        private Texture squareTex;
        private Texture blockTex;
        private Texture blockTex2;
        private Texture wireBlockTex;
        private Texture solidBlockTex;
        private Texture undestroyableBlockTex;
        private Texture thrivingBlockTex;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawBoardTable();
        }
        private void DrawBoardTable()
        {
            squareTex = DataManager.Instance.boardSprites[0].texture;
            blockTex = DataManager.Instance.blockSprites[0].texture;
            blockTex2 = DataManager.Instance.blockSprites[1].texture;
            wireBlockTex = DataManager.Instance.iceSprites.texture;
            solidBlockTex = DataManager.Instance.rockSprites[0].texture;
            undestroyableBlockTex = DataManager.Instance.rockSprites[1].texture;
            thrivingBlockTex = DataManager.Instance.thrivingSprites.texture;
            GUIBlocks();
            GUIGameField();
        }

        void GUIBlocks()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();

            GUILayout.Label("Tools:", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            if (GUILayout.Button("Clear", new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
            {
                for (int i = 0; i < instance.Data.Length; i++)
                {
                    instance.Data[i].block = EMapType.Empty;
                    instance.Data[i].obstacle = EMapType.Normal;
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();


            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();

            GUILayout.Label("Blocks:", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            GUI.color = new Color(1, 1, 1, 1f);
            if (GUILayout.Button(squareTex, new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
            {
               instance.squareType = EMapType.Empty;
            }

            GUILayout.Label(" - normal", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            //        if (target == Target.BLOCKS)
            {
                GUILayout.BeginHorizontal();

                GUI.color = new Color(0.8f, 1, 1, 1f);
                if (GUILayout.Button(blockTex, new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
                {
                    instance.squareType = EMapType.Block_1;

                }

                GUILayout.Label(" - block /\n  double click x2", EditorStyles.boldLabel);
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            GUI.color = new Color(1, 1, 1, 1f);

            if (GUILayout.Button("X", new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
            {
                instance.squareType = EMapType.Normal;
            }

            GUILayout.Label(" - none", EditorStyles.boldLabel);

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button(thrivingBlockTex, new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
            {
                instance.squareType = EMapType.Thriving;

            }

            GUILayout.Label("-thriving\n block", EditorStyles.boldLabel);

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            GUIStyle style = new GUIStyle();

            if (GUILayout.Button(wireBlockTex, new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
            {
                instance.squareType = EMapType.Frozen;
            }

            GUILayout.Label(" - wire block", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button(solidBlockTex, new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
            {
                instance.squareType = EMapType.Rock_1;

            }

            GUILayout.Label(" - solid block", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(undestroyableBlockTex, new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) }))
            {
                instance.squareType = EMapType.Rock_2;

            }

            GUILayout.Label("-undestroyable\n block", EditorStyles.boldLabel);

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

        }

        void GUIGameField()
        {
            GUILayout.BeginVertical();
            for (int row = 0; row < instance.numberOfRows; row++)
            {
                GUILayout.BeginHorizontal();

                for (int col = 0; col < instance.numberOfCols; col++)
                {
                    Color squareColor = new Color(0.8f, 0.8f, 0.8f);

                    var imageButton = new object();
                    if (instance.Data[row * instance.numberOfCols + col].block == EMapType.Normal)
                    {
                        imageButton = "X";
                        squareColor = new Color(0.8f, 0.8f, 0.8f);
                    }
                    else if (instance.Data[row * instance.numberOfCols + col].block == EMapType.Empty)
                    {
                        imageButton = squareTex;
                        squareColor = Color.white;
                        if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Frozen)
                        {
                            imageButton = wireBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Rock_1)
                        {
                            imageButton = solidBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Rock_2)
                        {
                            imageButton = undestroyableBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Thriving)
                        {
                            imageButton = thrivingBlockTex;
                            squareColor = Color.white;
                        }

                    }
                    else if (instance.Data[row * instance.numberOfCols + col].block == EMapType.Block_1)
                    {
                        imageButton = blockTex;
                        if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Frozen)
                        {
                            imageButton = wireBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Rock_1)
                        {
                            imageButton = solidBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Rock_2)
                        {
                            imageButton = undestroyableBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Thriving)
                        {
                            imageButton = thrivingBlockTex;
                            squareColor = Color.white;
                        }
                        //     squareColor = new Color(0.8f, 1, 1, 1f);
                    }
                    else if (instance.Data[row * instance.numberOfCols + col].block == EMapType.Block_2)
                    {
                        imageButton = blockTex2;
                        if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Frozen)
                        {
                            imageButton = wireBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Rock_1)
                        {
                            imageButton = solidBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Rock_2)
                        {
                            imageButton = undestroyableBlockTex;
                            squareColor = Color.white;
                        }
                        else if (instance.Data[row * instance.numberOfCols + col].obstacle == EMapType.Thriving)
                        {
                            imageButton = thrivingBlockTex;
                            squareColor = Color.white;
                        }
                        // squareColor = new Color(0.3f, 1, 1, 1f);
                    }
                    GUI.color = squareColor;
                    if (GUILayout.Button(imageButton as Texture, new GUILayoutOption[] {
                        GUILayout.Width (50),
                        GUILayout.Height (50)
                    }))
                        {
                            SetType(col, row);
                        }
                    }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

        void SetType(int col, int row)
        {
            if (instance.squareType == EMapType.Block_1)
            {
                if (instance.Data[row * instance.numberOfCols + col].block == EMapType.Block_1)
                    instance.Data[row * instance.numberOfCols + col].block = EMapType.Block_2;
                else
                    instance.Data[row * instance.numberOfCols + col].block = EMapType.Block_1;
            }
            else if (instance.squareType == EMapType.Frozen || instance.squareType == EMapType.Rock_1 || instance.squareType == EMapType.Rock_2 || instance.squareType == EMapType.Thriving)
                instance.Data[row * instance.numberOfCols + col].obstacle = instance.squareType;
            else
            {
                instance.Data[row * instance.numberOfCols + col].block = instance.squareType;
                instance.Data[row * instance.numberOfCols + col].obstacle = EMapType.Normal;
            }
        }
    }
}
