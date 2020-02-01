﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
  //Panel
  private RectTransform PanelRect;

    private Text message;

  void Start () {

    message = transform.Find ("message").GetComponent<Text> ();

    PanelRect = GetComponent<RectTransform> ();
    SwitchPanelOff();
  }

  public void SwitchPanelOn () {
    PanelRect.localScale = new Vector3 (1, 1);
  }

  public void SwitchPanelOff () {
    PanelRect.localScale = new Vector3 (0, 0);
  }

  public void ShowNotification (string textstuff) {

message.text = textstuff;
StartCoroutine(DisplayNotification()); 

  }


IEnumerator DisplayNotification(){

SwitchPanelOn();

float katze = 5.0f;

yield return new WaitForSeconds(katze);

SwitchPanelOff();
}





}