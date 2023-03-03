using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public GameObject Source;
    public int damage;
    public bool Critical;
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject == Source) return;
        var health = collider.GetComponent<Health>();
        if (health == null) return;
        
        health.Damage(damage);
        ShowDamagePopup(collider);
    }
    public void ShowDamagePopup(Collider2D collider) {
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
        position.y += collider.GetComponent<BoxCollider2D>().size.y;

        Instantiate(prefab, position, Quaternion.identity);
    }

}
