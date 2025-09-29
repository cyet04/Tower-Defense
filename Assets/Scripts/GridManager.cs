using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public int gridWidth = 18;
    public int gridHeight = 10;
    public int minPathLength = 40;

    public GridCellData[] pathCellObjects;
    public GridCellData[] sceneryCellObjects;

    public PathGenerator pathGenerator;
    public List<Vector2Int> pathCells;
    public RouteData routeData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        pathCells = pathGenerator.GeneratePath();

        int pathSize = pathCells.Count;
        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            while (pathGenerator.GenerateCrossRoads()) ;
            pathSize = pathCells.Count;
        }

        pathGenerator.GenerateRoute();
        SaveRoute();

        StartCoroutine(LayGrid(pathCells));
    }

    public IEnumerator LayGrid(List<Vector2Int> pathCells)
    {
        // Chay coroutine va cho chay xong moi tiep tuc
        yield return StartCoroutine(LayPathCells(pathCells));
        yield return StartCoroutine(LaySceneryCells());
    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (var pathCell in pathCells)
        {
            int neighborValue = pathGenerator.GetCellNeighborValue(pathCell.x, pathCell.y);

            GameObject cellPrefab = pathCellObjects[neighborValue].cellPrefab;
            GameObject pathTileCell = Instantiate(cellPrefab, new Vector3(pathCell.x, 0, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, pathCellObjects[neighborValue].yRotation, 0f);
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }

    private IEnumerator LaySceneryCells()
    {
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    int randomIndex;
                    if (Random.Range(0, 10) < 4)
                    {
                        randomIndex = Random.Range(1, sceneryCellObjects.Length);
                    }
                    else
                    {
                        randomIndex = 0;
                    }

                    Instantiate(sceneryCellObjects[randomIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    yield return new WaitForSeconds(0.001f);
                }
            }
        }

        yield return null;
    }


    public void SaveRoute()
    {
        if (routeData.routePoints != null)
        {
            routeData.routePoints.Clear();
        }

        routeData.routePoints = pathGenerator.routeCells.Select(cell => new Vector3(cell.x, 0f, cell.y)).ToList();
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(routeData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }
}
