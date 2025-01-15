using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInteractable : MonoBehaviour
{

    private bool hasChange;
    
    private void Start() {
        hasChange = false;
    }
    
    private void Update()
    {
        if (CursedPriestStoryTeller.getHasStoryBeenTold() && !hasChange)
        {
            gameObject.layer = LayerMask.NameToLayer("InteractableElements");
            hasChange = true;
        }
    }
    
    public void OnPointerEnter() {

    }

    public void OnPointerExit() {
      
    }
}