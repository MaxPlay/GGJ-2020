using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Vector2.up);
        }
    }
}
