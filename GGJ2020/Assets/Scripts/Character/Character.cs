using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    Item inventory;

    [SerializeField]
    Rigidbody rb;

    Vector2 additiveSpeed;

    protected void Update()
    {
        rb.velocity = MathUtils.VectorLerp(rb.velocity, Vector3.right * additiveSpeed.x + Vector3.forward * additiveSpeed.y, 0.5f);
        additiveSpeed = Vector2.zero;
    }

    protected void Move(Vector2 speed)
    {
        additiveSpeed += speed;
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
