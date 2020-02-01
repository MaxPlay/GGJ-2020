using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceStation : Interactable
{
    [SerializeField]
    Slider swordSlider;

    [SerializeField]
    float heatDropSpeed;

    [SerializeField]
    Transform dropParent;

    [SerializeField]
    bool debug = false;

    float currentHeat = 0.5f;
    Sword inventory;
    bool isHeatingUp;

    public int currentHeatLevel
    {
        get
        {
            return (int)(currentHeat * 4);
        }
    }

    public float HeatDropSpeed { get => heatDropSpeed; }

    public override Interactable Interact(Character character)
    {
        if(character.Inventory is Sword && inventory == null)
        {
            inventory = (character as Player).PlaceSwordInWorkstation();
            inventory.transform.parent = dropParent;
            swordSlider.gameObject.SetActive(true);
            inventory.transform.localPosition = Vector3.zero;
        }
        else if(character.Inventory == null && inventory != null)
        {
            character.PickUpItem(inventory);
            swordSlider.gameObject.SetActive(false);
            inventory = null;
        }
        return this;
    }

    public void HeatFurnace(float strength)
    {
        currentHeat = Mathf.Clamp01(currentHeat + strength);
        SetProgressbarValue(currentHeat);
        isHeatingUp = true;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && inventory != null)
        {
            swordSlider.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            swordSlider.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(currentHeatLevel > 0 && inventory != null)
        {
            inventory.HeatSword(Time.deltaTime / (9 / currentHeatLevel));
            swordSlider.value = inventory.Heat;
        }
        if(heatDropSpeed > 0 && currentHeat > 0 && !isHeatingUp)
        {
            currentHeat -= (Time.deltaTime / 4) * heatDropSpeed * 0.01f * (currentHeatLevel + 1) * (currentHeatLevel + 1);
        }
        else if(currentHeat < 0)
        {
            currentHeat = 0;
        }
        SetProgressbarValue(currentHeat);
        isHeatingUp = false;
        if (debug)
        {
            Debug.Log("<b>[FurnaceStation]</b> New Heat: " + currentHeat);
        }
    }

    private void OnDrawGizmos()
    {
        if (dropParent != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(dropParent.position, 0.1f);
        }
    }
}
