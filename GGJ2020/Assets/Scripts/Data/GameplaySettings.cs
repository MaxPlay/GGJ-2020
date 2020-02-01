using UnityEngine;

[CreateAssetMenu(menuName = "Data/Gameplay Settings")]
public class GameplaySettings : ScriptableObject
{
    [SerializeField]
    private int maxObjectives;

    public int MaxObjectives => maxObjectives;

    [SerializeField]
    private float objectiveFrequency;

    public float ObjectiveFrequency => objectiveFrequency;

    [SerializeField]
    private float customerMoveTime;

    public float CustomerMoveTime => customerMoveTime;

    [SerializeField]
    private float waitTimer;

    public float WaitTimer => waitTimer;

    [SerializeField]
    private float duckReactRange;

    public float DuckReactRange => duckReactRange;
}