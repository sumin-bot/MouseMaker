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
    public float limitTime = 300.0f;
    public bool isGoal = false;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI blockText;
    public TextMeshProUGUI TimeText;
    public GameObject blockPrefab;
    public GameObject jumpBoostPrefab;
    public GameObject settingUI;

    private bool isBlock = false;
    private bool isJumpBoost = false;
    private bool isPause = false;

    public void AddAppleCount(int amount)
    {
        // ��� �� UI
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
    private void Awake()
    {
        settingUI.SetActive(false);
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
        // ���� �ð�
        limitTime -= Time.deltaTime;
        TimeText.text = $"{System.Math.Truncate(limitTime / 60)} : {Mathf.Round(limitTime % 60)}";

        if (limitTime <= 0)
        {
            player.health = 0;
            DataManager.Instance.SaveDataBeforeSceneChange();

            SceneManager.LoadScene("GameResultScene");
        }

        // �� ��ġ
        if (isBlock)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // UI ������ Ŭ���� ��� ��ġ X
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
        // ���� �ν�Ʈ ��ġ
        if (isJumpBoost)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // UI ������ Ŭ���� ��� ��ġ X
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

        // �Ͻ������� ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                settingUI.SetActive(false);
                Time.timeScale = 1;
                isPause = false;
            }
            else
            {
                settingUI.SetActive(true);
                Time.timeScale = 0;
                isPause = true;
            }
        }

        // �÷��̾ ����� ���
        if (player.health <= 0)
        {
            player.health = 0;
            limitTime = 0;
            DataManager.Instance.SaveDataBeforeSceneChange();

            SceneManager.LoadScene("GameResultScene");
        }
    }

    // �÷��̾ ���������� ������ ���
    public void ArriveGoalPoint()
    {
        isGoal = true;

        DataManager.Instance.SaveDataBeforeSceneChange();

        SceneManager.LoadScene("GameResultScene");
    }

    // �Ͻ����� (����) ��ư
    public void Setting()
    {
        if (isPause)
        {
            settingUI.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
        }
        else
        {
            settingUI.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
        }
    }

    // 1. ����ϱ� ��ư
    public void Continue()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // 2. ����� ��ư
    public void Restart()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
        SceneManager.LoadScene("MainGameScene");
    }

    // 3. ���� ��ư
    public void Quit()
    {
        Application.Quit();
    }
}
