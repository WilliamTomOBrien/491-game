using System;
using System.Collections.Generic;
using UnityEngine;


public class CardState {
    public string name;
    public int cost;

    public List<Task> tasks;

    public CardState(string name, int cost, List<Task> task){
        this.name = name;
        this.cost = cost;
        this.tasks = task;
    }

    public void RunTask(GameObject input, int i){
        tasks[i].Run(input);
    }
}

public class StrikeState : CardState {
    private static string NAME = "Strike";
    private static int COST = 1;

    public StrikeState() : base(NAME, COST, GetTasks()) {
    }

    private static List<Task> GetTasks() {
        return new List<Task> {
            new Damage(10)
        };
    }
}