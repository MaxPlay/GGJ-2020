﻿using System;
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
}

public class Player : Character
{
    private enum DebugState
    {
        OnInteract,
    }

    PlayerStates currentState;
    PlayerStates previousState;

    [SerializeField]
    Vector2 speeds;
    Vector3 memorizedPosition;
    Interactable currentStation;

    [Space(20)]

    [SerializeField]
    bool debug = false;

    protected void Update()
    {
        switch (currentState)
        {
            case PlayerStates.Default:
                currentState = UpdateDefaultState();
                break;
            case PlayerStates.Fletching:
                currentState = UpdateFletchingState();
                break;
            case PlayerStates.Heating:
                currentState = UpdateHeatingState();
                break;
            case PlayerStates.Smithing:
                currentState = UpdateSmithingState();
                break;
        }
        HandleNextState();

        base.Update();
        previousState = currentState;
    }

    private void HandleNextState()
    {
        if(previousState != currentState)
        {
            switch(currentState)
            {
                case PlayerStates.Default:
                    EnterDefaultState();
                    break;
                case PlayerStates.Fletching:
                    EnterFletchingState();
                    break;
                case PlayerStates.Heating:
                    EnterHeatingState();
                    break;
                case PlayerStates.Smithing:
                    EnterSmithingState();
                    break;
            }
            switch (previousState)
            {
                case PlayerStates.Default:
                    ExitDefaultState();
                    break;
                case PlayerStates.Fletching:
                    ExitFletchingState();
                    break;
                case PlayerStates.Heating:
                    ExitHeatingState();
                    break;
                case PlayerStates.Smithing:
                    ExitSmithingState();
                    break;
            }
        }
    }

    private void ExitFletchingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Exit Fletching State");
        transform.position = memorizedPosition;
        if(inventory is Wood && (inventory as Wood).Worked >= 1)
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
    }

    private void ExitSmithingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Exit Smithing State");
        transform.position = memorizedPosition;
    }

    private void EnterSmithingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Smithing State");
        memorizedPosition = transform.position;
        transform.position = (currentStation as SmithingStation).SmithingPosition;
    }

    private void EnterHeatingState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Heating State");
        memorizedPosition = transform.position;
        transform.position = (currentStation as HeatingStation).HeatingPosition;
    }

    private void EnterDefaultState()
    {
        if (debug)
            Debug.Log("<b>[Player]</b> Enter Default State");
    }

    private void EnterFletchingState()
    {
        if(debug)
            Debug.Log("<b>[Player]</b> Enter Fletching State");
        memorizedPosition = transform.position;
        transform.position = (currentStation as FletchingStation).GrindingPosition;
    }

    private PlayerStates HandleInteractions()
    {
        if(Interactable.Instances != null)
        {
            int currentClosest = -1;
            for (int i = 0; i < Interactable.Instances.Count; i++)
            {
                if(inventory != Interactable.Instances[i] && Interactable.Instances[i].InteractionRange * Interactable.Instances[i].InteractionRange > Vector3.SqrMagnitude(transform.position - Interactable.Instances[i].transform.position))
                {
                    if(currentClosest < 0 || Vector3.SqrMagnitude(transform.position - Interactable.Instances[i].transform.position) < Vector3.SqrMagnitude(transform.position - Interactable.Instances[currentClosest].transform.position))
                    {
                        currentClosest = i;
                    }
                }
            }
            if(currentClosest >= 0 && currentClosest < Interactable.Instances.Count && Interactable.Instances[currentClosest])
            {
                OnHandleDebug(DebugState.OnInteract, DebugLogStates.NormalLog, Interactable.Instances[currentClosest].name);
                currentStation = Interactable.Instances[currentClosest].Interact(this);
                return SwapToNewAction(Interactable.Instances[currentClosest]);
            }
            else if(currentClosest < 0 && inventory != null)
            {
                DropItem();
            }
        }
        return currentState;
    }

    public Sword PlaceSwordInFurncae()
    {
        if(inventory is Sword)
        {
            Sword sword = inventory as Sword;
            inventory.gameObject.transform.parent = null;
            inventory = null;
            return sword;
        }
        return null;
    }

    private PlayerStates SwapToNewAction(Interactable interactable)
    {
        if(interactable is Item)
        {
            return PlayerStates.Default;
        }
        else if(interactable is FletchingStation && inventory != null && (inventory is Sword || inventory is Wood))
        {
            return PlayerStates.Fletching;
        }
        else if(interactable is HeatingStation)
        {
            return PlayerStates.Heating;
        }
        else if (interactable is SmithingStation && inventory != null && inventory is Sword)
        {
            return PlayerStates.Smithing;
        }

        return PlayerStates.Default;
    }

    private PlayerStates UpdateDefaultState()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            return HandleInteractions();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Vector2.up * speeds.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector2.right * speeds.x);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector2.left * speeds.x);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Vector2.down * speeds.y);
        }
        return PlayerStates.Default;
    }

    private PlayerStates UpdateSmithingState()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            return PlayerStates.Default;
        }
        (inventory as Sword).HammerSword(Time.deltaTime * (currentStation as SmithingStation).SmithingSpeed);
        return PlayerStates.Smithing;
    }

    private PlayerStates UpdateHeatingState()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            return PlayerStates.Default;
        }

        (currentStation as HeatingStation).HeatFurnace();
        return PlayerStates.Heating;
    }

    private PlayerStates UpdateFletchingState()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            return PlayerStates.Default;
        }

        if((currentStation as FletchingStation).TimeToGrind > 0)
        {
            if(inventory is Sword)
            {
                (inventory as Sword).SharpenSword(Time.deltaTime / (currentStation as FletchingStation).TimeToGrind);
            }
            else if(inventory is Wood)
            {
                (inventory as Wood).WorkOnWood(Time.deltaTime / (currentStation as FletchingStation).TimeToGrind);
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
        switch(logState)
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
