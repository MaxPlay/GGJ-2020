using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates
{
    Default = 0,
    Fletching = 1,
    Heating = 2,
    Smithing = 3,
    Attaching = 4,
}

public class Player : Character
{
    private enum DebugState
    {
        OnInteract,
    }

    PlayerStates currentState;
    PlayerStates previousState;

    Vector3 memorizedPosition;
    Interactable currentStation;

    [Space(20)]

    [SerializeField]
    bool debug = false;

    Dictionary<PlayerStates, Action> enterStates;
    Dictionary<PlayerStates, Action> exitStates;
    Dictionary<PlayerStates, Func<PlayerStates>> upgradeStates;

    private void Awake()
    {
        enterStates = new Dictionary<PlayerStates, Action>()
        {
            { PlayerStates.Default, EnterDefaultState },
            { PlayerStates.Fletching, EnterFletchingState },
            { PlayerStates.Heating, EnterHeatingState },
            { PlayerStates.Smithing, EnterSmithingState },
            { PlayerStates.Attaching, EnterAttachingState },
        };

        exitStates = new Dictionary<PlayerStates, Action>()
        {
            { PlayerStates.Default, ExitDefaultState },
            { PlayerStates.Fletching, ExitFletchingState },
            { PlayerStates.Heating, ExitHeatingState },
            { PlayerStates.Smithing, ExitSmithingState },
            { PlayerStates.Attaching, ExitAttachingState },
        };

        upgradeStates = new Dictionary<PlayerStates, Func<PlayerStates>>()
        {
            { PlayerStates.Default, UpdateDefaultState },
            { PlayerStates.Fletching, UpdateFletchingState },
            { PlayerStates.Heating, UpdateHeatingState },
            { PlayerStates.Smithing, UpdateSmithingState },
            { PlayerStates.Attaching, UpdateAttachingState },
        };
    }

    protected override void Update()
    {
        currentState = upgradeStates[currentState]();
        HandleNextState();

        base.Update();
        previousState = currentState;
    }

    private void HandleNextState()
    {
        if (previousState != currentState)
        {
            enterStates[currentState]();
            exitStates[previousState]();
        }
    }

