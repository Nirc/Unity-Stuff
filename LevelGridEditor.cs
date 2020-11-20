using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGrid))]
public class LevelGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var levelGrid = target as LevelGrid;

        EditorGUILayout.Space();

        if (GUILayout.Button("Save level"))
        {
            var filePath = EditorUtility.SaveFilePanel("Save level", levelGrid.LevelFilePath, "", "dat");
            if (string.IsNullOrWhiteSpace(filePath)) { return; }

            levelGrid.SaveLevel(filePath);
        }

        if (GUILayout.Button("Load level"))
        {
            var filePath = EditorUtility.OpenFilePanel("Load level", levelGrid.LevelFilePath, "dat");
            if (string.IsNullOrWhiteSpace(filePath)) { return; }

            var levelData = LevelDataManager.Load(filePath);
            if (GameManager.instance == null) { GameManager.instance = GameObject.FindObjectOfType<GameManager>(); }
            GameManager.instance.LoadLevel(levelData);
        }
    }
}