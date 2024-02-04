using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Skill : MonoBehaviour
{
    public int Level;
    public int MaxLevel { get; protected set; }
    public SkillData Info;
    public virtual void LevelUp() {
        if (Level < MaxLevel) Level++;
    }

    public virtual bool IsAvailable(){
        return Level < MaxLevel;
    }
}
