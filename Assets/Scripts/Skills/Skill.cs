using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Skill : MonoBehaviour
{
    public int Level;
    public int MaxLevel;
    public virtual void LevelUp() {
        if (Level + 1 < MaxLevel) Level++;
    }

    public virtual bool IsAvailable(){
        return Level < MaxLevel;
    }
}
