using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    public int OptionsCount;
    public List<UpgradeOption> options;
    public List<Skill> upgrades;
    public int selectedIndex = -1;


    void Start() {
        for (int i = 0; i < options.Count; i++) {
            var index = i;
            options[i].Unselect();
            options[i].OnSelected.AddListener(delegate{OptionPressedCallback(index);});
        }
    }

    public void Open() {
        Time.timeScale = 0;
        RerollUpgrades();
        this.gameObject.SetActive(true);
    }

    public void Close() {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void Select() {
        if (selectedIndex == -1) return;

        upgrades[selectedIndex].LevelUp();
        Close();
    }

    void OptionPressedCallback(int index) {
        for (int i = 0; i < options.Count; i++) {
            if (i != index) options[i].Unselect();
        }
        selectedIndex = index;
    }

    private void RerollUpgrades() {
        selectedIndex = -1;
        upgrades = new List<Skill>();
        //for (int i = 0; i < OptionsCount; i++) {
        //    upgrades.Add(SkillBook.Instance.Skills[0]);
        //}
        upgrades.Add(SkillBook.Instance.Skills[0]);
        upgrades.Add(SkillBook.Instance.Skills[1]);
        upgrades.Add(SkillBook.Instance.Skills[2]);
        for (int i = 0; i < upgrades.Count; i++) {
            options[i].Load(upgrades[i]);
        }


    }
}
