using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public abstract class Card {
        private string name;
        private int cost;
        private Program.Rarity rarity;
        private List<ITask> tasks;

        public Card(string name, int cost, Program.Rarity rarity, List<ITask> tasks) {
            this.name = name;
            this.cost = cost;
            this.rarity = rarity;
            this.tasks = tasks;
        }

        public int GetCost() {
            return cost;
        }

        public Program.Rarity GetRarity() {
            return rarity;
        }

        public void Run(GameState state) {
            foreach (ITask task in tasks) {
                task.Run(state, this);
            }
        }

        override
        public string ToString() {
            return name;
        }
    }

    public class TestCard : Card {
        private static string name = "Attack";
        private static int cost = 1;
        private static Program.Rarity rarity = Program.Rarity.Common;
        private static List<ITask> taskList = new List<ITask> {
            new ExampleTask()
        };

        public TestCard() : base(name, cost, rarity, taskList) {
        }
    }
}