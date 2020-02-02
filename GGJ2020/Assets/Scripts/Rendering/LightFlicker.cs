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
    float baseAngle;
    private Gradient colorGradient;

    private void Start()
    {
        light = GetComponent<Light>();
        baseIntensity = light.intensity;
        baseAngle = light.innerSpotAngle;
        colorGradient = new Gradient()
        {
            colorKeys = new GradientColorKey[] { new GradientColorKey(light.color, 0.0f) }
        };
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
            time -= 1;

        light.intensity = baseIntensity + curve.Evaluate(time);
        light.innerSpotAngle = baseAngle + curve.Evaluate(time);
        light.color = colorGradient.Evaluate(curve.Evaluate(time) + 0.5f);
    }

    public void SetLightRange(Gradient colorGradient)
    {
        this.colorGradient = colorGradient;
    }
}
