using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    //SpriteRenderer spriteRenderer;
    //private IEnumerator coroutine;
    private Color spriteRendererColor;
    private GameObject image;


    void Awake()
    {
        spriteRendererColor = GetComponent<Image>().color;
    }

    public IEnumerator FadeOut(float time)
    {


        while (spriteRendererColor.a < 1)
        {
            //print(spriteRendererColor.a);
            spriteRendererColor = gameObject.GetComponent<Image>().color;
            spriteRendererColor.a += Time.unscaledDeltaTime / time;
            gameObject.GetComponent<Image>().color = spriteRendererColor;
            yield return null;
        }
    }



    public IEnumerator FadeIn(float time)
    {
        while (spriteRendererColor.a > 0)
        {
            spriteRendererColor = gameObject.GetComponent<Image>().color;
            spriteRendererColor.a -= Time.unscaledDeltaTime / time;
            gameObject.GetComponent<Image>().color = spriteRendererColor;
            yield return null;
        }
    }

    public IEnumerator TransitionToScene(int sceneIndex, float time)
    {
        yield return FadeOut(time);
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        yield return FadeIn(time);
    }

    public void StartTransition(int sceneIndex, float time)
    {
        StartCoroutine(TransitionToScene(sceneIndex, time));
    }

}

