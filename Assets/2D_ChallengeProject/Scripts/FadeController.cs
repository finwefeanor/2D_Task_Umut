using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public Camera mainCamera;
    public Camera castleCamera;
    public Canvas fadeCanvas; // Reference to the Fade Out Canvas

    void Start()
    {
        // Ensure the image is fully transparent at start
        SetFadeImageAlpha(0f);
        //StartCoroutine(FadeOut());
        castleCamera.gameObject.SetActive(false);
        fadeCanvas.gameObject.SetActive(false);
    }

    public void FadeInAndSwitchCamera(bool toCastle)
    {
        StartCoroutine(FadeInAndSwitch(toCastle));
    }

    IEnumerator FadeInAndSwitch(bool toCastle)
    {
        // Fade in to black
        yield return StartCoroutine(FadeToAlpha(1.0f));

        if (toCastle)
        {
            // Switch to castle camera
            mainCamera.gameObject.SetActive(false);
            castleCamera.gameObject.SetActive(true);
            fadeCanvas.gameObject.SetActive(true);
            mainCamera.GetComponent<AudioListener>().enabled = false;
            castleCamera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            // Switch back to main camera
            castleCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            fadeCanvas.gameObject.SetActive(false);
            castleCamera.GetComponent<AudioListener>().enabled = false;
            mainCamera.GetComponent<AudioListener>().enabled = true;
        }

        // Fade out from black
        yield return StartCoroutine(FadeToAlpha(0.0f));
    }

    IEnumerator FadeToAlpha(float targetAlpha)
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;
        float startAlpha = color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Ensure the alpha is set to the target value at the end
        color.a = targetAlpha;
        fadeImage.color = color;
    }

    void SetFadeImageAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }

    //IEnumerator FadeOut()
    //{
    //    float elapsedTime = 0.0f;
    //    Color color = fadeImage.color;

    //    while (elapsedTime < fadeDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeDuration);
    //        fadeImage.color = color;
    //        yield return null;
    //    }
    //}
}