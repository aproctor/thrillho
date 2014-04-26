using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(BoxCollider2D))]
public class CarvalloAttack : MonoBehaviour {

  [SerializeField]
  private CarvalloController controller;
  [SerializeField]
  private GameObject bulletPrefab;
  [SerializeField]
  private float speed;

  private List<Monster> monstersInRange = new List<Monster>();
  private List<Bullet> bulletsInRange = new List<Bullet>();

  private BoxCollider2D boxCollider;

  void Start(){
    boxCollider = GetComponent<BoxCollider2D>();
    controller.MeleeFired += Melee;
    controller.RangedFired += Ranged;
  }

  void Melee(float horizontal, float vertical){
    Debug.Log("SWING");
    foreach(Monster monster in monstersInRange){
      monster.Die();
    }
    List<Bullet> toRemove = new List<Bullet>();
    foreach(Bullet bullet in bulletsInRange){
      if(bullet == null){
        toRemove.Add(bullet);
        continue;
      }
      if(bullet.GetType() != bulletPrefab.GetType()){
        if(bullet.transform.position.x > controller.transform.position.x
           && bullet.velocity.x < 0){
          bullet.velocity = new Vector3(-bullet.velocity.x, bullet.velocity.y, bullet.velocity.z);
        } else if(bullet.transform.position.x < controller.transform.position.x
                  && bullet.velocity.x > 0){
          bullet.velocity = new Vector3(-bullet.velocity.x, bullet.velocity.y, bullet.velocity.z);
        }
        if(bullet.transform.position.y > controller.transform.position.y
           && bullet.velocity.y < 0){
          bullet.velocity = new Vector3(bullet.velocity.x, -bullet.velocity.y, bullet.velocity.z);
        } else if(bullet.transform.position.y < controller.transform.position.y
                  && bullet.velocity.y > 0){
          bullet.velocity = new Vector3(bullet.velocity.x, -bullet.velocity.y, bullet.velocity.z);
        }
        KillVolume kv = bullet.GetComponent<KillVolume>();
        if(kv){
          kv.targets = bulletPrefab.GetComponent<KillVolume>().targets;
        }
      }
    }
    foreach (Bullet b in toRemove){
      bulletsInRange.Remove(b);
    }
  }

  void Ranged(float horizontal, float vertical){
    GameObject bulletGO = GameObject.Instantiate(bulletPrefab,transform.position,transform.rotation) as GameObject;
    Bullet bullet = bulletGO.GetComponent<Bullet>();
    bullet.velocity = new Vector3(horizontal, -vertical, 0).normalized*speed;
  }

  void OnTriggerEnter2D(Collider2D collider){
    Monster m = collider.GetComponent<Monster>();
    if(m){
      monstersInRange.Add(m);
    }
    Bullet b = collider.GetComponent<Bullet>();
    if(b){
      bulletsInRange.Add(b);
    }
  }

  void OnTriggerExit2D(Collider2D collider){
    Monster m = collider.GetComponent<Monster>();
    if(m && monstersInRange.Contains(m)){
      monstersInRange.Remove(m);
    }
    Bullet b = collider.GetComponent<Bullet>();
    if(b && bulletsInRange.Contains(b)){
      bulletsInRange.Remove(b);
    }
  }

}