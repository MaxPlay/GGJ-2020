using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlefieldSlider : MonoBehaviour {

private PanelManager panelManager;

    private Slider slider;

    [SerializeField]
    private GameObject customerLine;

    //Copies from GameManager

    [SerializeField]
    float secondsPerRound;

    [SerializeField]
    int customersWithoutPenalty;

    [SerializeField]
    int penaltyEndOfRoundPerCustomer;

    [SerializeField]
    int ratioPointsCustomerSatisfied;

    [SerializeField]
    int ratioPointsCustomerImpressed;

    [SerializeField]
    int ratioPointsCustomerDiappointed;

    //inner logic

    private int gameScore;

    private int scoreLost;

    private int scoreWon;

    private bool gameIsOver = false;

    void Start () {

panelManager = GameObject.Find("PanelNotifications").GetComponent<PanelManager>();

        slider = GetComponent<Slider> ();

        SetupScoreRange ();



        //TEST

        InitAndStartBattlePointLoop();

    }

    public void SetupScoreRange () {

        slider.minValue = GameManager.Instance.Settings.ScoreLose;

        slider.maxValue = GameManager.Instance.Settings.ScoreWin;

        slider.value = GameManager.Instance.Settings.Score;

        gameScore = GameManager.Instance.Settings.Score;

        scoreWon = GameManager.Instance.Settings.ScoreWin;

        scoreLost = GameManager.Instance.Settings.ScoreLose;

    }

    public void SetScore (int score) {

        slider.value = score;

    }

    //**********************************************************************************************
    //**********************************************************************************************
    // Game Over Stuff
    //**********************************************************************************************
    //**********************************************************************************************

    public void LooseGame () { 
        
        Debug.Log ("DUMMY: YOU LOST YOU IDIOT!");
        
        gameIsOver = true;

        panelManager.ShowNotification("YOU LOST KAREN!");
         }

    public void WinGame () {
        
         Debug.Log ("DUMMY: YOU WON YOU ARROGANT PRICK!");

        panelManager.ShowNotification("YOU WON PIKACHU!");
         
           gameIsOver = true;
          }

    public int CountCustomers () {

        int childCount = 0;

        foreach (Transform b in customerLine.transform) {
            //        Debug.Log("Child: "+b);
            childCount++;
            //            childCount += CountChildren(b);
        }
        return childCount;

    }

    public void InitAndStartBattlePointLoop () {

        secondsPerRound = GameManager.Instance.Settings.SecondsPerRound;

        customersWithoutPenalty = GameManager.Instance.Settings.CustomersWithoutPenalty;

        penaltyEndOfRoundPerCustomer = GameManager.Instance.Settings.PenaltyEndOfRoundPerCustomer;

        ratioPointsCustomerSatisfied = GameManager.Instance.Settings.RatioPointsCustomerSatisfied;

        ratioPointsCustomerImpressed = GameManager.Instance.Settings.RatioPointsCustomerImpressed;

        ratioPointsCustomerDiappointed = GameManager.Instance.Settings.RatioPointsCustomerDiappointed;

        StartCoroutine (LoosePointsOverTime ());
    }

    IEnumerator LoosePointsOverTime () {

        while(!gameIsOver) {

        int waitingCustomers = CountCustomers ();

        if (waitingCustomers > customersWithoutPenalty) {

            int penalty = waitingCustomers * penaltyEndOfRoundPerCustomer;

            gameScore -= penalty;

            SetScore(gameScore);

            Debug.Log ("LoosePointsOverTime: Your penalty is: "+penalty+", waiting are: "+waitingCustomers);


            if (gameScore <= scoreLost) { LooseGame (); }

        } else {Debug.Log ("LoosePointsOverTime: No penalty for you!");}

        yield return new WaitForSeconds (secondsPerRound);

        }

    }

    public void CheckIfWon () {

        if (gameScore >= scoreWon) { WinGame (); }

    }




}