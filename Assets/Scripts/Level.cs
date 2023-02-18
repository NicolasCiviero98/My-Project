using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private int _level;
    private int _experience;

    private const int _baseExperienceToLevelUp = 100;
    private int _experienceToLevelUp;

    void Start() {
        UpdateUI();
        _experienceToLevelUp = _baseExperienceToLevelUp;
    }

    public void OnExperienceCollected(int exp) {
        _experience += exp;
        while (_experience >= _experienceToLevelUp) LevelUp();
        UpdateUI();
    }

    private void UpdateUI() {
        slider.value = _experience;
        slider.maxValue = _experienceToLevelUp;
        sliderText.text = $"{_level}";
    }

    private void LevelUp() {
        if (_experience < _experienceToLevelUp) return;
        
        _experience = _experience - _experienceToLevelUp;
        _level++;
        _experienceToLevelUp = (int)(_baseExperienceToLevelUp * Math.Pow(1.2, _level));
    }

}
