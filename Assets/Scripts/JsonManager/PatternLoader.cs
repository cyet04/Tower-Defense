using Mono.Cecil;
using UnityEngine;

public static class PatternLoader
{
    public static CrossroadPatternCollection LoadPatterns(string jsonFileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Json Files/" + jsonFileName);

        if (jsonFile == null)
        {
            return new CrossroadPatternCollection { patterns = new System.Collections.Generic.List<CrossroadPattern>() };
        }

        return JsonUtility.FromJson<CrossroadPatternCollection>(jsonFile.text);
    }
}
