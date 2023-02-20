using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValueText;
    private int score = 0;

    void Start() {
        Health.OnPlayerDeath += ActivateGameObject;
        Health.OnEnemyDeath += CountScore;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        Health.OnPlayerDeath -= ActivateGameObject;
        Health.OnEnemyDeath -= CountScore;
    }
    public void RestartGame() {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    private void CountScore() {
        score++;
    }

    private void ActivateGameObject() {
        Debug.Log("Activate GameOver Screen");
        this.gameObject.SetActive(true);
        scoreValueText.text = score.ToString();
    }
}