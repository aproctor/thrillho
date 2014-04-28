using UnityEngine;
using System.Collections;

public class Monster : Unit {

  public enum MonsterStates {
    Idle,
    Aggro,
    Dead
  }

  public MonsterStates state = MonsterStates.Idle;

  public override void Update() {
    if(state == MonsterStates.Aggro) {
      FireUpdate();
    }
  }

  public void Aggro() {
    if(state == MonsterStates.Idle) {
      state = MonsterStates.Aggro;
      this.OnAggro();
    }
  }

	public override void Die ()
	{
		//Don't call base.Die() here that will destroy it
		state = MonsterStates.Dead;
	}

  public virtual void OnAggro() {
    //Override this to allow shooting
  }

  private void FireUpdate() {
    if(unitConfig == null || unitConfig.autoShoot == false) {
      return;
    }


    float curTime = Time.fixedTime;
    if(curTime > this.lastShootTime + unitConfig.fireCooldown) {

      if(unitConfig.bulletPrefab == null) {
        Debug.LogError("Unable to fire without a bulletPrefab", this);
      } else {
        GameObject.Instantiate(unitConfig.bulletPrefab, this.transform.position, Quaternion.identity);
      }

      this.lastShootTime = curTime;
    }
  }
}
