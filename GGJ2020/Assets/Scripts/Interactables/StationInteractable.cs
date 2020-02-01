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


//inner logic

[SerializeField]
private float progressSpeed;




    // Start is called before the first frame update
    void Start()
    {
        
    }



 void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.GetComponent<Player>() != null) && (stationIsInUse))
        {








        
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


break;




default:

Debug.Log("UseStation: The fuck happened?")

break;

















}


}



private float GetProgressInPercent(float start, float finish, float present) {

float hundredPercent = finish - start;

float percentage = (present - start) / hundredPercent * 100;

return percentage;

}



}
