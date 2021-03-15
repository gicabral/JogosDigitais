using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotsEnemyBehaviour : SteerableBehaviour
{

    Vector3 direction;
    GameManager gm;

    private void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Inimigos")) return;

        IDamageable damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if(!(damageable is null)){
            damageable.TakeDamage();
        }
        Destroy(gameObject);
    }

    private void Start(){
        gm = GameManager.GetInstance();
    }
    private void Update(){
        if (gm.gameState != GameManager.GameState.GAME) return;
        Vector3 posPlayer = GameObject.FindWithTag("Player").transform.position;
        direction = (posPlayer - transform.position).normalized;
        Thrust(direction.x,direction.y);
    }

    private void OnBecameInvisible(){
        gameObject.SetActive(false);
    }
    
}
