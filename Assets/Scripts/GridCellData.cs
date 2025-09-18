using UnityEngine;

[CreateAssetMenu(fileName = "GridCellData", menuName = "ScriptableObjects/GridCellData")]
public class GridCellData : ScriptableObject
{
    public CellType cellType;
    public GameObject cellPrefab;
    public int yRotation;
}

public enum CellType
{
    Path,
    Ground,
    Obstacle,
    Start,
    End,
}