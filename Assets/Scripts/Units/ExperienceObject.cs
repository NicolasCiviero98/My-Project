using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceObject : MonoBehaviour
{
    [SerializeField] public int expCount = 5;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Level>() != null)
        {
            collider.GetComponent<Level>().OnExperienceCollected(expCount);
            Destroy(gameObject);
        }
    }

}
