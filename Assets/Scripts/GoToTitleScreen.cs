using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleScreen : MonoBehaviour
{
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
