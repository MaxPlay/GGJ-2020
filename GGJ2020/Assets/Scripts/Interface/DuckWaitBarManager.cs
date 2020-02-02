using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckWaitBarManager : MonoBehaviour
{

    GameObject[] duckWaitBar = new GameObject[5];

    private int duckRequestCounter = -1;

    void Start()
    {

        int i = 0;

        foreach (Transform child in transform)
        {

            duckWaitBar[i] = child.gameObject;

            i += 1;

        }

    }


    public GameObject GiveWaitBarToDuck()
    {

        duckRequestCounter += 1;

        if (duckRequestCounter > 4) { duckRequestCounter = 0; }

        return duckWaitBar[duckRequestCounter];

    }


}