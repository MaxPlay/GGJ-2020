using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DuckWaitBar : MonoBehaviour {

    private RectTransform rectBar_Green;
    private RectTransform rectBar_Blue;
    private RectTransform rectBar_Red;

    [SerializeField]
    private Image barGreen;
    [SerializeField]
    private Image barBlue;
    [SerializeField]
    private Image barRed;
    [SerializeField]
    private Image background;

    //inner logic
    private float totalWaitTime;
    [SerializeField]
    private float waitTimeBonus = 7.0f;

    [SerializeField]
    private float waitTimeWarning = 3.0f;

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

    public void SetPositionToDuck (Transform duck) {

        Vector3 duckPos = duck.transform.position;

        transform.position = new Vector3 (duckPos.x, duckPos.y + 1, duckPos.z);

        //     transform.position = duck.transform.position;
        // transform.parent = duck.transform;

        EnableAllBars ();
        EnableGreen ();
    }

    public void SetWaitBar (float timeLeft) {

        float barSize =  (totalWaitTime - timeLeft) * 10;

        rectBar_Green.sizeDelta = new Vector2 (barSize, 100);
        rectBar_Blue.sizeDelta = new Vector2 (barSize, 100);
        rectBar_Red.sizeDelta = new Vector2 (barSize, 100);

        if (timeLeft < waitTimeBonus) { EnableBlue (); }

        if (timeLeft < waitTimeWarning) { EnableRed (); }

     //   Debug.Log ("SetWaitBar: Setting % to: " + barSize + ", totalWaitTime: " + totalWaitTime + ", time left: " + timeLeft);

    }

    public void DisableBars () {

        barGreen.enabled = false;
        barBlue.enabled = false;
        barRed.enabled = false;
        background.enabled = false;
    }

    public void EnableAllBars () {

        barGreen.enabled = true;
        barBlue.enabled = true;
        barRed.enabled = true;
        background.enabled = true;
    }

    public void EnableGreen () {

        // barGreen.SetActive (true);
        // barBlue.SetActive (false);
        // barRed.SetActive (false);

        barGreen.enabled = true;
        barBlue.enabled = false;
        barRed.enabled = false;

    }

    public void EnableBlue () {

        // barGreen.SetActive (false);
        // barBlue.SetActive (true);
        // barRed.SetActive (false);

        barGreen.enabled = false;
        barBlue.enabled = true;
        barRed.enabled = false;

    }

    public void EnableRed () {

        // barGreen.SetActive (false);
        // barBlue.SetActive (false);
        // barRed.SetActive (true);

        barGreen.enabled = false;
        barBlue.enabled = false;
        barRed.enabled = true;

    }

}