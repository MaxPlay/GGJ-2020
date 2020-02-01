using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceProgressbar : MonoBehaviour {

    private RectTransform rectProg;

    [SerializeField]
    private GameObject furnaceStation;

    void Start () {
        rectProg = transform.Find ("Progressbar_Front").GetComponent<RectTransform> ();

        rectProg.sizeDelta = new Vector2 (0, 100);

        Vector3 parentPos = furnaceStation.transform.position;

//Fuck this
  //      transform.position = new Vector3 (parentPos.x, parentPos.y + 1, parentPos.z);

      DisableBar();



    }

    public void SetProgressbar (float presentBar) {

        int pixelsBar = (int) presentBar * 100;

        Debug.Log ("SetProgressbar: Setting % to: " + presentBar);

        rectProg.sizeDelta = new Vector2 (pixelsBar, 100);

    }

    public void DisableBar () {

        foreach (Transform child in transform)
            child.gameObject.SetActive (false);

    Debug.Log ("FurnaceProgressbar: OFF ");

    }
    public void EnableBar () {

        foreach (Transform child in transform)
            child.gameObject.SetActive (true);

    Debug.Log ("FurnaceProgressbar: ON ");

    }

    private void OnTriggerEnter (Collider other) {

    //       Debug.Log ("OnTriggerEnter: Somebody entered ");

        if (other.gameObject.GetComponent<Player> () != null) { EnableBar (); }

    }

    private void OnTriggerExit (Collider other) {

  //        Debug.Log ("OnTriggerEnter: Somebody left ");

        if (other.gameObject.GetComponent<Player> () != null) { DisableBar (); }

    }

}