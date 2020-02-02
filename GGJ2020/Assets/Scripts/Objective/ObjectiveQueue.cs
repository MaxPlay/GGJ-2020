using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveQueue
{
    Objective[] objectives;
    private CustomerLine customerLine;

    public ObjectiveQueue(int maxObjectives, CustomerLine customerLine)
    {
        objectives = new Objective[maxObjectives];
        this.customerLine = customerLine;
    }

    public int MaxObjectives => objectives.Length;

    public int Count { get; private set; }

    public Objective this[int index] => objectives[index];

    public void AddRandom() => AddObjective(Objective.Generate());

    public void AddObjective(Objective objective)
    {
        if(Count < MaxObjectives)
        {
            int index = FillFreeSlot(objective);
            Customer customer = customerLine[index];
            objective.Owner = customer;
            customer.Initialize(objective);
        }
    }

    private int FillFreeSlot(Objective objective)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i] == null)
            {
                objectives[i] = objective;
                Count++;
                return i;
            }
        }
        return -1;
    }

    public void FreeSlot(int index)
    {
        objectives[index] = null;
        Count--;
    }

    public Objective LastObjective()
    {
        if (Count == 0)
            return null;

        return objectives.Last();
    }
}
