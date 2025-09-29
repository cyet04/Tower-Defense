using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 5f;


    [Header("Route Settings")]
    private List<Vector3> routePoints = new();
    private int currentIndex = 0;

    private bool isMoving = false;
    private bool hasReachedEnd = false;

    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (isMoving && !hasReachedEnd)
        {
            MoveAlongRoute();
        }
    }

    private void MoveAlongRoute()
    {
        if (routePoints.Count == 0 || currentIndex >= routePoints.Count)
        {
            hasReachedEnd = true;
            OnReachedEnd();
            return;
        }

        Vector3 target = routePoints[currentIndex];
        Vector3 direction = (target - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentIndex++;
        }
    }

    private void OnReachedEnd()
    {
       isMoving = false;

        // Broadcast event enemy reached end

        gameObject.SetActive(false);
    }

    public void SetRoute(List<Vector3> route)
    {
        routePoints = new List<Vector3>(route);
        currentIndex = 0;
        hasReachedEnd = false;
        isMoving = true;

        transform.position = routePoints[0];
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void StopMovement()
    {
        isMoving = false;
    }

    public void ResumeMovement()
    {
        if (!hasReachedEnd)
        {
            isMoving = true;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool HasReachedEnd()
    {
        return hasReachedEnd;
    }

    public float GetProgress()
    {
        if (routePoints.Count <= 1) return 0f;

        float totalDistance = 0f;
        for (int i = 0; i < routePoints.Count - 1; i++)
        {
            totalDistance += Vector3.Distance(routePoints[i], routePoints[i + 1]);
        }

        float traveledDistance = 0f;
        for (int i = 0; i < currentIndex; i++)
        {
            traveledDistance += Vector3.Distance(routePoints[i], routePoints[i + 1]);
        }

        if (currentIndex < routePoints.Count)
        {
            traveledDistance += Vector3.Distance(transform.position, routePoints[currentIndex]);
        }

        return totalDistance > 0 ? traveledDistance / totalDistance : 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < routePoints.Count - 1; i++)
        {
            Gizmos.DrawLine(routePoints[i], routePoints[i + 1]);
        }

        Gizmos.color = Color.green;
        for (int i = 0; i < routePoints.Count; i++)
        {
            Gizmos.DrawSphere(routePoints[i], 0.2f);
        }
    }
}
