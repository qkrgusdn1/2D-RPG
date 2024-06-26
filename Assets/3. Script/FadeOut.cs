using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image fadeImage;
    public Text fadeText;
    float fadeAlpha = 1;

    AudioSource backGroundAudio;
    void Start()
    {
        backGroundAudio = GetComponent<AudioSource>();
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (fadeAlpha >= 0)
        {
            fadeAlpha -= 0.003f;

            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeAlpha);
            fadeText.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeAlpha);

            yield return new WaitForSeconds(0.001f);
        }
        fadeImage.gameObject.SetActive(false);
        backGroundAudio.Play();
    }
}
