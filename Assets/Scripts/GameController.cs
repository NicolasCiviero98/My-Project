using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static MatchStatistics Statistics;
    [SerializeField] private GameOverUI GameOverUI;
    [SerializeField] private TextMeshProUGUI timeText;


    void Start() {
        Time.timeScale = 1;
        Statistics = new MatchStatistics();
    }

    void Update() {
        Statistics.SurvivalTime += Time.deltaTime;
        timeText.text = Statistics.GetSurvivedTimeText();
    }

    public void GameOver() {
        GameOverUI.Activate();
    }

}
