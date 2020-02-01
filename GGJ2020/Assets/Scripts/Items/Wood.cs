using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Item
{
    private float worked;

    private bool debug = true;

    [SerializeField]
    GameObject handlePrefab;

    public float Worked { get => worked; }

    public override Interactable Interact(Character character)
    {
        return base.Interact(character);
    }

    public float WorkOnWood(float strength)
    {
        worked = Mathf.Clamp01(worked + strength);
        if (debug)
            Debug.Log("<b>[Sword]</b> New Sharpness: " + worked);
        return worked;
    }

    public Handle TurnIntoHandle()
    {
        Destroy(gameObject);
        return Instantiate(handlePrefab, transform.position, transform.rotation, transform.parent).GetComponent<Handle>();
    }
}
