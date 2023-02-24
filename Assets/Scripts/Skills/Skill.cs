using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Skill : MonoBehaviour
{
    public int Level;
    public abstract void LevelUp();
}
