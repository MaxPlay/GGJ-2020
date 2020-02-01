using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationProgressbar : MonoBehaviour {

    private RectTransform rectProg;

    [SerializeField]
    private GameObject fletchingStation;

    // Start is called before the first frame update
    void Start () {
        rectProg = transform.Find ("Progressbar_Front").GetComponent<RectTransform> ();

        rectProg.sizeDelta = new Vector2 (0, 100);

        Vector3 parentPos = fletchingStation.transform.position;

        transform.position = new Vector3 (parentPos.x, parentPos.y + 1, parentPos.z);

        transform.gameObject.SetActive (false);

    }

    public void SetProgressbar (float presentBar) {

        int pixelsBar = (int) presentBar * 100;

        Debug.Log ("SetProgressbar: Setting % to: " + presentBar);

        // Vector3 katze = new Vector3(presentBar, 1, 1);

        //    rectProg.localScale = katze;

        rectProg.sizeDelta = new Vector2 (pixelsBar, 100);

    }

    public void DisableBar () { transform.gameObject.SetActive (false); }
    public void EnableBar () { transform.gameObject.SetActive (true); }

}