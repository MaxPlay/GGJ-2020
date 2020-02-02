using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckWaitBar : MonoBehaviour {

    private RectTransform rectBar_Green;
    private RectTransform rectBar_Blue;
    private RectTransform rectBar_Red;

    [SerializeField]
    private GameObject barGreen;
    [SerializeField]
    private GameObject barBlue;
    [SerializeField]
    private GameObject barRed;

    //inner logic
    private float totalWaitTime;
    [SerializeField]
    private float waitTimeBonus = 7.0f;

    [SerializeField]
    private float waitTimeWarning = 3.0f;

    // Update is called once per frame
    void Update () {

    }

    //   [SerializeField]

    void Start () {

        rectBar_Green = barGreen.GetComponent<RectTransform> ();
        rectBar_Blue = barBlue.GetComponent<RectTransform> ();
        rectBar_Red = barRed.GetComponent<RectTransform> ();

        rectBar_Green.sizeDelta = new Vector2 (0, 100);
        rectBar_Blue.sizeDelta = new Vector2 (0, 100);
        rectBar_Red.sizeDelta = new Vector2 (0, 100);

        totalWaitTime = GameManager.Instance.Settings.WaitTimer;

        EnableGreen ();

    }

    public void SetParentDuck (Transform duck) {

        transform.position = duck.transform.position;
        transform.parent = duck.transform;

    }

    public void SetWaitBar (float timeLeft) {

        int barSize = (int) (timeLeft / totalWaitTime) * 100;

        rectBar_Green.sizeDelta = new Vector2 (barSize, 100);
        rectBar_Blue.sizeDelta = new Vector2 (barSize, 100);
        rectBar_Red.sizeDelta = new Vector2 (barSize, 100);

        if (timeLeft < waitTimeBonus) {EnableBlue();}

        if (timeLeft < waitTimeWarning) {EnableRed();}

        

        Debug.Log ("SetWaitBar: Setting % to: " + barSize);

    }

    public void EnableGreen () {

        barGreen.SetActive (true);
        barBlue.SetActive (false);
        barRed.SetActive (false);

    }

    public void EnableBlue () {

        barGreen.SetActive (false);
        barBlue.SetActive (true);
        barRed.SetActive (false);

    }

    public void EnableRed () {

        barGreen.SetActive (false);
        barBlue.SetActive (false);
        barRed.SetActive (true);

    }

}