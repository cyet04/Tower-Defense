using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class JsonDemo : MonoBehaviour
{
    public string playerName;
    public int playerScore;
    public List<int> levelIds;

    private void Start()
    {
        PrintPattern();
    }

    public void PrintPattern()
    {
        var collection = PatternLoader.LoadPatterns("crossroadPatterns");

        foreach (var pattern in collection.patterns)
        {
            Debug.Log($"Pattern Name: {pattern.name}");
            Debug.Log("Required Empty Offsets:");
            foreach (var offset in pattern.requiredEmptyOffsets)
            {
                Debug.Log($"  - {offset}");
            }
            Debug.Log("Path Offsets:");
            foreach (var offset in pattern.pathOffsets)
            {
                Debug.Log($"  - {offset}");
            }
        }
    }
}
