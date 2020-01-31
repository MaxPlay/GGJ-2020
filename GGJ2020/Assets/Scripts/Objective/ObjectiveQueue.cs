using System.Collections;
using System.Collections.Generic;

public class ObjectiveQueue : IReadOnlyList<Objective>
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
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => objectives.GetEnumerator();

    public IEnumerator<Objective> GetEnumerator() => objectives.GetEnumerator();
}