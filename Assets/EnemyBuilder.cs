using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyBuilder {

    int level;

    System.Random r;
    List<CardStateBuilder> cardBuilders;
    List<string> spritePaths;
    string spritePath;

    List<float> mean;
    List<float> stdDev;

    public EnemyBuilder(int level, string spritePath, List<CardStateBuilder> cardBuilders){
        this.spritePaths = new List<string>();
        spritePaths.Add("Sprites/enemies/bee_256px");
        spritePaths.Add("Sprites/enemies/brain_256px");
        spritePaths.Add("Sprites/enemies/derp_256px");
        spritePaths.Add("Sprites/enemies/gremlin_256px");
        spritePaths.Add("Sprites/enemies/ice_slime_256px");
        spritePaths.Add("Sprites/enemies/pizza_256px");
        spritePaths.Add("Sprites/enemies/slimy_256px");
        spritePaths.Add("Sprites/enemies/small_pink_slime_256px");
        spritePaths.Add("Sprites/enemies/snowman_256px");
        spritePaths.Add("Sprites/enemies/staff_256px");
        spritePaths.Add("Sprites/enemies/witch_hat_256px");
        spritePaths.Add("Sprites/enemies/flame_256px");
        spritePaths.Add("Sprites/enemies/king_256px");
        spritePaths.Add("Sprites/enemies/pawn_256px");
        spritePaths.Add("Sprites/enemies/rook_256px");
        spritePaths.Add("Sprites/enemies/tentacle_reaper_submission");






        r = new System.Random();

        this.level = level;
        this.cardBuilders = cardBuilders;
        this.spritePath = spritePath;
    }

    public void SetEnemy(Enemy e, int level){
        List<CardState> enemyCards = new List<CardState>();
        for(int i = 0; i < cardBuilders.Count; i++){
            enemyCards.Add(cardBuilders[i].GetCardState(level));
        }

        e.SetCards(enemyCards);
        SetSprite(e);
    }

    public void SetSprite(Enemy e) {
        if(spritePath != null) {
            e.SetSprite(spritePath);
        } else {
            e.SetSprite(spritePaths[r.Next(spritePaths.Count)]);
        }
    }




}
