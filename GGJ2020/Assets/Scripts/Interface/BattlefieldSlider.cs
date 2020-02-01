using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlefieldSlider : MonoBehaviour {

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

    void Start () {

        slider = GetComponent<Slider> ();

        SetupScoreRange ();

    }

    public void SetupScoreRange () {

        slider.minValue = GameManager.Instance.GetComponent<GameplaySettings> ().ScoreLose;

        slider.maxValue = GameManager.Instance.GetComponent<GameplaySettings> ().ScoreWin;

        slider.value = GameManager.Instance.GetComponent<GameplaySettings> ().Score;

        gameScore = GameManager.Instance.GetComponent<GameplaySettings> ().Score;

        scoreWon = GameManager.Instance.GetComponent<GameplaySettings> ().ScoreWin;

        scoreLost = GameManager.Instance.GetComponent<GameplaySettings> ().ScoreLose;

    }

    public void SetScore (int score) {

        slider.value = score;

    }

    //**********************************************************************************************
    //**********************************************************************************************
    // Game Over Stuff
    //**********************************************************************************************
    //**********************************************************************************************

    public void LooseGame () { Debug.Log ("DUMMY: YOU LOST YOU IDIOT!"); }

    public void WinGame () { Debug.Log ("DUMMY: YOU WON YOU ARROGANT PRICK!"); }

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

        secondsPerRound = GameManager.Instance.GetComponent<GameplaySettings> ().SecondsPerRound;

        customersWithoutPenalty = GameManager.Instance.GetComponent<GameplaySettings> ().CustomersWithoutPenalty;

        penaltyEndOfRoundPerCustomer = GameManager.Instance.GetComponent<GameplaySettings> ().PenaltyEndOfRoundPerCustomer;

        ratioPointsCustomerSatisfied = GameManager.Instance.GetComponent<GameplaySettings> ().RatioPointsCustomerSatisfied;

        ratioPointsCustomerImpressed = GameManager.Instance.GetComponent<GameplaySettings> ().RatioPointsCustomerImpressed;

        ratioPointsCustomerDiappointed = GameManager.Instance.GetComponent<GameplaySettings> ().RatioPointsCustomerDiappointed;

        StartCoroutine (LoosePointsOverTime ());
    }

    IEnumerator LoosePointsOverTime () {

        int waitingCustomers = CountCustomers ();

        if (waitingCustomers > customersWithoutPenalty) {

            int penalty = waitingCustomers * penaltyEndOfRoundPerCustomer;

            gameScore -= penalty;

            if (gameScore <= scoreLost) { LooseGame (); }

        }

        yield return new WaitForSeconds (secondsPerRound);

    }

    public void CheckIfWon () {

        if (gameScore >= scoreWon) { WinGame (); }

    }

}