using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private SpriteSlider spriteSlider;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Slider slider;

    private int MAX_HEALTH = 100;

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.D)) { Damage(10); }
        //if (Input.GetKeyDown(KeyCode.H)) { Heal(10); }
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = currentHealth;

        if (slider != null) slider.maxValue = maxHealth;
        if (spriteSlider != null) spriteSlider.Max = maxHealth;
        
        UpdateSlider();
    }

    public void Damage(int amount)
    {
        if (amount < 0) { throw new System.ArgumentOutOfRangeException("Cannot have negative Damage"); }

        this.health -= amount;
        if (health <= 0) { Die(); }

        StartCoroutine(VisualIndicator(Color.red));
        UpdateSlider();
    }
    public void Heal(int amount)
    {
        if (amount < 0) { throw new System.ArgumentOutOfRangeException("Cannot have negative healing"); }

        this.health += amount;
        if (health  > MAX_HEALTH) { this.health = MAX_HEALTH; }

        StartCoroutine(VisualIndicator(Color.green));
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        if (slider != null) slider.value = health;
        if (spriteSlider != null) spriteSlider.ValueChanged(health);
        if (healthBar != null) healthBar.SetActive(health != MAX_HEALTH);
    }



    private IEnumerator VisualIndicator(Color color)
    {
        var defaultColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = defaultColor;
    }
    private void Die()
    {
        //Debug.Log("I am Dead!");
        GetComponent<Enemy>().OnDeath();
        Destroy(gameObject);
    }
}
