using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates
{
    Default = 0,
    Fletching = 1
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
    float speed;

    [Space(20)]

    [SerializeField]
    bool debug = false;

    private void Update()
    {
        switch (currentState)
        {
            case PlayerStates.Default:
                previousState = UpdateDefaultState();
                break;
            case PlayerStates.Fletching:
                previousState = UpdateFletchingState();
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
            }
            switch (previousState)
            {
                case PlayerStates.Default:
                    ExitDefaultState();
                    break;
                case PlayerStates.Fletching:
                    ExitFletchingState();
                    break;
            }
        }
    }

    private void ExitFletchingState()
    {

    }

    private void ExitDefaultState()
    {

    }

    private void EnterDefaultState()
    {

    }

    private void EnterFletchingState()
    {

    }

    private void HandleInteractions()
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
            if(currentClosest >= 0 && currentClosest < Interactable.Instances.Count)
            {
                OnHandleDebug(DebugState.OnInteract, DebugLogStates.NormalLog, Interactable.Instances[currentClosest].name);
                Interactable.Instances[currentClosest].Interact(this);
            }
        }
    }

    private PlayerStates UpdateDefaultState()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(inventory != null)
            {
                DropItem();
            }
            else
            {
                HandleInteractions();
            }
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

    private PlayerStates UpdateFletchingState()
    {
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
