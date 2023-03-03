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
        FillSprite.transform.localScale = new Vector3(scale, 1, 0);
    }
}
