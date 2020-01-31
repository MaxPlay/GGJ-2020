using System;
using UnityEngine;

public class GameState : ScriptableObject
{
    ObjectiveQueue objectiveQueue;

    float objectiveSpawnTimer;

    public void Start()
    {
        objectiveQueue = new ObjectiveQueue();
    }

    public void End()
    {

    }

    public void Update()
    {
        if(objectiveSpawnTimer <= 0)
        {
            objectiveSpawnTimer += GameManager.Instance.Settings.ObjectiveFrequency;
            objectiveQueue.AddRandom();
        }
    }
}