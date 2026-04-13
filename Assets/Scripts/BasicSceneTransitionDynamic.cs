using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSceneTransitionDynamic : MonoBehaviour
{
    [SerializeField] private Canvas transitionCanvas;
    [SerializeField] private RectTransform topBar;
    [SerializeField] private RectTransform bottomBar;
    [SerializeField] private float closeSpeed = 0.3f;
    [SerializeField] private float openSpeed = 0.3f;
    [SerializeField] private string sceneName;

    private bool isTransitioning;

    public void LoadScene()
    {
        if (!isTransitioning)
            StartCoroutine(BlinkTransition(sceneName));
    }

    public void LoadScene(string sceneNameOverride)
    {
        if (!isTransitioning)
            StartCoroutine(BlinkTransition(sceneNameOverride));
    }

    private IEnumerator BlinkTransition(string sceneName)
    {
        isTransitioning = true;

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

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;

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

        Destroy(transitionCanvas.gameObject);
        Destroy(gameObject);
    }
}
