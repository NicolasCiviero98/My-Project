using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInputController : MonoBehaviour
{
    public static ActionInputController Instance;

    private bool _panelPressed;
    void Start() {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    void Update() {
        
    }

    public void OnActionPanelPointerDown(){
        _panelPressed = true;
    }
    public void OnActionPanelPointerUp(){
        _panelPressed = false;
    }

    public bool ActionInputActive() {
        if (_panelPressed) return true;
        if (Input.GetKey(KeyCode.Space)) return true;
        return false;
    }
}
