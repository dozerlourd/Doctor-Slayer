using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelClass : MonoBehaviour, IPanel
{
    [SerializeField] protected bool isLerp = false;
    [SerializeField] float lerpSpeed = 1;

    Vector3 originScale;
    float deltaTime;
    WaitForSecondsRealtime realTime;

    private void Awake()
    {
        originScale = transform.localScale;
        deltaTime = Time.deltaTime;
    }

    private void Start() => realTime = new WaitForSecondsRealtime(deltaTime);

    public virtual void OpenPanel()
    {
        if (gameObject.activeInHierarchy) return;
        SceneEffectSystem.Instance.OnBGB();
        CoroutineManager.Instance.StartCoroutine(OnOffPanelAnimation(isLerp, true));
    }

    public virtual void ClosePanel()
    {
        if (!gameObject.activeInHierarchy) return;
        SceneEffectSystem.Instance.OffBGB();
        CoroutineManager.Instance.StartCoroutine(OnOffPanelAnimation(isLerp, false));
    }

    IEnumerator OnOffPanelAnimation(bool _isLerp, bool isOpen)
    {
        if(isOpen) gameObject.SetActive(true);

        if (_isLerp)
        {
            float lerpRate = isOpen ? 0f : 1f;

            while (isOpen ? lerpRate < 1f : lerpRate > 0f)
            {
                lerpRate += isOpen ? deltaTime * lerpSpeed : -deltaTime * lerpSpeed;
                transform.localScale = originScale * lerpRate;
                yield return realTime;
            }
        }

        if(!isOpen) gameObject.SetActive(false);
    }
}
