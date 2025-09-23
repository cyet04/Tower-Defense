using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathGenerator
{
    private int width, height;
    private List<Vector2Int> pathCells;

    private List<CrossroadPattern> crossroadPatterns;

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;

        var collection = PatternLoader.LoadPatterns("crossroadPatterns");
        crossroadPatterns = collection.patterns;
    }

    public List<Vector2Int> GeneratePath()
    {
        pathCells = new List<Vector2Int>();
        int y = height / 2;
        int x = 0;

        while (x < width)
        {
            pathCells.Add(new Vector2Int(x, y));


            bool validMove = false;

            while (!validMove)
            {
                int move = Random.Range(0, 3); // 0: right, 1: up, 2: down

                if (move == 0 || x % 2 == 0 || x > width - 2)
                {
                    x++;
                    validMove = true;
                }
                else if (move == 1 && CellIsEmpty(x, y + 1) && y < height - 2)
                {
                    y++;
                    validMove = true;
                }
                else if (move == 2 && CellIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }

        return pathCells;
    }

    public bool GenerateCrossRoads()
    {
        foreach (var pathCell in pathCells.ToList())
        {
            foreach (var pattern in crossroadPatterns)
            {
                if (CanPlacePattern(pathCell, pattern))
                {
                    PlacePattern(pathCell, pattern);
                    Debug.Log($"Crossroad placed: {pattern.name} at {pathCell}");
                    return true;
                }
            }
        }

        return false;
    }


    private bool CanPlacePattern(Vector2Int baseCell, CrossroadPattern pattern)
    {
        foreach (var offset in pattern.requiredEmptyOffsets)
        {
            Vector2Int pos = baseCell + offset.ToVector2Int();
            if (!CellIsEmpty(pos.x, pos.y)) return false;
            if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height) return false;
        }
        return true;
    }

    private void PlacePattern(Vector2Int baseCell, CrossroadPattern pattern)
    {
        var newCells = pattern.pathOffsets.Select(offset => baseCell + offset.ToVector2Int());
        pathCells.AddRange(newCells);
    }

    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public int GetCellNeighborValue(int x, int y)
    {
        int returnValue = 0;

        if (!CellIsEmpty(x, y - 1))
        {
            returnValue += 1;
        }

        if (!CellIsEmpty(x - 1, y))
        {
            returnValue += 2;
        }

        if (!CellIsEmpty(x, y + 1))
        {
            returnValue += 8;
        }

        if (!CellIsEmpty(x + 1, y))
        {
            returnValue += 4;
        }

        return returnValue;
    }
}
