using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardState {

    private string name;
    private int cost;
    private List<Task> tasks;
    public AudioClip soundEffect;

    public CardState(string name, int cost, List<Task> tasks, string audioPath){
        this.name = name;
        this.cost = cost;
        this.tasks = tasks;
        soundEffect = Resources.Load<AudioClip>(audioPath);
    }

    public List<Task> getTasks() {
        return tasks;
    }

    public int GetCost() {
        return cost;
    }

    public void RunTask(GameObject input, int i){
        tasks[i].Run(input);
    }

    override
    public string ToString() {
        return name;
    }

    public virtual IEnumerator Coroutine(GameObject g){
        yield return null;
    }
}

public class StrikeState : CardState {
    private static string NAME = "Strike";
    private static int COST = 1;

    public StrikeState() : base(NAME, COST, GetTasks(), "Sounds/PunchSound") {
    }

    private static List<Task> GetTasks() {
        return new List<Task> {
            new Damage(10)
        };
    }
}

public class HealState : CardState
{
    private static string NAME = "Heal";
    private static int COST = 1;

    public HealState() : base(NAME, COST, GetTasks(), "Sounds/PunchSound")
    {
    }

    private static List<Task> GetTasks()
    {
        return new List<Task> {
            new Heal(10)
        };
    }
}