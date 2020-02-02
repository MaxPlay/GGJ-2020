using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckWaitBarManager : MonoBehaviour {

    GameObject[] duckWaitBar = new GameObject[5];

    private int duckBarCounter = -1;

    void Update () {

    }

    private RectTransform[] rectProg = new RectTransform[5];

    //   [SerializeField]

    void Start () {

        int i = 0;

        foreach (Transform child in transform) {

            duckWaitBar[i] = child.gameObject;

            i += 1;

        }

    }


    public GameObject GiveWaitBarToDuck () {

duckBarCounter += 1;

return duckWaitBar[duckBarCounter];

    }


}