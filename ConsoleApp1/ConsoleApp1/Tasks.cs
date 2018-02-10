using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public interface ITask {
        void Run(GameState state, Card cardPlayed);
    }

    public class ExampleTask : ITask {
        public void Run(GameState state, Card cardPlayed) {
            state.TestEffect();
        }
    }
}