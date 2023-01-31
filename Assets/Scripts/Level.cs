using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private int _level;
    private int _experience;

    private const int _baseExperienceToLevelUp = 100;
    private int _experienceToLevelUp;

    void Start()
    {
        UpdateSlider();
    }

    public void OnExperienceCollected(int exp)
    {
        _experience += exp;
        while (_experience >= _experienceToLevelUp) LevelUp();
        UpdateSlider();
    }

    private void UpdateSlider()
    {
        slider.value = _experience;
        slider.maxValue = _experienceToLevelUp;
    }

    private void LevelUp()
    {
        if (_experience < _experienceToLevelUp) return;
        
        _experience = _experience - _experienceToLevelUp;
        _level++;
        _experienceToLevelUp = (int)(_baseExperienceToLevelUp * Math.Pow(1.2, _level));
    }

}
