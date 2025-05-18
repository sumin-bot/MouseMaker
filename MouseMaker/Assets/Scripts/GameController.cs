using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int health;

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
        healthText.text = $"HP : {health}";
    }
}
