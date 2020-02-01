using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    protected Item inventory;

    [SerializeField]
    protected Transform inventorySlot, dropPosition;

    [SerializeField]
    protected Rigidbody rb;

    private Vector2 additiveSpeed;

    protected void Update()
    {
        rb.velocity = MathUtils.VectorLerp(rb.velocity, Vector3.right * additiveSpeed.x + Vector3.forward * additiveSpeed.y, 0.5f);
        additiveSpeed = Vector2.zero;
    }

    protected void Move(Vector2 speed)
    {
        additiveSpeed += speed;
    }

    public void PickUpItem(Item item)
    {
        inventory = item;
        inventory.transform.SetParent(inventorySlot);
        inventory.transform.localPosition = Vector3.zero;
    }

    protected void PutItem()
    {

    }

    protected void DropItem()
    {
        inventory.transform.position = dropPosition.position;
        inventory.transform.parent = null;
        inventory = null;
    }

    private void OnDrawGizmos()
    {
        if(inventorySlot != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(inventorySlot.position, 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(dropPosition.position, 0.1f);
        }
    }
}
