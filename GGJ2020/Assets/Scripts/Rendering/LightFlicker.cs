using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    AnimationCurve curve;

    float time;

    new Light light;

    float baseIntensity;

    private void Start()
    {
        light = GetComponent<Light>();
        baseIntensity = light.intensity;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
            time -= 1;

        light.intensity = baseIntensity + curve.Evaluate(time);
    }
}
