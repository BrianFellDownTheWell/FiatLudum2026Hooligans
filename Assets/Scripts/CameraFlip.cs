using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlip : MonoBehaviour
{
    public RectTransform topBar;
    public RectTransform bottomBar;
    public Camera cameraToFlip;

    public float closeSpeed = 0.3f;

    public float openSpeed = 0.3f;

    private bool isTransitioning;

    public void Flip()
    {
        if (!isTransitioning)
            StartCoroutine(BlinkFlip());
    }

    private IEnumerator BlinkFlip()
    {
        // CLOSE EYES
        isTransitioning = true;

        Canvas canvas = topBar.GetComponentInParent<Canvas>();
        float screenHalf = canvas.GetComponent<RectTransform>().rect.height / 2f;

        float elapsed = 0f;
        while (elapsed < closeSpeed)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / closeSpeed);
            float smooth = Mathf.SmoothStep(0f, 1f, t);

            topBar.anchoredPosition = new Vector2(0f, Mathf.Lerp(screenHalf, 0f, smooth));
            bottomBar.anchoredPosition = new Vector2(0f, Mathf.Lerp(-screenHalf, 0f, smooth));
            yield return null;
        }
        topBar.anchoredPosition = new Vector2(0f, 0f);
        bottomBar.anchoredPosition = new Vector2(0f, 0f);

        // flip camera while eyes are closed
        if (cameraToFlip != null)
            cameraToFlip.transform.Rotate(0f, 180f, 0f);

        yield return new WaitForSecondsRealtime(0.05f);

        // OPEN EYES

        elapsed = 0f;
        while (elapsed < openSpeed)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / openSpeed);
            float smooth = Mathf.SmoothStep(0f, 1f, t);

            topBar.anchoredPosition = new Vector2(0f, Mathf.Lerp(0f, screenHalf, smooth));
            bottomBar.anchoredPosition = new Vector2(0f, Mathf.Lerp(0f, -screenHalf, smooth));
            yield return null;
        }
        topBar.anchoredPosition = new Vector2(0f, screenHalf);
        bottomBar.anchoredPosition = new Vector2(0f, -screenHalf);

        isTransitioning = false;
    }
}
