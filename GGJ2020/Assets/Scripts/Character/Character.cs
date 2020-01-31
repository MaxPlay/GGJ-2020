using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    Item inventory;

    [SerializeField]
    Rigidbody rb;

    protected void Move(Vector2 speed)
    {
        rb.velocity = Vector3.right * speed.x + Vector3.forward * speed.y;
    }

    private void Interact()
    {

    }

    private void PickUpItem()
    {

    }

    private void PutItem()
    {

    }

    private void DropItem()
    {

    }
}
