using System;
using System.Collections.Generic;
using UnityEngine;

// Everything here must be Serializable to work with the BinaryFormatter in LevelDataManager

[Serializable]
public class LevelData
{
    public int LevelNumber { get; set; }
    public int TurnLimit { get; set; }
    public CameraData CameraData { get; set; }
    public GridBounds GridBounds { get; set; }
    public List<TileData> TileDatas { get; set; }
}

#region Classes and enums used by LevelData

[Serializable]
public class CameraData
{
    public TransformData transformData;
    public float size;
}

[Serializable]
public class TransformData
{
    public float positionX;
    public float positionY;
    public float positionZ;

    public TransformData(Transform transform)
    {
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;
    }
}

// While not all of these settings apply to all tile behaviours, they are put into one class for simplicity's sake
[Serializable]
public class TileData
{
    public List<TileBehaviourType> tileBehaviourTypes;
    public Coordinates2D coordinates;
    public TileColor tileColor;
    public ConveyerDirection conveyerDirection;
    public int tilesToMove;
    public int startingTurns;
    public bool isToggled;

    public TileData()
    {
        tileBehaviourTypes = new List<TileBehaviourType>();
        startingTurns = 1;
        tilesToMove = 1;
        isToggled = true;
    }
}


[Serializable]
public enum TileBehaviourType { Blocking, Countdown, Goal, Movable, Togglable, Conveyer }

[Serializable]
public enum TileColor { Red, Blue, Green }

[Serializable]
public enum ConveyerDirection { Right, Left, Up, Down }

#endregion