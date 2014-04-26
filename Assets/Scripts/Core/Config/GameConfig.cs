using UnityEngine;

public class GameConfig : ScriptableObject {
  public GameConfig() {
  }

  public int maxLives = 8;


  private static GameConfig _instance = null;
  public static GameConfig Instance {
    get {
      if(_instance == null) {
        _instance = (GameConfig)Resources.Load("ScriptableObjects/GameConfig");
      }
      return _instance;
    }
  }
}
