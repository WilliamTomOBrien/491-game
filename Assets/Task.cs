using System;
using System.Collections.Generic;
using UnityEngine;



public class Task {

    public enum Input {
        Card,
        Entity,
        Enemy,
        Player,
        Null
    };

    public Input type;
    public virtual void Run(GameObject input) {
    }
}


public class Damage : Task {
    int damage = 0;
    
    override public void Run(GameObject input){
        Entity p = input.GetComponent<Entity>();
        p.hp -= damage;
    }

    public Damage(int damage){
        this.damage = damage;
        this.type = Input.Entity;
    }

}

public class DebugTask : Task {
    private int i;
    override public void Run(GameObject input){
        Debug.Log("Task " + i + " Run!");
    }

    public DebugTask(int i){
        this.type = Input.Null;
        this.i = i;
    }

}

public class Example : Task {
    override public void Run(GameObject input){
        Debug.Log("Task Run!");
    }

    public Example(){
        this.type = Input.Null;
    }

}