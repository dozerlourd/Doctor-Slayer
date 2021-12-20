using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAutoScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 11;
    [SerializeField] Transform[] initVectors;

    void Start()
    {
        StartCoroutine(Co_ScrollBackground());
    }

    IEnumerator Co_ScrollBackground()
    {
        float scrollScale = scrollSpeed / 22;

        while (true)
        {
            transform.position -= new Vector3(scrollScale, 0, 0) * Time.fixedDeltaTime;
            if (initVectors[0].position.x > transform.position.x)
            {
                transform.position = initVectors[1].position;
            }
            yield return Time.fixedDeltaTime;
        }
    }
}
