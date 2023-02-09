using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour
{
    public float delay = 0f;
 
     void Start () {
        foreach (Transform child in transform){
            var anim = child.GetComponent<Animator>();
            if (anim == null) continue;
            if (anim.GetCurrentAnimatorStateInfo(0).length > delay) delay = anim.GetCurrentAnimatorStateInfo(0).length;
        }
        Debug.Log($"Delay: {delay}");
         Destroy (gameObject, delay); 
     }
}
