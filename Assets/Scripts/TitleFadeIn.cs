using UnityEngine;

public class TitleFadeIn : MonoBehaviour
{
    public Animator titleAnimator;

    void Start()
    {
        // Trigger fade after a delay
        Invoke(nameof(FadeInTitle), 2f);
    }

    void FadeInTitle()
    {
        titleAnimator.SetTrigger("FadeIn");
    }
}
