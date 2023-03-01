using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UpgradeOption : MonoBehaviour
{
    public bool Selected;
    public Skill Skill;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private Image Border;
    [SerializeField] private Image Frame;
    [SerializeField] private Image Icon;

    public UnityEvent OnSelected;

    void Start() {
        
    }

    void Update() {
        
    }

    public void OnClick() {
        Border.color = new Color32(211,91,50,255);
        OnSelected.Invoke();
    }
    public void Unselect() {
        Border.color = new Color32(20,35,58,255);
    }

    public void Load(Skill skill) {
        Skill = skill;
        Name.text = skill.Info.Name;
        Level.text = $"Level {skill.Level + 1}.";
        Icon.sprite = Skill.Info.Icon;
    }




}
