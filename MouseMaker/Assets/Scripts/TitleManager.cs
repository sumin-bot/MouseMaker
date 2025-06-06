using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleManager : MonoBehaviour
{
    public Image gameExplainImage;

    private void Awake()
    {
        gameExplainImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameExplainCancle();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void GameExplain()
    {
        gameExplainImage.gameObject.SetActive(true);
    }

    public void GameExplainCancle()
    {
        gameExplainImage.gameObject.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
