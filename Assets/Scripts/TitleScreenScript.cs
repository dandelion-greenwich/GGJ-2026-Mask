using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    public void OpenGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
