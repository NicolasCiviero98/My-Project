using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

 [CreateAssetMenu(fileName ="data",menuName ="SkillData",order=1)]
public class SkillData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
}
