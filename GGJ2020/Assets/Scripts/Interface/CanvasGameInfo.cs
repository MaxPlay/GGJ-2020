using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameInfo : MonoBehaviour {

    GameObject progressbarGrindingStation;

    FletchingStation fletchingStation;

    //inner logic
    private bool stuffWasInitiated = false;

    // Start is called before the first frame update
    void Start () {

        fletchingStation = GameObject.Find ("GrindingStation").GetComponent<FletchingStation> ();

        initCanvasShit ();

    }

    // Update is called once per frame
    void Update () {

        if (!stuffWasInitiated) { return; } else {

            progressbarGrindingStation.GetComponent<StationProgressbar> ().SetProgressbar (fletchingStation.TimeToGrind);

        }

    }

    public void initCanvasShit () {

        progressbarGrindingStation = transform.Find ("ProgressbarGrinding").gameObject;

        Vector3 grindingStation = GameObject.Find ("GrindingStation").transform.position;

        Vector3 adjustedPos = new Vector3 (grindingStation.x, grindingStation.y + 4, 0);

        progressbarGrindingStation.transform.position = adjustedPos;

        stuffWasInitiated = true;

    }

    // private float GetProgressInPercent(float start, float finish, float present) {

    // float hundredPercent = finish - start;

    // float percentage = (present - start) / hundredPercent * 100;

    // return percentage;

    // }

}