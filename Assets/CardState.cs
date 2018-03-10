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