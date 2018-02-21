using System;
using System.Collections.Generic;
using UnityEngine;
    
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

    private static int msgNumber = 0;

    public static void UnityOutput(string tag, string msg){
		Debug.Log(tag + ": " + msg);
		msgNumber++;
	}

	public static void UnityOutput(string msg){
		UnityOutput(String.Format("{0}", msgNumber), msg);
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

        public void setCurrentEntity(IEntity i){
            currentEntity = i;
        }

        public void Heal(IEntity e, int amount) {
            e.SetHP(e.GetHP() + amount);
        }

        public void Damage(IEntity e, int amount) {
            Heal(e, -amount);
        }

        public void TestEffect() {
            UnityOutput("You attack!  It's not very effective.");
        }

        public IEntity NextTurn() {
            if (currentEntity == null) {
                currentEntity = entities[0];
            } else {
                int turnIndex = entities.IndexOf(currentEntity) + 1;
                if (turnIndex >= entities.Count) {
                    turnIndex = 0;
                }
                currentEntity = entities[turnIndex];
            }
            return currentEntity;
        }
    }