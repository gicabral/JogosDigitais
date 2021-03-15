using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotsBehaviour : SteerableBehaviour
{
    GameManager gm;

    private void OnTriggerEnter2D(Collider2D other){
        if (gm.gameState != GameManager.GameState.GAME) return;

        if(other.CompareTag("Player")) return;

        IDamageable damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if(!(damageable is null)){
            damageable.TakeDamage();
        }
        if (other.CompareTag("Inimigos"))
        {
            Destroy(other.gameObject);
            gm.pontos+= 2000;
        }
        if (other.CompareTag("InimigoPersegue"))
        {
            Destroy(other.gameObject);
            gm.pontos+= 1000;
        }
        if (other.CompareTag("InimigoTiro"))
        {
            Destroy(other.gameObject);
            gm.pontos+= 100;
        }
        if (other.CompareTag("InimigoRochaG"))
        {
            Destroy(other.gameObject);
            gm.pontos+= 100;
        }
        if (other.CompareTag("InimigoRochaM"))
        {
            Destroy(other.gameObject);
            gm.pontos+=200;
        }
        if (other.CompareTag("InimigoRochaP"))
        {
            Destroy(other.gameObject);
            gm.pontos+=300;
        }
        if(gm.pontos > gm.highscore){
            gm.highscore = gm.pontos;
        }
        // if(gm.pontos == 10000){
        //     gm.ChangeState(GameManager.GameState.ENDGAME);
        // }
        Destroy(gameObject);
    }
    void Awake(){
        gm = GameManager.GetInstance();
    }
    private void Update(){
        if (gm.gameState != GameManager.GameState.GAME) return;
        // Thrust(0,1);
    }
    
}
