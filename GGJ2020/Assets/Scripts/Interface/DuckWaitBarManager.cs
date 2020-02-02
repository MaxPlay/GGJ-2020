using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckWaitBarManager : MonoBehaviour
{
    GameObject[] duckWaitBars;

    [SerializeField]
    GameObject template;

    private int duckRequestCounter = -1;

    void Start()
    {
        duckWaitBars = new GameObject[GameManager.Instance.Settings.MaxObjectives];
        for (int i = 0; i < duckWaitBars.Length; i++)
        {
            duckWaitBars[i] = Instantiate(template, transform);
        }
    }


    public GameObject GiveWaitBarToDuck()
    {
        ++duckRequestCounter;

        if (duckRequestCounter >= GameManager.Instance.Settings.MaxObjectives)
        {
            duckRequestCounter = 0;
        }

        return duckWaitBars[duckRequestCounter];

    }
}