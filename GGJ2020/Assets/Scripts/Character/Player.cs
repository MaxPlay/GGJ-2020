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
    PlayerStates currentState;
    PlayerStates previousState;

    bool didDebug = false;

    protected void Update()
    {
        didDebug = false;
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
                if(Interactable.Instances[i].InteractionRange * Interactable.Instances[i].InteractionRange > Vector3.SqrMagnitude(transform.position - Interactable.Instances[i].transform.position))
                {
                    if(currentClosest < 0 || Vector3.SqrMagnitude(transform.position - Interactable.Instances[i].transform.position) < Vector3.SqrMagnitude(transform.position - Interactable.Instances[currentClosest].transform.position))
                    {
                        currentClosest = i;
                    }
                }
            }
            if(currentClosest >= 0 && currentClosest < Interactable.Instances.Count)
            {
                Interactable.Instances[currentClosest].Interact(this);
            }
        }
    }

    private PlayerStates UpdateDefaultState()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HandleInteractions();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Vector2.up);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Vector2.down);
        }
        return PlayerStates.Default;
    }

    private PlayerStates UpdateFletchingState()
    {
        return PlayerStates.Fletching;
    }
}
