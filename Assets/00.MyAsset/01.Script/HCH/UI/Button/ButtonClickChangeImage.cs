using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickChangeImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite pointerInSprite;
    [SerializeField] Sprite clickSprite;

    public void OnPointerDown(PointerEventData eventData)
    {
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
