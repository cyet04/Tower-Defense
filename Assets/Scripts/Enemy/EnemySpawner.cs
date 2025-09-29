using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var enemy = MyPoolManager.Instance.GetFromPool(enemyPrefab, this.transform);
    }
}
