using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodworkStation : Interactable
{
    [SerializeField]
    private Transform woodPosition, swordPosition, workingPosition;

    float progress;

    private Item woodInventory, swordInventory;

    public Animator anim;

    public Vector3 WorkingPosition
    {
        get
        {
            return workingPosition.position;
        }
    }

    public bool HasBothItems
    {
        get
        {
            return woodInventory && swordInventory;
        }
    }

    public bool WorkOnSword()
    {
        if(settings.TimeToAttachHandle > 0)
        {
            progress += Time.deltaTime / settings.TimeToAttachHandle;
        }
        if(progress >= 1)
        {
            FinishWork();
            return true;
        }
        SetProgressbarValue(progress);
        return false;
    }

    private void FinishWork()
    {
        Destroy(woodInventory.gameObject);
        woodInventory = null;
        progress = 0;
        (swordInventory as Sword).AddHandle();
    }

    public void ResetProgress()
    {
        progress = 0;
    }

    public override Interactable Interact(Character character)
    {
        if(swordInventory != null && woodInventory != null)
        {
        }
        else if (character.Inventory is Sword && swordInventory == null && !(character.Inventory as Sword).HasHandle)
        {
            swordInventory = (character as Player).PlaceSwordInWorkstation();
            swordInventory.transform.parent = swordPosition;
            swordInventory.transform.localPosition = Vector3.zero;
        }
        else if (character.Inventory is Handle && woodInventory == null)
        {
            if(!(swordInventory != null && (swordInventory as Sword).HasHandle))
            {
                woodInventory = (character as Player).PlaceHandleInObject();
                woodInventory.transform.parent = woodPosition;
                woodInventory.transform.localPosition = Vector3.zero;
            }
        }
        else if (character.Inventory == null && swordInventory != null)
        {
            character.PickUpItem(swordInventory);
            swordInventory = null;
        }
        else if (character.Inventory == null && woodInventory != null)
        {
            character.PickUpItem(swordInventory);
            swordInventory = null;
        }
        return this;
    }

    public override void Start()
    {
        anim = GetComponent<Animator>();
        base.Start();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void OnDrawGizmos()
    {
        if (woodPosition != null)
        {
            Gizmos.color = new Color(1,0.5f,0,1);
            Gizmos.DrawSphere(woodPosition.position, 0.1f);
        }
        if (swordPosition != null)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawSphere(swordPosition.position, 0.1f);
        }
        if (workingPosition != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(workingPosition.position, 0.1f);
        }
    }
}
