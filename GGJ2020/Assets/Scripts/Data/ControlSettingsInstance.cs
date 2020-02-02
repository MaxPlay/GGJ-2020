using System;
using UnityEngine;

[Serializable]
public class ControlSettingsInstance
{
    [SerializeField]
    private KeyCode up;

    public KeyCode Up
    {
        get { return up; }
        set { up = value; }
    }

    [SerializeField]
    private KeyCode down;

    public KeyCode Down
    {
        get { return down; }
        set { down = value; }
    }

    [SerializeField]
    private KeyCode left;

    public KeyCode Left
    {
        get { return left; }
        set { left = value; }
    }

    [SerializeField]
    private KeyCode right;

    public KeyCode Right
    {
        get { return right; }
        set { right = value; }
    }

    [SerializeField]
    private KeyCode interact;

    public KeyCode Interact
    {
        get { return interact; }
        set { interact = value; }
    }

    [SerializeField]
    private KeyCode pause;

    public KeyCode Pause
    {
        get { return pause; }
        set { pause = value; }
    }
}