using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Player player;

    public int appleCount = 0;
    public int blockCount = 0;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI blockText;
    public GameObject blockPrefab;
    public GameObject jumpBoostPrefab;

    private bool isBlock = false;
    private bool isJumpBoost = false;

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

    public void SelectBlock()
    {
        if (!isBlock)
        {
            isBlock = true;
            isJumpBoost = false;
            blockText.text = "Block : Block";
        }
        else
        {
            isBlock = false;
            isJumpBoost = false;
            blockText.text = "Block : None";
        }
    }

    public void SelectJumpBoost()
    {
        if (!isJumpBoost)
        {
            isJumpBoost = true;
            isBlock = false;
            blockText.text = "Block : Jump";
        }
        else
        {
            isBlock = false;
            isJumpBoost = false;
            blockText.text = "Block : None";
        }
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
        // 블럭 설치
        if (isBlock)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // UI 위에서 클릭할 경우 설치 X
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPosition.z = 0f;

                Instantiate(blockPrefab, clickPosition, Quaternion.identity);
                blockCount++;
            }
        }
        // 점프 부스트 설치
        if (isJumpBoost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // UI 위에서 클릭할 경우 설치 X
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPosition.z = 0f;

                Instantiate(jumpBoostPrefab, clickPosition, Quaternion.identity);
                blockCount++;
            }
        }

        // 플레이어가 사망할 경우
        if (player.health == 0)
        {
            DataManager.Instance.SaveDataBeforeSceneChange();

            SceneManager.LoadScene("GameResultScene");
        }
    }
}
