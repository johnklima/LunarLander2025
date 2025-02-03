using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "CustomTools/Levels Data Asset", fileName = "LevelsData")]
public class LevelDataSO : ScriptableObject
{
    public List<LevelData> levelsData = new();

    public LevelData GetData(int id)
    {
        foreach (var levelData in levelsData)
        {
            if (levelsData.IndexOf(levelData) == id)
            {
                return levelData;
            }
        }
        return null;
    }
}

[Serializable]
public class LevelData
{
    [Header("General Info")]
    public string name;
    public GameObject planetPrefab;
    public float shipMaxFuel;

    [Header("Skybox Config")]
    [Range(0, 50)] public float zenitBlend;
    [Range(0, 50)] public float nadirBlend;
    [Range(0, 50)] public float horizontBlend;
    public Color skyColor;
    public Color groundColor;
    public Color horizontColor;
    public float starHeight;
    public float starPower;
    public float starIntensity;
}
