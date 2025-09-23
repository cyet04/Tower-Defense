using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Offset
{
    public int x;
    public int y;

    public Vector2Int ToVector2Int() => new Vector2Int(x, y);
}

[Serializable]
public class CrossroadPattern
{
    public string name;
    public List<Offset> requiredEmptyOffsets = new List<Offset>();
    public List<Offset> pathOffsets = new List<Offset>();
}

[Serializable]
public class CrossroadPatternCollection
{
    public List<CrossroadPattern> patterns = new List<CrossroadPattern>();
}
