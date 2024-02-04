using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
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
        upgrades = new List<Skill>(SkillBook.Instance.AvailableSkills());
        Shuffle(upgrades);
        
        for (int i = 0; i < options.Count && i < upgrades.Count; i++) {
            options[i].Load(upgrades[i]);
        }
    }



    public void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        var rng = new System.Random();  

        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }

}
