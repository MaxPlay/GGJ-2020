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


rectProg.localScale = new Vector3(0,1,1);
    }







public void SetProgressbar(float presentBar) {

float divideByHundred = presentBar / 100;

Vector3 katze = new Vector3(divideByHundred, 1, 1);

   rectProg.localScale = katze;

}













}
