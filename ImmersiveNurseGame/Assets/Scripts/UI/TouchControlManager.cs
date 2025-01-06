using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TouchControlManager : MonoBehaviour
{
    public UIDocument touchUI;

    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            ToggleTouchUI(false);
        }
        if(Input.GetKeyDown(KeyCode.N)){
            ToggleTouchUI(true);
        }
    }

    public void ToggleTouchUI(bool isActive){
        touchUI.rootVisualElement.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
