using UnityEngine;

[CreateAssetMenu(menuName = "Data/Control Settings")]
public class ControlSettings : ScriptableObject
{
    [SerializeField]
    private KeyCode up;

    public KeyCode Up
    {
        get { return up; }
    }

    [SerializeField]
    private KeyCode down;

    public KeyCode Down
    {
        get { return down; }
    }

    [SerializeField]
    private KeyCode left;

    public KeyCode Left
    {
        get { return left; }
    }

    [SerializeField]
    private KeyCode right;

    public KeyCode Right
    {
        get { return right; }
    }

    [SerializeField]
    private KeyCode interact;

    public KeyCode Interact
    {
        get { return interact; }
    }

    [SerializeField]
    private KeyCode pause;

    public KeyCode Pause
    {
        get { return pause; }
    }
}
