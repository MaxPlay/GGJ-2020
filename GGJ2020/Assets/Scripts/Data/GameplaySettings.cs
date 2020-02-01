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

    #region Sword Settings

    [Space(15)]
    [Header("Sword Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The time needed to fully coold down by its own")]
    private float swordTimeToAutoCoolDown;

    public float SwordTimeToAutoCoolDown => swordTimeToAutoCoolDown;

    [SerializeField, Tooltip("The minimum heat required to fix the blade")]
    private float minimumHeatToForge;

    public float MinimumHeatToForge => minimumHeatToForge;

    [SerializeField, Tooltip("When this theat-threshold is reached, the sword will become unfixable")]
    private float maximumHeatToMelt;

    public float MaximumHeatToMelt => maximumHeatToMelt;

    #endregion

    #region Player

    [Space(15)]
    [Header("Player Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The axis-speed at which the player moves around")]
    private Vector2 playerSpeeds;

    public Vector2 PlayerSpeeds => playerSpeeds;

    #endregion

    #region FurnaceStation Settings

    [Space(15)]
    [Header("FurnaceStation Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The speed at which the furnace cools down by itself.")]
    float furnaceHeatDropSpeed;
    [SerializeField, Tooltip("The strength with which items will be melted.")]
    float furnaceMeltingStrength;

    public float FurnaceMeltingStrength => furnaceMeltingStrength;

    public float FurnaceHeatDropSpeed => furnaceHeatDropSpeed;

    #endregion

    #region HeatingStation Settings

    [Space(15)]
    [Header("HeatingStation Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The time needed to fully heat the oven. Not ENTIRELLY true <v<")]
    float timeToFullyHeat;

    public float TimeToFullyHeat => timeToFullyHeat;

    #endregion

    #region GrindingStation Settings

    [Space(15)]
    [Header("GrindingStation Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The time needed to fully grind a dull weapon")]
    float timeToGrindWeapon;

    public float TimeToGrindWeapon => timeToGrindWeapon;

    #endregion

    #region Bucket Settings

    [Space(15)]
    [Header("Bucket Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The time needed to cool down a sword in the bucket")]
    float bucketTimeToCooldDown;

    public float BucketTimeToCooldDown => bucketTimeToCooldDown;

    #endregion

    #region SmithingStation Settings

    [Space(15)]
    [Header("SmithingStation Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The time needed to smith a hot weapon")]
    float timeForSmithing;

    public float TimeForSmithing => timeForSmithing;

    #endregion

    #region WorkshopStation

    [Space(15)]
    [Header("WorkshopStation Settings")]
    [Space(5)]
    [SerializeField, Tooltip("The time needed to attach a handle to a blade")]
    float timeToAttachHandle;

    public float TimeToAttachHandle => timeToAttachHandle;

    #endregion
}