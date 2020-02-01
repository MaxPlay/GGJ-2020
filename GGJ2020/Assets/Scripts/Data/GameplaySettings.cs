using UnityEngine;

[CreateAssetMenu (menuName = "Data/Gameplay Settings")]
public class GameplaySettings : ScriptableObject {
    [SerializeField]
    private int maxObjectives;

    public int MaxObjectives => maxObjectives;

    [SerializeField]
    private float objectiveFrequency;

    public float ObjectiveFrequency => objectiveFrequency;

    [SerializeField]
    float customerMoveTime;

    public float CustomerMoveTime => customerMoveTime;

    //Win or lose conditions

    [SerializeField]
    private int scoreWin = 200;

    public int ScoreWin => scoreWin;

    [SerializeField]
    private int scoreLose = 0;

    public int ScoreLose => scoreLose;

    [SerializeField]
    private int score = 50;

    public int Score => score;

    //Battlefield-Point-Loop
    [SerializeField]
    private float secondsPerRound = 10.0f;

    public float SecondsPerRound => secondsPerRound;

    [SerializeField]
    private int penaltyEndOfRoundPerCustomer = 1;

    public int PenaltyEndOfRoundPerCustomer => penaltyEndOfRoundPerCustomer;

    [SerializeField]
    private int customersWithoutPenalty = 2;

    public int CustomersWithoutPenalty => customersWithoutPenalty;

    [SerializeField]
    private int ratioPointsCustomerSatisfied = 1;

    public int RatioPointsCustomerSatisfied => ratioPointsCustomerSatisfied;

    [SerializeField]
    private int ratioPointsCustomerImpressed = 1;

    public int RatioPointsCustomerImpressed => ratioPointsCustomerImpressed;

    [SerializeField]
    private int ratioPointsCustomerDiappointed = 1;

    public int RatioPointsCustomerDiappointed => ratioPointsCustomerDiappointed;

}