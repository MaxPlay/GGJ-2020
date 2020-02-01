using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpriteOrientation : MonoBehaviour
{
    public void UpdateOrientation(Transform relativeTransform)
    {
        Vector3 direction = relativeTransform.forward;
        transform.up = direction;
    }
}
