using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public GameObject Source;
    public int pierces = 0;
    public int damage;
    public bool Critical;
    
    void Awake()
    {
        Destroy(gameObject, 1);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == Source) return;
        if (collider.GetComponent<Health>() != null)
        {
            collider.GetComponent<Health>().Damage(damage);
            ShowDamagePopup(collider);
            if (pierces <= 0) Destroy(gameObject);
            pierces--;
        }
    }

    
    public void ShowDamagePopup(Collider2D collider)
    {
        var prefab = (GameObject)Resources.Load("DamagePopup", typeof(GameObject));
        prefab.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();

        if(Critical){
            var scale = new Vector3(1.5f, 1.5f, 1.5f);
            prefab.transform.GetChild(0).localScale = scale;
            prefab.transform.GetChild(0).GetComponent<TextMesh>().color = Color.yellow;
        }
        else{
            var scale = new Vector3(1f, 1f, 1f);
            prefab.transform.GetChild(0).localScale = scale;
            prefab.transform.GetChild(0).GetComponent<TextMesh>().color = Color.white;
        }

        var position = collider.transform.position;
        position.y += GetComponent<BoxCollider2D>().size.y;

        Instantiate(prefab, position, Quaternion.identity);
    }
}
