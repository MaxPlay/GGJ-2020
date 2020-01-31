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

    private void Update()
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

    private void OnTriggerStay(Collider other)
    {
        if(!didDebug)
        {
            Debug.Log("Trigger With: " + other.name);
            didDebug = true;
        }
        if (other.tag == TagPrefix.InteractableTag)
        {
            Interactable interactable = other.GetComponent<Interactable>();
        }
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

    private PlayerStates UpdateDefaultState()
    {
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
