using System.Collections;
using UnityEngine;

public class LevelCompleteAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName;
    [SerializeField] private int level;
    [SerializeField] private float delayBeforeAnimation = 0f;

    private void OnEnable()
    {
        StartCoroutine(SubscribeWhenReady());
    }

    private IEnumerator SubscribeWhenReady()
    {
        while (GameManager.Instance == null)
            yield return null;

        switch (level)
        {
            case 1: GameManager.Instance.OnLevel1Complete.AddListener(PlayAnimation); break;
            case 2: GameManager.Instance.OnLevel2Complete.AddListener(PlayAnimation); break;
            case 3: GameManager.Instance.OnLevel3Complete.AddListener(PlayAnimation); break;
            case 4: GameManager.Instance.OnLevel4Complete.AddListener(PlayAnimation); break;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnLevel1Complete.RemoveListener(PlayAnimation);
        GameManager.Instance.OnLevel2Complete.RemoveListener(PlayAnimation);
        GameManager.Instance.OnLevel3Complete.RemoveListener(PlayAnimation);
        GameManager.Instance.OnLevel4Complete.RemoveListener(PlayAnimation);
    }

    private void PlayAnimation()
    {
        StartCoroutine(PlayAfterDelay());
    }

    private IEnumerator PlayAfterDelay()
    {
        if (delayBeforeAnimation > 0f)
            yield return new WaitForSeconds(delayBeforeAnimation);

        if (animator != null && !string.IsNullOrEmpty(triggerName))
            animator.SetTrigger(triggerName);
    }
}
