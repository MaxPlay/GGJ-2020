using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationProgressbar : MonoBehaviour
{
[SerializeField]
private RectTransform rectProg;


    // Start is called before the first frame update
    void Start()
    {
        rectProg = transform.Find("Progressbar_Front").GetComponent<RectTransform>();


  rectProg.sizeDelta = new Vector2(0, 100);
    }







public void SetProgressbar(float presentBar) {

int pixelsBar = (int) presentBar * 100;

Debug.Log("SetProgressbar: Setting % to: "+presentBar);

// Vector3 katze = new Vector3(presentBar, 1, 1);

//    rectProg.localScale = katze;



   rectProg.sizeDelta = new Vector2(pixelsBar, 100);

}













}
