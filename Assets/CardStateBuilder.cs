using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardStateBuilder {
    
    CardState baseCard;

    List<TaskBuilder> taskBuilders;

    List<float> mean;
    List<float> stdDev;


    public CardStateBuilder(CardState baseCard, List<TaskBuilder> taskBuilders){
        this.baseCard = baseCard;
        this.taskBuilders = taskBuilders;
    }

    public virtual CardState GetCardState(int level) {
        CardState c = baseCard.Clone();
        List<Task> taskList = new List<Task>();
        for(int i = 0; i < taskBuilders.Count; i++){
            taskList.Add(taskBuilders[i].GetTask(level));
        }
        c.SetTasks(taskList);

        return c;
    }
}

public class EnemyCardStateBuilder : CardStateBuilder {
    public EnemyCardStateBuilder(int level) : base(null, null){

    }
}