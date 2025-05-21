using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Player player;

    public int appleCount = 0;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI healthText;

    public void AddAppleCount(int amount)
    {
        // 사과 수 UI
        appleCount += amount;
        appleText.text = $"Apple X {appleCount}";
    }

    public void ChangeHealthUI()
    {
        // HP UI
        healthText.text = $"HP : {player.health}";
    }

    private void Start()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.gameManager = this;
        }
    }

    private void Update()
    { 
        // 플레이어가 사망할 경우
        if (player.health == 0)
        {
            DataManager.Instance.SaveDataBeforeSceneChange();

            SceneManager.LoadScene("GameResultScene");
        }
    }
}
