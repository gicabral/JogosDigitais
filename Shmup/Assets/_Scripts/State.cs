using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public List<Transition> transitions;
    public virtual void Awake()
    {
        transitions = new List<Transition>();
    }

    public void LateUpdate(){
        foreach(Transition t in transitions){
            if(t.condition.Test()){
                t.target.enabled = true;
                this.enabled = false;
                return;
            }
        }
    }
}
