using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card {
    private string name;
    private int cost;
    public enum Rarity {
        Common = 0,
        Uncommon = 1,
        Rare = 2
    }
    Rarity rarity;

    private List<ITask> tasks;

    public Card(string name, int cost, Rarity rarity, List<ITask> tasks) {
        this.name = name;
        this.cost = cost;
        this.rarity = rarity;
        this.tasks = tasks;
    }

    public int GetCost() {
        return cost;
    }

    public Rarity GetRarity() {
        return rarity;
    }

    public void Run(GameState state) {

        foreach (ITask task in tasks) {
            task.Run(state, this);
        }
        state.GetCurrentEntity().Discard(this);
    }

    override
    public string ToString() {
        return name;
    }
}

public class Attack : Card {
    private static string name = "Attack";
    private static int cost = 1;
    private static Rarity rarity = Rarity.Common;
    private static List<ITask> taskList = new List<ITask> {
            new ExampleTask()
        };

    public Attack() : base(name, cost, rarity, taskList) {
    }
}
