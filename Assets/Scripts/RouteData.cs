using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RouteData", menuName = "ScriptableObjects/RouteData")]
public class RouteData : ScriptableObject
{
    public List<Vector3> routePoints;
}
