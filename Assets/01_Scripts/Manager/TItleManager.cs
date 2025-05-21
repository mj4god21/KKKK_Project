using UnityEngine;
using UnityEngine.SceneManagement;

public class TItleManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("KMJ");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
