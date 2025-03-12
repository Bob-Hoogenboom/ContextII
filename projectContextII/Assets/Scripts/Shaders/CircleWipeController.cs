using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleWipeController : MonoBehaviour
{
    private Animator _anime;
    private Image _image;
    private readonly int _circleDiaHash = Shader.PropertyToID("_CircleDiameter");
    private readonly int _fadeInHash;
    private bool _isIn = false;

    // Delegate for transition completion
    public Action OnFadeOutComplete;
    public float transitionTime = 1.5f; // Set this to match your animation duration
    public float circleDiameter = 0f;

    private void Start()
    {
        _anime = GetComponent<Animator>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.materialForRendering.SetFloat(_circleDiaHash, circleDiameter);
    }


    public void CircleFadeIn() 
    {
        _anime.SetTrigger("circleIn");
        _isIn = true;
    }
    public void CircleFadeOut(Action onComplete) 
    {
        StartCoroutine(FadeOutComplete(onComplete));
    }

     private IEnumerator FadeOutComplete(Action onComplete)
    {
        _anime.SetTrigger("circleOut");

        yield return new WaitForSeconds(transitionTime);

        onComplete?.Invoke();
    }
}
