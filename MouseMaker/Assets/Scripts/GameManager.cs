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
        appleCount += amount;
        appleText.text = $"Apple X {appleCount}";
    }

    public void ChangeHealthUI()
    {
        healthText.text = $"HP : {player.health}";
    }

    private void Update()
    {
        if (player.health == 0)
        {
            SceneManager.LoadScene("GameResultScene");
        }
    }


}
