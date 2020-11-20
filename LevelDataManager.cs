using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LevelDataManager
{
    // Once all the levels are saved, this will no longer be necessary
    public static void Save(string filePath, GridBounds gridBounds, List<TileData> tileDatas)
    {
        if (GameManager.instance == null) { GameManager.instance = GameObject.FindObjectOfType<GameManager>(); }
        var bf = new BinaryFormatter();
        var file = File.Create(filePath);

        int levelNum = int.Parse(filePath.Substring(filePath.LastIndexOf("level")).Replace("level", "").Replace(".dat", ""));
        var levelData = new LevelData()
        {
            LevelNumber = levelNum,
            TurnLimit = GameManager.instance.TurnLimit,
            CameraData = new CameraData
            {
                size = Camera.main.orthographicSize,
                transformData = new TransformData(Camera.main.transform)
            },
            GridBounds = gridBounds,
            TileDatas = tileDatas
        };

        bf.Serialize(file, levelData);
        file.Close();
    }

    public static LevelData Load(string filePath)
    {
        if (!File.Exists(filePath)) { return null; }

        var bf = new BinaryFormatter();
        var file = File.Open(filePath, FileMode.Open);
        var data = (LevelData)bf.Deserialize(file);
        file.Close();

        return data;
    }

    public static int GetLevelFileCount(string filePath)
    {
        int count = 0;
        var fileNames = Directory.GetFiles(filePath, "*.dat", SearchOption.TopDirectoryOnly).Select(x => Path.GetFileName(x));

        foreach(var fileName in fileNames)
        {
            if (fileName.StartsWith("level")) { count++; }
        }

        return count;
    }
}