using System;
using UnityEngine;
using UnityEngine.Events;

public class GameState : ScriptableObject
{
    CustomerLine customerLine;

    float objectiveSpawnTimer;

    public ObjectiveQueue ObjectiveQueue { get; private set; }

    public void Start()
    {
        customerLine = FindObjectOfType<CustomerLine>();
        ObjectiveQueue = new ObjectiveQueue(GameManager.Instance.Settings.MaxObjectives, customerLine);
    }

    public void End()
    {

    }

    public void Update()
    {
        if(objectiveSpawnTimer <= 0)
        {
            objectiveSpawnTimer += GameManager.Instance.Settings.ObjectiveFrequency;
            ObjectiveQueue.AddRandom();
        }

        objectiveSpawnTimer -= Time.deltaTime;
    }
}