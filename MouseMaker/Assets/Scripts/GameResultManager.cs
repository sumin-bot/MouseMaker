using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameResultManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Text gameResultText;
    DataManager dataManager;

    [SerializeField]
    private int score;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    private void Start()
    {
        score = dataManager.apple * 1000 + dataManager.health * 100;
        gameResultText.text = $"Apple = {dataManager.apple} X 1000\nUseBlock = 0 X 100\nHealth = {dataManager.health} X 100\nGoal = 0";
        scoreText.text = $"Score : {score}";
    }

    public void GameTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
