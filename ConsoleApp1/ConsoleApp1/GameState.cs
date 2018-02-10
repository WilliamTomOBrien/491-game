using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public class GameState {
        private int numPlayers = 0;
        List<IEntity> entities = new List<IEntity>();
        List<Player> players = new List<Player>();
        List<IEntity> enemies = new List<IEntity>();
        IEntity currentTurn;

        public void AddEntity(IEntity e) {
            entities.Add(e);
            if (e is Player) {
                players.Add((Player)e);
            } else {
                enemies.Add(e);
            }
        }

        public int NumPlayers() {
            return numPlayers;
        }

        public List<Player> GetPlayers() {
            return new List<Player>(players);
        }

        public List<IEntity> GetEntities() {
            return new List<IEntity>(entities);
        }

        public List<IEntity> GetEnemies() {
            return new List<IEntity>(enemies);
        }

        public IEntity getEntity(int i) {
            return entities[i];
        }

        public void Heal(IEntity e, int amount) {
            e.SetHP(e.GetHP() + amount);
        }

        public void Damage(IEntity e, int amount) {
            Heal(e, -amount);
        }

        public void TestEffect() {
            Console.WriteLine("Hello world!");
        }
    }
}