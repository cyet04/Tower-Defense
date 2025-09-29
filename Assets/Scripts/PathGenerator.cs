using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class PathGenerator
{
    private int width, height;
    private List<Vector2Int> pathCells;
    public List<Vector2Int> routeCells;

    private List<CrossroadPattern> crossroadPatterns;

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;

        var collection = PatternLoader.LoadPatterns("crossroadPatterns");
        crossroadPatterns = collection.patterns;

        //crossroadPatterns = null;
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
            if (crossroadPatterns == null || crossroadPatterns.Count == 0) return false;
            foreach (var pattern in crossroadPatterns)
            {
                if (CanPlacePattern(pathCell, pattern))
                {
                    PlacePattern(pathCell, pattern);
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

    public bool CellIsEmpty(Vector2Int cell)
    {
        return !pathCells.Contains(cell);
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

    public List<Vector2Int> GenerateRoute()
    {
        routeCells = new List<Vector2Int>();
        Vector2Int direction = Vector2Int.right;
        Vector2Int currentCell = pathCells[0];

        while (currentCell.x < width - 1)
        {
            routeCells.Add(currentCell);

            if (!CellIsEmpty(currentCell + direction))
            {
                currentCell += direction;
            }
            else if (!CellIsEmpty(currentCell + Vector2Int.up) && direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
                currentCell += direction;
            }
            else if (!CellIsEmpty(currentCell + Vector2Int.down) && direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
                currentCell += direction;
            }
            else if (!CellIsEmpty(currentCell + Vector2Int.right) && direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
                currentCell += direction;
            }
            else if (!CellIsEmpty(currentCell + Vector2Int.left) && direction != Vector2Int.right) {
                direction = Vector2Int.left;
                currentCell += direction;
            }
            else
            {
                throw new System.Exception("Het route roi");
            }
        }

        routeCells.Add(currentCell);

        return routeCells;
    }
}
