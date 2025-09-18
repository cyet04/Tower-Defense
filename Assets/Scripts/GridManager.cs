using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridWidth = 16;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private int minPathLength = 30;

    [SerializeField] private GridCellData[] pathCellObjects;
    [SerializeField] private GridCellData[] sceneryCellObjects;

    private PathGenerator pathGenerator;

    private void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        List<Vector2Int> pathCells = pathGenerator.GeneratePath();

        int pathSize = pathCells.Count;
        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSize = pathCells.Count;
        }

        StartCoroutine(LayGrid(pathCells));
    }

    private IEnumerator LayGrid(List<Vector2Int>  pathCells)
    {
        // Chay coroutine va cho xong moi tiep tuc
        yield return StartCoroutine(LayPathCells(pathCells));
        yield return StartCoroutine(LaySceneryCells());

    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (var pathCell in pathCells)
        {
            int neighborValue = pathGenerator.GetCellNeighborValue(pathCell.x, pathCell.y);
            Debug.Log("Tile " + pathCell.x + ", " + pathCell.y + " neighbor value = " + neighborValue);
            
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
                    int randomIndex = Random.Range(0, sceneryCellObjects.Length);
                    Instantiate(sceneryCellObjects[randomIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    yield return new WaitForSeconds(0.001f);
                }
            }
        }

        yield return null;
    }
}
