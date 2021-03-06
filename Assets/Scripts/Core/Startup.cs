﻿using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

  [SerializeField]
  private GameObject tapToStartUi;

  [SerializeField]
  private GameObject splashUi;

  private bool acceptingInput = false;

  void Start() {
    Game.Initialize();
    AudioManager.Instance.StopMusic();
    acceptingInput = true;
  }

  void Update() {
    if(acceptingInput && Input.anyKeyDown) {
      acceptingInput = false;
      tapToStartUi.SetActive(false);
      LoadGame();
    }
  }

  private void LoadGame() {
    AudioManager.Instance.PlaySound("Title/ThrillhouseSHORT2");
    splashUi.SetActive(true);

    UkenTimer.SetTimeout(4f, () => {
      Game.LoadScene(Game.Scenes.Welcome);
    });
  }
}