    private void ExitFletchingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Exit Fletching State");
        transform.position = memorizedPosition;
        currentStation.SetProgressbarEnabled(false);
        if (inventory is Wood && (inventory as Wood).Worked >= 1)
        {
            inventory = (inventory as Wood).TurnIntoHandle();
        }
    }

    private void ExitDefaultState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Exit Default State");
    }

    private void ExitHeatingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Exit Heating State");
        transform.position = memorizedPosition;
        currentStation.SetProgressbarEnabled(false);
    }

    private void ExitSmithingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Exit Smithing State");
        transform.position = memorizedPosition;
        currentStation.SetProgressbarEnabled(false);
    }

    private void ExitAttachingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Exit Woodworking State");
        transform.position = memorizedPosition;
        currentStation.SetProgressbarEnabled(false);
    }

    private void EnterAttachingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Smithing State");
        (currentStation as WoodworkStation).ResetProgress();
        memorizedPosition = transform.position;
        currentStation.SetProgressbarEnabled(true);
        transform.position = (currentStation as WoodworkStation).WorkingPosition;
    }

    private void EnterSmithingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Smithing State");
        memorizedPosition = transform.position;
        currentStation.SetProgressbarEnabled(true);
        transform.position = (currentStation as SmithingStation).SmithingPosition;
    }

    private void EnterHeatingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Heating State");
        memorizedPosition = transform.position;
        currentStation.SetProgressbarEnabled(true);
        transform.position = (currentStation as HeatingStation).HeatingPosition;
        characterSpriteManager.SetState(CharacterSpriteManager.CharacterState.Left);
    }

    private void EnterDefaultState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Default State");
    }

    private void EnterFletchingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Fletching State");
        memorizedPosition = transform.position;
        currentStation.SetProgressbarEnabled(true);
        transform.position = (currentStation as FletchingStation).GrindingPosition;
    }

    private PlayerStates HandleInteractions()
    {
        if (Interactable.Instances != null)
        {
            int currentClosest = -1;
            for (int i = 0; i < Interactable.Instances.Count; i++)
            {
                if (inventory != Interactable.Instances[i] && Interactable.Instances[i].InteractionRange * Interactable.Instances[i].InteractionRange > Vector3.SqrMagnitude(transform.position - Interactable.Instances[i].transform.position))
                {
                    if (currentClosest < 0 || Vector3.SqrMagnitude(transform.position - Interactable.Instances[i].transform.position) < Vector3.SqrMagnitude(transform.position - Interactable.Instances[currentClosest].transform.position))
                    {
                        currentClosest = i;
                    }
                }
            }
            if (currentClosest >= 0 && currentClosest < Interactable.Instances.Count && Interactable.Instances[currentClosest])
            {
                OnHandleDebug(DebugState.OnInteract, DebugLogStates.NormalLog, Interactable.Instances[currentClosest].name);
                currentStation = Interactable.Instances[currentClosest].Interact(this);
                return SwapToNewAction(Interactable.Instances[currentClosest]);
            }
            else if (currentClosest < 0 && inventory != null)
            {
                DropItem();
            }
        }
        return currentState;
    }

    public Sword PlaceSwordInWorkstation()
    {
        if (inventory is Sword)
        {
            Sword sword = inventory as Sword;
            inventory.gameObject.transform.parent = null;
            inventory = null;
            return sword;
        }
        return null;
    }

    public Handle PlaceHandleInObject()
    {
        if (inventory is Handle)
        {
            Handle handle = inventory as Handle;
            inventory.gameObject.transform.parent = null;
            inventory = null;
            return handle;
        }
        return null;
    }

    private PlayerStates SwapToNewAction(Interactable interactable)
    {
        if (interactable is Item)
        {
            return PlayerStates.Default;
        }
        else if (interactable is FletchingStation && inventory != null && (inventory is Sword || inventory is Wood))
        {
            return PlayerStates.Fletching;
        }
        else if (interactable is HeatingStation)
        {
            return PlayerStates.Heating;
        }
        else if (interactable is SmithingStation && inventory != null && inventory is Sword)
        {
            return PlayerStates.Smithing;
        }
        else if (interactable is WoodworkStation && (interactable as WoodworkStation).HasBothItems)
        {
            return PlayerStates.Attaching;
        }

        return PlayerStates.Default;
    }

    private PlayerStates UpdateDefaultState()
    {
        GameplaySettings settings = GameManager.Instance.Settings;
        ControlSettingsInstance input = GameManager.Instance.Controls.Input;
        ControlSettingsInstance alternativeInput = GameManager.Instance.Controls.AlternativeInput;

        if (Input.GetKeyDown(input.Interact) || Input.GetKeyDown(alternativeInput.Interact))
        {
            return HandleInteractions();
        }
        if (Input.GetKey(input.Up) || Input.GetKey(alternativeInput.Up))
        {
            Move(Vector2.up * settings.PlayerSpeeds.y);
            characterSpriteManager.SetState(CharacterSpriteManager.CharacterState.Backward);
        }
        if (Input.GetKey(input.Right) || Input.GetKey(alternativeInput.Right))
        {
            Move(Vector2.right * settings.PlayerSpeeds.x);
            characterSpriteManager.SetState(CharacterSpriteManager.CharacterState.Right);
        }
        if (Input.GetKey(input.Left) || Input.GetKey(alternativeInput.Left))
        {
            Move(Vector2.left * settings.PlayerSpeeds.x);
            characterSpriteManager.SetState(CharacterSpriteManager.CharacterState.Left);
        }
        if (Input.GetKey(input.Down) || Input.GetKey(alternativeInput.Down))
        {
            Move(Vector2.down * settings.PlayerSpeeds.y);
            characterSpriteManager.SetState(CharacterSpriteManager.CharacterState.Forward);
        }
        return PlayerStates.Default;
    }

    private PlayerStates UpdateAttachingState()
    {
        ControlSettingsInstance input = GameManager.Instance.Controls.Input;
        ControlSettingsInstance alternativeInput = GameManager.Instance.Controls.AlternativeInput;

        if (Input.GetKeyUp(input.Interact) || Input.GetKeyUp(alternativeInput.Interact))
        {
            return PlayerStates.Default;
        }
        if ((currentStation as WoodworkStation).WorkOnSword())
        {
            return PlayerStates.Default;
        }
        return PlayerStates.Attaching;
    }

    private PlayerStates UpdateSmithingState()
    {
        ControlSettingsInstance input = GameManager.Instance.Controls.Input;
        ControlSettingsInstance alternativeInput = GameManager.Instance.Controls.AlternativeInput;

        if (Input.GetKeyUp(input.Interact) || Input.GetKeyUp(alternativeInput.Interact))
        {
            return PlayerStates.Default;
        }

        GameplaySettings settings = GameManager.Instance.Settings;
        if (settings.TimeForSmithing > 0)
            (inventory as Sword).HammerSword(Time.deltaTime / settings.TimeForSmithing);
        currentStation.SetProgressbarValue((inventory as Sword).Quality);
        return PlayerStates.Smithing;
    }

    private PlayerStates UpdateHeatingState()
    {
        ControlSettingsInstance input = GameManager.Instance.Controls.Input;
        ControlSettingsInstance alternativeInput = GameManager.Instance.Controls.AlternativeInput;

        if (Input.GetKeyUp(input.Interact) || Input.GetKeyUp(alternativeInput.Interact))
        {
            return PlayerStates.Default;
        }

        (currentStation as HeatingStation).HeatFurnace();
        return PlayerStates.Heating;
    }

    private PlayerStates UpdateFletchingState()
    {
        ControlSettingsInstance input = GameManager.Instance.Controls.Input;
        ControlSettingsInstance alternativeInput = GameManager.Instance.Controls.AlternativeInput;

        if (Input.GetKeyUp(input.Interact) || Input.GetKeyUp(alternativeInput.Interact))
        {
            return PlayerStates.Default;
        }

        GameplaySettings settings = GameManager.Instance.Settings;
        if (settings.TimeToGrindWeapon > 0)
        {
            if (inventory is Sword)
            {
                currentStation.SetProgressbarValue((inventory as Sword).SharpenSword(Time.deltaTime / settings.TimeToGrindWeapon));
            }
            else if (inventory is Wood)
            {
                currentStation.SetProgressbarValue((inventory as Wood).WorkOnWood(Time.deltaTime / settings.TimeToGrindWeapon));
            }
        }
        return PlayerStates.Fletching;
    }

    private void OnHandleDebug(DebugState state, DebugLogStates logState, string delegator = null)
    {
        string message = "<b>[Player]</b> ";
        switch (state)
        {
            case DebugState.OnInteract:
                message += delegator == null ? "Interact With something" : string.Format("Interact with: {0}", delegator);
                break;
        }
        switch (logState)
        {
            case DebugLogStates.LogError:
                Debug.LogError(message);
                break;
            case DebugLogStates.LogWarning:
                Debug.LogWarning(message);
                break;
            case DebugLogStates.NormalLog:
                Debug.Log(message);
                break;
        }
    }
}
