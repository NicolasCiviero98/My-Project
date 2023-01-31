using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceObject : MonoBehaviour
{
    [SerializeField] private int expCount = 5;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Level>() != null)
        {
            collision.collider.GetComponent<Level>().OnExperienceCollected(expCount);
            Destroy(gameObject);
        }
    }

}
