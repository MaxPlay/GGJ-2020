using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class LinearPath
{
    [SerializeField]
    private Vector3 start;

    [SerializeField]
    private Vector3 end;

    public Vector3 End => end;

    public Vector3 Start => start;

    public Vector3 Owner { get; set; }

    public Vector3 Lerp(float amount)
    {
        return Vector3.Lerp(start + Owner, end + Owner, amount);
    }
}
