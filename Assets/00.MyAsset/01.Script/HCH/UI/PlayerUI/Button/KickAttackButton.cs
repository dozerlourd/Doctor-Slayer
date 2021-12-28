using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KickAttackButton : MonoBehaviour, IButton, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Reaction(true);
    }

    public void Reaction(bool _value)
    {
        InputManager.Instance.SetInputValue(out InputManager.Instance.inputButton.kickAttackBtn, _value);
    }

    public void Reaction() { }
}
