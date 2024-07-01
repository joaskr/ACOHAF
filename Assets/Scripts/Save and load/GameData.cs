using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currency;
    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, bool> checkpoints;
    public string closestCheckpointId;
    public GameData()
    {
        this.currency = 0;
        skillTree = new SerializableDictionary<string, bool>();
        closestCheckpointId = string.Empty;
        checkpoints = new SerializableDictionary<string, bool>();
    }

}