using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStatistics
{
    public int BaseScore;
    public int EnemiesKilled;
    public float DamageDealt;
    public float DamageReceived;
    public float TotalHealed;
    public float SurvivalTime;

    public void OnEnemyDeath(Enemy enemy) {
        BaseScore += enemy.power;
        EnemiesKilled++;
    }

    public int GetScore() {
        return BaseScore;
    }
    public string GetSurvivedTimeText() {
        var minutes = Math.Floor(SurvivalTime / 60).ToString().PadLeft(2, '0');
        var seconds = Math.Floor(SurvivalTime % 60).ToString().PadLeft(2, '0');
        var text = $"{minutes}:{seconds}";
        return text;
    }
}
