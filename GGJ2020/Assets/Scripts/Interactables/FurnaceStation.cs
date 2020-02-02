using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceStation : Interactable
{
    [SerializeField]
    Slider swordSlider;

    [SerializeField]
    Transform dropParent;

    [SerializeField]
    Renderer fireRenderer;

    [SerializeField]
    bool debug = false;

    float currentHeat = 0.5f;
    float currentLerp;
    int previousLevel = -1;
    Sword inventory;
    bool isHeatingUp;

    public int currentHeatLevel
    {
        get
        {
            return (int)(currentHeat * 4);
        }
    }

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
        UpdateFlames();
        SetProgressbarValue(currentHeat);
        isHeatingUp = true;
    }

    private void SetFlameColor(Color backBot, Color backTop, Color frontBot, Color frontTop)
    {
        fireRenderer.material.SetColor("_BotColor", backBot);

        fireRenderer.material.SetColor("_TopColor", backTop);

        fireRenderer.material.SetColor("_FlameColor", frontBot);

        fireRenderer.material.SetColor("_ToppingColor", frontTop);
    }

    public override void Start()
    {
        swordSlider.gameObject.SetActive(false);
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void UpdateFlames()
    {
        if(previousLevel == -1)
        {
            try
            {
                SetFlameColor(
                    GameManager.Instance.Settings.BackBotColors[currentHeatLevel],
                    GameManager.Instance.Settings.BackTopColors[currentHeatLevel],
                    GameManager.Instance.Settings.FrontBotColors[currentHeatLevel],
                    GameManager.Instance.Settings.FrontTopColors[currentHeatLevel]);
                currentLerp = 0;
                previousLevel = currentHeatLevel;
            }
            catch
            {
                Debug.LogError("<b>[FurnaceStation]</b> Something went wrong when trying to set Flame color. Did you assign a renderer and set the Colors in the Settings");
            }
        }
        if(previousLevel != currentHeatLevel)
        {
            if(currentLerp < 1)
            {
                try
                {
                    SetFlameColor(
                        Color.Lerp(GameManager.Instance.Settings.BackBotColors[previousLevel], GameManager.Instance.Settings.BackBotColors[currentHeatLevel], currentLerp),
                        Color.Lerp(GameManager.Instance.Settings.BackTopColors[previousLevel], GameManager.Instance.Settings.BackTopColors[currentHeatLevel], currentLerp),
                        Color.Lerp(GameManager.Instance.Settings.FrontBotColors[previousLevel], GameManager.Instance.Settings.FrontBotColors[currentHeatLevel], currentLerp),
                        Color.Lerp(GameManager.Instance.Settings.FrontTopColors[previousLevel], GameManager.Instance.Settings.FrontTopColors[currentHeatLevel], currentLerp));
                    currentLerp += Time.deltaTime / 2;
                }
                catch
                {

                }
            }
            else
            {
                SetFlameColor(
                        GameManager.Instance.Settings.BackBotColors[currentHeatLevel],
                        GameManager.Instance.Settings.BackTopColors[currentHeatLevel],
                        GameManager.Instance.Settings.FrontBotColors[currentHeatLevel],
                        GameManager.Instance.Settings.FrontTopColors[currentHeatLevel]);
                currentLerp = 0;
                previousLevel = currentHeatLevel;
            }
        }
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
            inventory.HeatSword(Time.deltaTime * settings.FurnaceMeltingStrength / (9 / currentHeatLevel));
            swordSlider.value = inventory.Heat;
        }
        if(settings.FurnaceHeatDropSpeed > 0 && currentHeat > 0 && !isHeatingUp)
        {
            currentHeat -= (Time.deltaTime / 4) * settings.FurnaceHeatDropSpeed * 0.01f * (currentHeatLevel + 1) * (currentHeatLevel + 1);
            UpdateFlames();
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
