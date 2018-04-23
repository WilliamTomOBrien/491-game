using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task {

    public enum Input {
        Card,
        Entity,
        Enemy,
        Player,
        DiscardCard,
        Null
    };

    public Input type;
    public virtual void Run(GameObject input) {}
    public virtual void Set(float f) {}
    public virtual Task Clone(){
        return new Task();
    }
}


public class Damage : Task {

    int damage;

    public Damage(int damage) {
        this.damage = damage;
        this.type = Input.Entity;
    }

    override public void Set(float f){
        damage = (int) f;
    }

    override public Task Clone(){
        return new Damage(damage);
    }

    override public void Run(GameObject input){
        Entity p = input.GetComponent<Entity>();
        p.Damage(damage);
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

    public Example() {
        this.type = Input.Null;
    }

    override public void Run(GameObject input) {
        Debug.Log("Task Run!");
    }
}

public class Heal : Task
{
    int damage = 0;

    public Heal(int damage) {
        this.damage = damage;
        this.type = Input.Entity;
    }

    override public void Run(GameObject input) {
        Entity p = input.GetComponent<Entity>();
        p.Heal(damage);
    }
}