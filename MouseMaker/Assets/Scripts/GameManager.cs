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
        // 제한 시간
        limitTime -= Time.deltaTime;
        TimeText.text = $"{System.Math.Truncate(limitTime / 60)} : {Mathf.Round(limitTime % 60)}";

        if (limitTime <= 0)
        {
            player.health = 0;
            DataManager.Instance.SaveDataBeforeSceneChange();

            SceneManager.LoadScene("GameResultScene");
        }

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

        // 일시정지한 경우
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

        // 플레이어가 사망할 경우
        if (player.health <= 0)
        {
            player.health = 0;
            limitTime = 0;
            DataManager.Instance.SaveDataBeforeSceneChange();

            SceneManager.LoadScene("GameResultScene");
        }
    }

    // 플레이어가 도착지점에 도달할 경우
    public void ArriveGoalPoint()
    {
        isGoal = true;

        DataManager.Instance.SaveDataBeforeSceneChange();

        SceneManager.LoadScene("GameResultScene");
    }

    // 일시정지 (설정) 버튼
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

    // 1. 계속하기 버튼
    public void Continue()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // 2. 재시작 버튼
    public void Restart()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
        SceneManager.LoadScene("MainGameScene");
    }

    // 3. 종료 버튼
    public void Quit()
    {
        Application.Quit();
    }
}
