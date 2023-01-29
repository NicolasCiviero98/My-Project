using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSlider : MonoBehaviour
{
    public float Value;
    public float Min;
    public float Max;
    [SerializeField] private GameObject FillSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChanged(float value)
    {
        Value = value;
        var scale = (Value - Min) / (Max - Min);
        Debug.Log($"{scale}");
        FillSprite.transform.localScale = new Vector3(scale, 1, 0);

        //FillSprite.transform.localPosition = new Vector3(-(1 - scale) * GetComponent<SpriteRenderer>().bounds.size.x / 2, 0, 0);
    }
}
