using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    private bool fadeIsRunning;
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private string sceneName;
    public float setTime;
    [SerializeField]
    private AudioClip StartSE;
    [SerializeField]
    private AudioSource BGMAudioSource;
    [SerializeField]
    private AudioSource SEAudioSource;
    [SerializeField]
    private Text textLight;


    #region
    IEnumerator Start()
    {
        Init();
        StartCoroutine(FlashLoop());
        Coroutine coroutine = StartCoroutine(TouchWait());
        yield return coroutine;
        yield return new WaitForSeconds(setTime);
        coroutine = StartCoroutine(FadeIn(1));
        yield return coroutine;
        SceneManager.LoadSceneAsync(sceneName);
    }
    #endregion

    private void Init()
    {
        BGMAudioSource.Play();
        fadeIsRunning = false;
    }
    private IEnumerator TouchWait()
    {
        bool flag = false;
        while (!flag)
        {
            if (Input.anyKeyDown)
            {
                flag = true;
                SEAudioSource.PlayOneShot(StartSE);
            }

            yield return null;
        }
    }
    private IEnumerator FlashLoop()
    {
        while (true)
        {
            Coroutine coroutine = StartCoroutine(FadeInText(0.5f));
            yield return coroutine;
            coroutine = StartCoroutine(FadeOutText(0.5f));
            yield return coroutine;
        }
    }
    public IEnumerator FadeInText(float fadeLeap)
    {
        if (fadeIsRunning) yield break;
        fadeIsRunning = true;
        float elapsedTime = 0;
        while (textLight.color.a < 1)
        {
            elapsedTime += Time.deltaTime;
            Color tmpColor = textLight.color;
            tmpColor.a = Mathf.Lerp(tmpColor.a, 1, elapsedTime * fadeLeap);
            textLight.color = tmpColor;
            yield return null;
            if (textLight.color.a >= 0.99f)
            {
                tmpColor.a = 1;
                textLight.color = tmpColor;
                break;
            }
        }
        fadeIsRunning = false;
    }
    public IEnumerator FadeOutText(float fadeLeap)
    {
        if (fadeIsRunning) yield break;
        fadeIsRunning = true;
        float elapsedTime = 0;
        while (textLight.color.a > 0)
        {
            elapsedTime += Time.deltaTime;
            var tmpColor = textLight.color;
            tmpColor.a = Mathf.Lerp(textLight.color.a, 0, elapsedTime * fadeLeap);
            textLight.color = tmpColor;
            yield return null;
            if (textLight.color.a <= 0.01f)
            {
                tmpColor.a = 0;
                textLight.color = tmpColor;
                break;
            }
        }
        fadeIsRunning = false;
    }
    public IEnumerator FadeIn(float fadeLeap)
    {
        if (fadeIsRunning) yield break;
        fadeIsRunning = true;
        float elapsedTime = 0;
        while (fadeImage.color.a < 1)
        {
            elapsedTime += Time.deltaTime;
            Color tmpColor = fadeImage.color;
            tmpColor.a = Mathf.Lerp(tmpColor.a, 1, elapsedTime * fadeLeap);
            fadeImage.color = tmpColor;
            yield return null;
            if (fadeImage.color.a >= 0.99f)
            {
                tmpColor.a = 1;
                fadeImage.color = tmpColor;
                break;
            }
        }
        fadeIsRunning = false;
    }
    public IEnumerator FadeOut(float fadeLeap)
    {
        if (fadeIsRunning) yield break;
        fadeIsRunning = true;
        float elapsedTime = 0;
        while (fadeImage.color.a > 0)
        {
            elapsedTime += Time.deltaTime;
            var tmpColor = fadeImage.color;
            tmpColor.a = Mathf.Lerp(fadeImage.color.a, 0, elapsedTime * fadeLeap);
            fadeImage.color = tmpColor;
            yield return null;
            if (fadeImage.color.a <= 0.01f)
            {
                tmpColor.a = 0;
                fadeImage.color = tmpColor;
                break;
            }
        }
        fadeIsRunning = false;
    }
}
