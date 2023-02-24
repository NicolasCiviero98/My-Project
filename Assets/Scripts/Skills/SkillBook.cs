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
}
