using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private Text text;
    public static PopupWindow Instance;
    private Graphic[] _graphics;
    private Image[] _images;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _graphics = gameObject.GetComponentsInChildren<Graphic>();
        Fade(0, 0, 1f);
        Instance = this;
    }

    public void ShowMessage(string message, float duration = 1f)
    {
        text.text = message;
        Fade(0, 1, duration);
        StartCoroutine(ExecuteAfterTime(duration, CloseWindow));
    }

    public void CloseWindow()
    {
        Fade(1, 0, 3f);
    }

    private void Fade(float startOpacity, float endOpacity, float duration)
    {
        foreach (var graphic in _graphics)
        {
            var startColor = graphic.color;
            startColor.a = startOpacity;
            graphic.color = startColor;

            var endColor = graphic.color;
            endColor.a = endOpacity;
            graphic.DOColor(endColor, duration);
        }
    }

    private IEnumerator ExecuteAfterTime(float time, Action task)
    {
        yield return new WaitForSeconds(time);
        task.Invoke();
    }
}
