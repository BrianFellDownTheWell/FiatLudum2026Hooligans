using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Canvas transitionCanvas;
    [SerializeField] private RectTransform topBar;
    [SerializeField] private RectTransform bottomBar;
    [SerializeField] private float closeSpeed = 0.3f;
    [SerializeField] private float openSpeed = 0.3f;

    private bool isTransitioning;

    public void LoadScene()
    {
        if (!isTransitioning)
            StartCoroutine(BlinkTransition());
    }

    private IEnumerator BlinkTransition()
    {
        isTransitioning = true;

        // persist across scene load
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transitionCanvas.gameObject);

        float screenHalf = transitionCanvas.GetComponent<RectTransform>().rect.height / 2f;

        // CLOSE EYES
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
        topBar.anchoredPosition = Vector2.zero;
        bottomBar.anchoredPosition = Vector2.zero;

        // load the new scene while eyes are closed
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

        // wait a frame
        yield return null;

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

        // Clean up persisted objects
        Destroy(transitionCanvas.gameObject);
        Destroy(gameObject);
    }
}