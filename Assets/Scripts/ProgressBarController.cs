using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ProgressBarController : MonoBehaviour
{
    private const float MULTIPLE_TIME_CHANGE_PROGRESS = .5F;
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private RectTransform progress;
    [SerializeField] private float maxOffset;
    [SerializeField] private float minOffset;
    private Coroutine progressAnimation;

    private void Start()
    {
        panel.alpha = 1;
        progress.SetRight(minOffset + maxOffset);
    }

    public void HidePanel()
    {
        panel.DOFade(0, .5f).OnComplete(() => gameObject.SetActive(false));
    }

    public void SetNewValue(float newValue)
    {
        if (progressAnimation != null)
        {
            StopCoroutine(progressAnimation);
        }
        progressAnimation = StartCoroutine(SetNewOffset(1 - newValue));
    }

    private IEnumerator SetNewOffset(float newValue)
    {
        var startOffset = Mathf.Abs(progress.offsetMax.x);
        var startOffset01 = startOffset / (maxOffset + minOffset);
        var maxTimer = Mathf.Abs(newValue - startOffset01) * MULTIPLE_TIME_CHANGE_PROGRESS;
        var timer = 0f;
        while (timer < maxTimer)
        {
            yield return null;
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, maxTimer);
            var multiplyMaxOffset = Mathf.Lerp(startOffset01, newValue, timer / maxTimer);
            var newOffset = maxOffset * multiplyMaxOffset;
            progress.SetRight(minOffset + newOffset);
        }
        progress.SetRight(minOffset + maxOffset * newValue);
        progressAnimation = null;
    }
}