using UnityEngine;

[CreateAssetMenu(menuName = "Data/Prefab Container")]
public class PrefabContainer : ScriptableObject
{
    public Customer Customer;

    public Sword Sword;

    public Handle Handle;

    public Wood Wood;
}