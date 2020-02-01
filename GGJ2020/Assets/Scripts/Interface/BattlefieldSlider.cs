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

    private Slider slider;

    // Start is called before the first frame update
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

}