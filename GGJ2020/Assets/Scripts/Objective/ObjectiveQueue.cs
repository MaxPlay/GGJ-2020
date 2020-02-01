using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveQueue
{
    List<Objective> objectives = new List<Objective>();

    public int MaxObjectives { get { return GameManager.Instance.Settings.MaxObjectives; } }

    public int Count => objectives.Count;

    public Objective this[int index] => objectives[index];

    public void AddRandom() => AddObjective(Objective.Generate());

    public void AddObjective(Objective objective)
    {
        if(objectives.Count < MaxObjectives)
        {
            objectives.Add(objective);
            Customer customer = UnityEngine.Object.Instantiate(GameManager.Instance.Prefabs.Customer);
            objective.Owner = customer;
            customer.Initialize(objective);
        }
    }

    public Objective LastObjective()
    {
        if (Count == 0)
            return null;

        return objectives.Last();
    }
}