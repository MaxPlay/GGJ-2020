using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DuckWaitBar : MonoBehaviour
{

    private RectTransform rectBar;

    [SerializeField]
    private Image bar;
    [SerializeField]
    private Image background;

    //inner logic
    private float totalWaitTime;
    [SerializeField]
    [Range(0,1)]
    private float waitTimeBonus = 0.3f;
    [SerializeField]
    [Range(0,1)]
    private float waitTimeWarning = 0.7f;

    void Start()
    {

        rectBar = bar.GetComponent<RectTransform>();

        bar.fillAmount = 0;
        bar.color = Color.green;

        totalWaitTime = GameManager.Instance.Settings.WaitTimer;
    }

    public void SetPositionToDuck(Transform duck)
    {
        Vector3 duckPos = duck.transform.position;

        transform.position = new Vector3(duckPos.x, duckPos.y + 1, duckPos.z);

        EnableAllBars();
        bar.color = Color.green;
    }

    public void SetWaitBar(float timeLeft)
    {
        float percentage = 1 - (timeLeft / totalWaitTime);

        bar.fillAmount = 1 - percentage;

        if (percentage < waitTimeBonus)
        {
            bar.color = Color.green;
        }
        else if (percentage < waitTimeWarning)
        {
            bar.color = Color.blue;
        }
        else
        {
            bar.color = Color.red;
        }
    }

    public void DisableBars()
    {
        bar.enabled = false;
        background.enabled = false;
    }

    public void EnableAllBars()
    {
        bar.enabled = true;
        background.enabled = true;
    }
}