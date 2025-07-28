using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    private int level;
    private LevelData levelData;
    private ColorIndex selectedColor = ColorIndex.Red;
    
    private SerializedObject _serializedLevelData;
    private SerializedProperty _busColorsProp;
    private Vector2 scrollPos;
    private int validLevel;

    private List<LevelData> allLevels = new List<LevelData>();
    
    [MenuItem("Window/Level Editor")]
    public static void OpenWindow()
    {
        GetWindow<LevelEditor>("Level Editor");
    }
    
    private void OnEnable()
    {
        FindLevelData();
    }
    
    private void FindLevelData()
    {
        allLevels.Clear();
        var allGuids = AssetDatabase.FindAssets("LevelData t:LevelData");
        
        foreach (var guid in allGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

            if (fileName.StartsWith("LevelData"))
            {
                var level = AssetDatabase.LoadAssetAtPath<LevelData>(path);
                if (level != null)
                {
                    allLevels.Add(level);
                }
            }
        }
    }
    
    private void CreateLevelDataAsset(string name)
    {
        string path = "Assets/Resources/Data/";
        
        LevelData levelData = CreateInstance<LevelData>();

        string fullPath = AssetDatabase.GenerateUniqueAssetPath(path + name + ".asset");
        AssetDatabase.CreateAsset(levelData, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = levelData;
    }
    
    private void OnGUI()
    {
        level = EditorGUILayout.IntField("Level", level);
        if(level <= 0) return;
        
        if (allLevels.Count < level)
        {
            GUILayout.Label("Level not found ", EditorStyles.helpBox);
            GUILayout.Label("Create Level Data", EditorStyles.boldLabel);

            if (GUILayout.Button("Create New LevelData"))
            {
                CreateLevelDataAsset("LevelData" + (level));
                FindLevelData();
                Repaint();
            }
            return;
        }
        
        allLevels[level-1].Width = EditorGUILayout.IntField("Width", allLevels[level-1].Width);
        allLevels[level-1].Height = EditorGUILayout.IntField("Height", allLevels[level-1].Height);
        allLevels[level-1].LevelTime = EditorGUILayout.IntField("Time", allLevels[level-1].LevelTime);
        
        _serializedLevelData = new SerializedObject(allLevels[level-1]);
        _busColorsProp = _serializedLevelData.FindProperty("BusColors");
        
        _serializedLevelData.Update();
        
        validLevel = allLevels[level-1].Width * allLevels[level-1].Height;

        if (validLevel % 3 != 0)
        {
            GUILayout.Label("The product of the height and width must be a multiple of 3 ", EditorStyles.helpBox);
            GUILayout.Label("Current product is " + validLevel, EditorStyles.helpBox);
            return;
        }
        
        allLevels[level-1] = (LevelData)EditorGUILayout.ObjectField("Level Data", allLevels[level-1], typeof(LevelData), false);
        
        _serializedLevelData.Update();
        
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Bus Colors", EditorStyles.boldLabel);

        for (int i = 0; i < _busColorsProp.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();

            SerializedProperty element = _busColorsProp.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(element, GUIContent.none);

            if (GUILayout.Button("✕", GUILayout.Width(25)))
            {
                _busColorsProp.DeleteArrayElementAtIndex(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("+ Add Color"))
        {
            _busColorsProp.arraySize++;
        }
        _serializedLevelData.ApplyModifiedProperties();
        
        if (allLevels[level-1] == null)
        {
            Repaint();
            return;
        }
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Create/Reset LevelData"))
        {
            allLevels[level-1].CreateGrid(allLevels[level-1].Width, allLevels[level-1].Height);
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        DrawColorButton("White", ColorIndex.White, Color.white);
        DrawColorButton("Blue", ColorIndex.Blue, Color.blue);
        DrawColorButton("Green", ColorIndex.Green, Color.green);
        DrawColorButton("Red", ColorIndex.Red, Color.red);
        DrawColorButton("Yellow", ColorIndex.Yellow, Color.yellow); 
        EditorGUILayout.EndHorizontal();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        Coloring();
        
        void Coloring()
        {
            if (allLevels[level-1].grids != null && allLevels[level-1].grids.Count > 0)
            {
                for (int x = allLevels[level-1].Height - 1; x >= 0; x--)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                
                    for (int z = 0; z < allLevels[level-1].Width; z++)
                    {
                        var grid = allLevels[level-1].GetGridAt(z, x);
                        if(grid != null)
                            GUI.color = GetColor(grid.ColorIndex);
        
                        if (GUILayout.Button("", GUILayout.Width(55), GUILayout.Height(55)))
                        {
                            grid.ColorIndex = selectedColor;
                        }
                    }
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();
                }
            }
            GUI.color = Color.white;
        }
        
        if (GUILayout.Button("Save Level"))
        {
            Save();
        }
        EditorGUILayout.EndScrollView();
        
        GUI.color = Color.white;
    }

    private void Save()
    {
        EditorUtility.SetDirty(allLevels[level-1]);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void DrawColorButton(string name, ColorIndex type, Color guiColor)
    {
        GUI.color = guiColor;
        if (GUILayout.Button(name))
        {
            selectedColor = type;
        }
        GUI.color = Color.white;
    }
    
    private Color GetColor(ColorIndex type)
    {
        switch (type)
        {
            case ColorIndex.Red: return Color.red;
            case ColorIndex.Green: return Color.green;
            case ColorIndex.Blue: return Color.blue;
            case ColorIndex.Yellow: return Color.yellow;
            case ColorIndex.White: return Color.white;
            case ColorIndex.None: default: return new Color(0.8f, 0.8f, 0.8f, 0.4f);
        }
    }
    
}

