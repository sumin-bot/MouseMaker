using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameResultManager : MonoBehaviour
{
    public Text bestscoreText;
    public TextMeshProUGUI scoreText;
    public Text gameResultText;
    DataManager dataManager;

    public float bestscore;
    public float score;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    private void Start()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.gameResultManager = this;
        }

        bestscore = dataManager.bestscore;
        score = Mathf.Round(dataManager.time) + dataManager.apple * 1000 - dataManager.block * 100 + dataManager.health * 100 + (dataManager.isGoal ? 10000 : 0);
        gameResultText.text = $"Time = {Mathf.Round(dataManager.time)} X 1\nApple = {dataManager.apple} X 1000\nUseBlock = -{dataManager.block} X 100\nHealth = {dataManager.health} X 100\nGoal = {(dataManager.isGoal ? 10000 : 0)}";
        scoreText.text = $"Score : {score}";

        // 최고 점수 계산
        if (score > bestscore)
        {
            bestscore = score;
            bestscoreText.text = $"Best Score : {bestscore}";
        }
        else
        {
            bestscoreText.text = $"Best Score : {bestscore}";
        }
    }

    public void GameTitle()
    {
        DataManager.Instance.SaveScoreBeforeSceneChange();
        SceneManager.LoadScene("TitleScene");
    }

    public void GameRestart()
    {
        DataManager.Instance.SaveScoreBeforeSceneChange();
        SceneManager.LoadScene("MainGameScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
