using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public static SkillBook Instance;
    public List<Skill> Skills;

    void Start() {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    public List<Skill> AvailableSkills() {
        var skills = new List<Skill>(Skills);
        for (int i = 0; i < skills.Count; i++) {
            if (!skills[i].IsAvailable()) {
                skills.RemoveAt(i);
                i--;
            }
        }
        return skills;
    }
}
