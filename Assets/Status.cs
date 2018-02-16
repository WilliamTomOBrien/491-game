using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status {
        private string name;
        private int turns
        private List<ITask> tasks;

        public Status(string name, int turns, List<ITask> tasks) {
            this.name = name;
            this.turns = turns;
            this.tasks = tasks;
        }

        public void Run(GameState state) {

            this.turns--;
            
            foreach (ITask task in tasks) {
                task.Run(state, this);
            }
            if(turns <= 0) state.GetCurrentEntity().DiscardStatus(this);

        }

        override
        public string ToString() {
            return name;
        }
    }

 