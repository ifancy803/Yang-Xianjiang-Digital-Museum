using System.Collections;
using UnityEngine;

public class TimedVisibilityWithFade : MonoBehaviour
{
    [Header("目标物体")]
    [Tooltip("需要控制显示/隐藏的GameObject")]
    public GameObject targetObject;

    [Header("时间设置")]
    [Tooltip("游戏开始后多久显示（秒）")]
    public float delayBeforeShow = 5f;

    [Tooltip("显示后停留多久再隐藏（秒）")]
    public float displayDuration = 5f;

    [Header("动效设置")]
    [Tooltip("淡入淡出动画时长（秒）")]
    public float fadeDuration = 0.5f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("TimedVisibilityWithFade: 未指定目标物体！");
            return;
        }

        // 获取或添加 CanvasGroup 组件（用于控制透明度）
        canvasGroup = targetObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = targetObject.AddComponent<CanvasGroup>();

        // 初始状态：完全透明且不可交互（隐藏）
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // 启动协程
        StartCoroutine(VisibilitySequence());
    }

    IEnumerator VisibilitySequence()
    {
        // 等待第一次延迟
        yield return new WaitForSeconds(delayBeforeShow);

        // 淡入
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // 停留显示
        yield return new WaitForSeconds(displayDuration);

        // 淡出
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}