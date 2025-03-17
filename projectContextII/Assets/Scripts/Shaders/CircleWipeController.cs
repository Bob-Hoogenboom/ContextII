using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleWipeController : MonoBehaviour
{
    private Animator _anime;
    private Image _image;
    private readonly int _circleDiaHash = Shader.PropertyToID("_CircleDiameter");
    private readonly int _fadeInHash = Animator.StringToHash("circleIn");
    private readonly int _fadeOutHash = Animator.StringToHash("circleOut");

    // Delegate for transition completion
    public Action OnFadeOutComplete;
    public float transitionTime = 1.5f; //TODO, maker time last as long as the animation clip instead of hard value
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
        _anime.SetTrigger(_fadeInHash);
    }
    public void CircleFadeOut(Action onComplete) 
    {
        StartCoroutine(FadeOutComplete(onComplete));
    }

     private IEnumerator FadeOutComplete(Action onComplete)
    {
        _anime.SetTrigger(_fadeOutHash);

        yield return new WaitForSeconds(transitionTime);

        onComplete?.Invoke();
    }
}
