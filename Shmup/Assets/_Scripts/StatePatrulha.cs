using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrulha : State
{
    SteerableBehaviour steerable;
    GameManager gm;

    void Start(){
        gm = GameManager.GetInstance();
    }

    public override void Awake()
    {
        base.Awake();

        Transition Atack = new Transition();
        Atack.condition = new ConditionDistLT(transform, GameObject.FindWithTag("Player").transform, 2.0f);
        Atack.target = GetComponent<StateAtaque>();
        transitions.Add(Atack);

        steerable = GetComponent<SteerableBehaviour>();
    }

    float angle = 0;

    private void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        angle += 0.1f;
        Mathf.Clamp(angle, 0.0f, 2.0f * Mathf.PI);
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);

        steerable.Thrust(x, y);
       
    }
}
