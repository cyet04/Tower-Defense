using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] private int enemyId = 1;
    [SerializeField] private Enemy_Tower enemyData;

    [Header("Enemy Stats")]
    private float currentHp;
    private float maxHp;
    private float moveSpeed;
    private float damage_base;
    private float weapon_damage;
    private float armor;
    private float reward;

    [Header("Components")]
    [SerializeField] private EnemyMovement enemyMovement;

    private EnemyInfo enemyInfo;
    private bool isAlive = true;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement == null)
        {
            enemyMovement = gameObject.AddComponent<EnemyMovement>();
        }
    }


    private void Start()
    {
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        if (enemyData != null)
        {
            enemyInfo = enemyData.GetEnemyInfo(enemyId);
            if (enemyInfo != null)
            {
                LoadEnemyStats();
                SetupMovement();
            }
        }
    }
    private void LoadEnemyStats()
    {
        maxHp = enemyInfo.hp;
        currentHp = maxHp;
        moveSpeed = enemyInfo.speed;
        damage_base = enemyInfo.damage_base;
        weapon_damage = enemyInfo.weapon_damage;
        armor = enemyInfo.armor;
        reward = enemyInfo.reward;
    }

    private void SetupMovement()
    {
        enemyMovement.SetMoveSpeed(moveSpeed);
        enemyMovement.SetRoute(MapManager.Instance.routeData.routePoints);
    }


    public void TakeDamage(float damage)
    {

    }

    private void Die()
    {

    }

    public EnemyInfo GetEnemyInfo()
    {
        return enemyInfo;
    }

    public float GetCurrentHp()
    {
        return currentHp;
    }

    public float GetMaxHp()
    {
        return maxHp;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void SetEnemyId(int id)
    {
        enemyId = id;
        InitializeEnemy();
    }
}
 