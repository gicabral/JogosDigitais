using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAtaque : State
{
    SteerableBehaviour steerable;
    IShooter shooter;

    GameManager gm;

    public float shootDelay = 0.5f;
    private float _lastShootTimestamp = 0.0f;

    void Start(){
        gm = GameManager.GetInstance();
    }

    public override void Awake()
    {
        base.Awake();

        Transition Patrulha = new Transition();
        Patrulha.condition = new ConditionDistGT(transform, GameObject.FindWithTag("Player").transform, 2.0f);
        Patrulha.target = GetComponent<StatePatrulha>();
        transitions.Add(Patrulha);

        steerable = GetComponent<SteerableBehaviour>();

        shooter = steerable as IShooter;
        if(shooter == null){
            throw new MissingComponentException("Esse GameObject não implementa IShooter");
        }
    }

    public void Update(){
        if (gm.gameState != GameManager.GameState.GAME) return;
        if(Time.time - _lastShootTimestamp < shootDelay) return;
        _lastShootTimestamp = Time.time;
        shooter.Shoot();
    }
}
