using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEffectSystem : MonoBehaviour
{
    #region Singleton

    public static SceneEffectSystem Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField] float bloodDuration = 0.1f;
    [SerializeField] float bloodFadingSpeed = 2f;
    [SerializeField] float bloodStartAlpha = 1;

    Image fadingImage;
    Image bloodFrameImage;
    [SerializeField] Image bgbImage;

    Coroutine Co_FadeIn, Co_FadeOut;
    private void Start()
    {
        fadingImage = transform.GetChild(0).GetComponent<Image>();
        bloodFrameImage = transform.GetChild(1).GetComponent<Image>();

        Initailze();
    }

    void Initailze()
    {
        fadingImage.gameObject.SetActive(true);
        bloodFrameImage.gameObject.SetActive(true);
        bgbImage.gameObject.SetActive(false);

        fadingImage.color = new Color(0, 0, 0, 0);
        bloodFrameImage.color = new Color(1, 1, 1, 0);
    }

    public void FadeIn(float _speed = 1) => Co_FadeIn = StartCoroutine(FadeInCoroutine(_speed));
    public IEnumerator FadeInCoroutine(float fadeRate = 0f, float fadeSpeed = 1)
    {
        //print("FadeIn");
        if (Co_FadeIn != null) StopCoroutine(Co_FadeIn);
        if (Co_FadeOut != null) StopCoroutine(Co_FadeOut);

        float alpha = fadingImage.color.a;

        Color fadingColor = fadingImage.color;
        fadingColor.a = alpha;

        while (alpha > fadeRate)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            fadingColor.a = alpha;
            fadingImage.color = fadingColor;
            yield return null;
        }
    }

    public void FadeOut(float _speed = 1) => Co_FadeOut = StartCoroutine(FadeOutCoroutine(_speed));
    public IEnumerator FadeOutCoroutine(float fadeRate = 1f, float fadeSpeed = 1)
    {
        //print("FadeOut");
        if (Co_FadeIn != null) StopCoroutine(Co_FadeIn);
        if (Co_FadeOut != null) StopCoroutine(Co_FadeOut);

        float alpha = fadingImage.color.a;

        Color fadingColor = fadingImage.color;
        fadingColor.a = alpha;

        while (alpha < fadeRate)
        {
            alpha += fadeSpeed * Time.deltaTime;
            fadingColor.a = alpha;
            fadingImage.color = fadingColor;
            yield return null;
        }
    }

    public void BloodFrame() => StartCoroutine(BloodFrameCoroutine());
    public IEnumerator BloodFrameCoroutine()
    {
        float alpha = bloodStartAlpha;

        bloodFrameImage.color = new Color(1, 1, 1, bloodStartAlpha);

        Color fadingColor = bloodFrameImage.color;
        fadingColor.a = alpha;

        yield return new WaitForSeconds(bloodDuration);

        while (alpha > 0f)
        {
            alpha -= bloodFadingSpeed * Time.deltaTime;
            fadingColor.a = alpha;
            bloodFrameImage.color = fadingColor;
            yield return null;
        }
    }

    public void OnBGB()
    {
        bgbImage.gameObject.SetActive(true);
    }

    public void OffBGB()
    {
        bgbImage.gameObject.SetActive(false);
    }

    public bool GetBGBActivation() => bgbImage.gameObject.activeInHierarchy;
}
