using System;
using System.Collections.Generic;

namespace ConsoleApp1 {

    public class GameState {
        private int numPlayers = 0;
        List<IEntity> entities = new List<IEntity>();
        List<Player> players = new List<Player>();
        List<IEntity> enemies = new List<IEntity>();
        IEntity currentEntity;

        public void AddEntity(IEntity e) {
            entities.Add(e);
            if (e is Player) {
                players.Add((Player)e);
            } else {
                enemies.Add(e);
            }
        }

        public void BeginFight() {
            foreach (IEntity entity in entities) {
                entity.BeginFight();
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

        public IEntity GetCurrentEntity() {
            return currentEntity;
        }

        public void Heal(IEntity e, int amount) {
            e.SetHP(e.GetHP() + amount);
        }

        public void Damage(IEntity e, int amount) {
            Heal(e, -amount);
        }

        public void TestEffect() {
            Console.WriteLine("You attack!  It's not very effective.");
        }

        public void NextTurn() {
            if (currentEntity == null) {
                currentEntity = entities[0];
            } else {
                int turnIndex = entities.IndexOf(currentEntity) + 1;
                if (turnIndex >= entities.Count) {
                    turnIndex = 0;
                }
                currentEntity = entities[turnIndex];
            }
            currentEntity.TakeTurn(this);
        }
    }
}