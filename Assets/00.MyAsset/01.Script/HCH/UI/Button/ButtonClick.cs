using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite pointerInSprite;
    [SerializeField] Sprite clickSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlayEnvironmentOneShot(SoundManager.Instance.ButtonClickSounds, 0.6f);
        GetComponent<Image>().sprite = clickSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = pointerInSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = defaultSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = pointerInSprite;
    }
}
