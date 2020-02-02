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
    [SerializeField]
    LightFlicker lightFlicker;

    public Animator anim;

    [Header("Particles")]
    [SerializeField]
    ParticleSystem smokeParticles;
    [SerializeField]
    ParticleSystem puffParticles;
    [SerializeField]
    ParticleSystem sparkParticles;
    
    public int CurrentHeatLevel
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

        Gradient gradient = new Gradient
        {
            colorKeys = new GradientColorKey[] 
            {
                new GradientColorKey(frontBot, 0.0f),
                new GradientColorKey(frontTop, 0.33f),
                new GradientColorKey(backBot, 0.66f),
                new GradientColorKey(backTop, 1.0f)
            }
        };
        lightFlicker.SetLightRange(gradient);
    }

    public override void Start()
    {
        swordSlider.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
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
                    GameManager.Instance.Settings.BackBotColors[CurrentHeatLevel],
                    GameManager.Instance.Settings.BackTopColors[CurrentHeatLevel],
                    GameManager.Instance.Settings.FrontBotColors[CurrentHeatLevel],
                    GameManager.Instance.Settings.FrontTopColors[CurrentHeatLevel]);
                currentLerp = 0;
                previousLevel = CurrentHeatLevel;
            }
            catch
            {
                Debug.LogError("<b>[FurnaceStation]</b> Something went wrong when trying to set Flame color. Did you assign a renderer and set the Colors in the Settings");
            }
        }
        if(previousLevel != CurrentHeatLevel)
        {
            if(currentLerp < 1)
            {
                try
                {
                    SetFlameColor(
                        Color.Lerp(GameManager.Instance.Settings.BackBotColors[previousLevel], GameManager.Instance.Settings.BackBotColors[CurrentHeatLevel], currentLerp),
                        Color.Lerp(GameManager.Instance.Settings.BackTopColors[previousLevel], GameManager.Instance.Settings.BackTopColors[CurrentHeatLevel], currentLerp),
                        Color.Lerp(GameManager.Instance.Settings.FrontBotColors[previousLevel], GameManager.Instance.Settings.FrontBotColors[CurrentHeatLevel], currentLerp),
                        Color.Lerp(GameManager.Instance.Settings.FrontTopColors[previousLevel], GameManager.Instance.Settings.FrontTopColors[CurrentHeatLevel], currentLerp));
                    currentLerp += Time.deltaTime / 2;
                }
                catch
                {

                }
            }
            else
            {
                SetFlameColor(
                        GameManager.Instance.Settings.BackBotColors[CurrentHeatLevel],
                        GameManager.Instance.Settings.BackTopColors[CurrentHeatLevel],
                        GameManager.Instance.Settings.FrontBotColors[CurrentHeatLevel],
                        GameManager.Instance.Settings.FrontTopColors[CurrentHeatLevel]);
                currentLerp = 0;
                previousLevel = CurrentHeatLevel;
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
        if(CurrentHeatLevel > 0 && inventory != null)
        {
            inventory.HeatSword(Time.deltaTime * settings.FurnaceMeltingStrength / (9 / CurrentHeatLevel));
            swordSlider.value = inventory.Heat;
            if(inventory.Heat > GameManager.Instance.Settings.MaximumHeatToMelt)
            {
                inventory.Owner.ObjectiveCompleted();
                Destroy(inventory.gameObject);
                inventory = null;
                return;
            }
        }
        if(settings.FurnaceHeatDropSpeed > 0 && currentHeat > 0 && !isHeatingUp)
        {
            currentHeat -= (Time.deltaTime / 4) * settings.FurnaceHeatDropSpeed * 0.01f * (CurrentHeatLevel + 1) * (CurrentHeatLevel + 1);
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

    public void SpawnSparks()
    {
        sparkParticles.Play();
    }
}
