using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValueText;
    [SerializeField] private TextMeshProUGUI timeValueText;

    void Start() {
    }

    private void OnDestroy() {
    }
    public void RestartGame() {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    public void Activate() {
        this.gameObject.SetActive(true);
        scoreValueText.text = GameController.Statistics.GetScore().ToString();
        timeValueText.text = GameController.Statistics.GetSurvivedTimeText();
    }
}