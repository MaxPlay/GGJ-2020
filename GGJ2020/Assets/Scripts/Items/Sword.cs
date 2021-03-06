﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item
{
    private bool hasHandle;
    private float sharpness;
    private float quality;
    private float heat;

    public Customer Owner { get; set; }

    bool isHeatingUp;

    private const bool debug = false;

    public bool HasHandle { get => hasHandle; }
    public float Sharpness { get => sharpness; }
    public float Quality { get => quality; }
    public float Heat { get => heat; }

    [SerializeField]
    GameObject iconBroken;
    [SerializeField]
    GameObject iconStump;
    [SerializeField]
    GameObject iconGrip;

    public override Interactable Interact(Character character)
    {
        return base.Interact(character);
    }

    public float HeatSword(float strength)
    {
        heat += strength;
        if (strength > 0)
            sharpness = Mathf.Clamp01(sharpness - strength);
        if (heat < 0)
            heat = 0;
        isHeatingUp = true;
        if (debug)
            Debug.Log("<b>[Sword]</b> New Heat: " + heat);
        return heat;
    }

    public float SharpenSword(float strength)
    {
        sharpness = Mathf.Clamp01(sharpness + strength);
        if (debug)
            Debug.Log("<b>[Sword]</b> New Sharpness: " + sharpness);
        return sharpness;
    }

    public float HammerSword(float strength)
    {
        if (heat > settings.MinimumHeatToForge)
        {
            quality = Mathf.Clamp01(quality + strength);
            sharpness = Mathf.Clamp01(sharpness - strength);
        }
        if (debug)
            Debug.Log("<b>[Sword]</b> New Quality: " + quality);
        return quality;
    }

    public bool AddHandle()
    {
        if (hasHandle)
        {
            if (debug)
                Debug.Log("<b>[Sword]</b> Already Had a Handle");
            return false;
        }

        hasHandle = true;
        if (debug)
            Debug.Log("<b>[Sword]</b> Added Handle");
        return true;
    }

    public void Initialize(Customer owner, Objective objective)
    {
        Owner = owner;
        hasHandle = !objective.Grip;
        sharpness = objective.Grind ? 0 : 1;
        quality = objective.Smith ? 0 : 1;
        heat = 0;
    }

    private void Update()
    {
        if (!isHeatingUp && settings.SwordTimeToAutoCoolDown > 0)
        {
            heat = Mathf.Clamp01(heat - Time.deltaTime / settings.SwordTimeToAutoCoolDown);
        }
        isHeatingUp = true;

        iconStump.SetActive(sharpness < 0.95f);
        iconGrip.SetActive(!hasHandle);
        iconBroken.SetActive(quality < 0.95f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (!hasHandle)
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.3f, 0.1f);
        if (heat > 0.2f)
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.6f, 0.1f);
        if (sharpness < 1)
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.9f, 0.1f);
        if (quality < 1)
            Gizmos.DrawSphere(transform.position + Vector3.up * 1.2f, 0.1f);

        Gizmos.color = Color.green;
        if (hasHandle)
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.3f, 0.1f);
        if (heat <= 0.2f)
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.6f, 0.1f);
        if (sharpness >= 1)
            Gizmos.DrawSphere(transform.position + Vector3.up * 0.9f, 0.1f);
        if (quality >= 1)
            Gizmos.DrawSphere(transform.position + Vector3.up * 1.2f, 0.1f);
    }
}
