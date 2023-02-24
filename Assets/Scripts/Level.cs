using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    public UnityEvent OnLevelUp;

    private int _level;
    private int _experience;

    private const int _baseExperienceToLevelUp = 100;
    private int _experienceToLevelUp;

    void Start() {
        _experienceToLevelUp = _baseExperienceToLevelUp;
        UpdateUI();
    }

    public void OnExperienceCollected(int exp) {
        _experience += exp * 10;
        while (_experience >= _experienceToLevelUp) LevelUp();
        UpdateUI();
    }

    private void UpdateUI() {
        slider.maxValue = _experienceToLevelUp;
        slider.value = _experience;
        sliderText.text = $"{_level}";
    }

    private void LevelUp() {
        if (_experience < _experienceToLevelUp) return;
        
        _experience = _experience - _experienceToLevelUp;
        _level++;
        _experienceToLevelUp = (int)(_baseExperienceToLevelUp * Math.Pow(1.2, _level));
        OnLevelUp?.Invoke();
    }

}
