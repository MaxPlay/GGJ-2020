using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlefieldSlider : MonoBehaviour {

    [SerializeField]
    int initialScore = 10;
    [SerializeField]
    int scoreWin = 50;
    [SerializeField]
    int scoreLose = 0;

        [SerializeField]
    int score = 10;

    private Slider slider;

    [SerializeField]
    private GameObject customerLine;

    [SerializeField]
    int ratioCustomerWaiting = 1;

    [SerializeField]
    int ratioCustomerSatisfied = 1;


    void Start () {

        slider = GetComponent<Slider> ();

        // SetupScoreRange (scoreLose, scoreWin, initialScore);
        // SetScore (initialScore);

    }

    public void SetupScoreRange (int min, int max, int start) {

        slider.minValue = min;

        slider.maxValue = max;

        slider.value = start;
    }

    public void SetScore (int score) {

        slider.value = score;

    }






//**********************************************************************************************
//**********************************************************************************************
// Game Over Stuff
//**********************************************************************************************
//**********************************************************************************************
    public int CountCustomers() {

         int childCount = 0;

         foreach (Transform b in customerLine.transform)
         {
     //        Debug.Log("Child: "+b);
             childCount ++;
//            childCount += CountChildren(b);
         }
         return childCount;

}

    public int LooseBattlePointsOverTime() {return 0;}




}