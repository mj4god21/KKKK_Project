using UnityEngine;
using UnityEngine.SceneManagement;

public class TItleManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(1);
        PlayerScript.Instance.gameObject.SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
