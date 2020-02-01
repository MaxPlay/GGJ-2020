using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInteractable : MonoBehaviour
{


//von Benny
[SerializeField]
private bool stationIsInUse;
[SerializeField]
private float itemGrindingValue;
[SerializeField]
private float itemGrindingStartValue;
[SerializeField]
private float itemGrindingFinishValue;





//Gamestuff
private GameObject progressbar;
private float distanceProgressbarToStation = (float) 1.0;

private StationProgressbar stationProgressbar;



//inner logic

[SerializeField]
private int stationIdentifier;
[SerializeField]
private float progressSpeed;




    // Start is called before the first frame update
    void Start()
    {

GameObject canvasGui = GameObject.Find("CanvasWorldGui");
        
progressbar = transform.Find("Progressbar").gameObject;
progressbar.transform.SetParent(canvasGui.transform);

stationProgressbar = progressbar.GetComponent<StationProgressbar>();


    }




void Update() {

//Test
// itemGrindingValue += progressSpeed * Time.deltaTime;
// float percentDone = GetProgressInPercent(itemGrindingStartValue, itemGrindingFinishValue, itemGrindingValue);







}









 void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.GetComponent<Player>() != null) && (stationIsInUse))
        {





UseStation(stationIdentifier);


        
        }

        else if ((other.gameObject.GetComponent<Player>() != null) && (!stationIsInUse))

        {

        }

    }













private void UseStation(int stationIdentifier) {

switch(stationIdentifier) {

case 0:
//Grinding

//Todo Animation

itemGrindingValue += progressSpeed * Time.deltaTime;

float percentDone = GetProgressInPercent(itemGrindingStartValue, itemGrindingFinishValue, itemGrindingValue);

//Give to Progressbar
stationProgressbar.SetProgressbar(percentDone);

break;




default:

Debug.Log("UseStation: The fuck happened?");

break;

}

}

private float GetProgressInPercent(float start, float finish, float present) {

float hundredPercent = finish - start;

float percentage = (present - start) / hundredPercent * 100;

return percentage;

}

}
