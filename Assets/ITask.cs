using System;
using System.Collections.Generic;
using UnityEngine;

    public interface ITask {
        void Run(GameState state, Card cardPlayed);
    }

    public class ExampleTask : ITask {
        public void Run(GameState state, Card cardPlayed) {
            state.TestEffect();
        }
    }