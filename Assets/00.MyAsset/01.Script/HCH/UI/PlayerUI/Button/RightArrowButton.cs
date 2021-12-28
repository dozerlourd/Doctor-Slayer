using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightArrowButton : MonoBehaviour, IButton, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        Reaction(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Reaction(false);
    }

    public void Reaction(bool _value)
    {
        InputManager.Instance.SetInputValue(out InputManager.Instance.inputButton.rightArrowBtn, _value);
    }

    public void Reaction() { }
}
