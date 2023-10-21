using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Pause Menu")]
    public static bool gameIsPaused = false;
    public KeyCode inventoryKey = KeyCode.Tab;
    public KeyCode pauseKey = KeyCode.Escape;
    public GameObject InventoryUI;
    public GameObject PauseUI;

    [Header("Victory Menu")]
    public GameObject VictoryUI;

    [Header("Defeat Menu")]
    public GameObject DefeatUI;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {

        //Pause functionality
        if(GameManager.GameActive == true)
        {
            if (Input.GetKeyDown(inventoryKey))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    InventoryUI.SetActive(true);
                    Pause();
                }
                    
            }

            if (Input.GetKeyDown(pauseKey))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    PauseUI.SetActive(true);
                    Pause();
                }

            }

        }
        else if (GameManager.GameActive == false)
        {
            if (PlayerStats.Dead == true)
            {
                DefeatUI.SetActive(true);
            }
            else if (GameManager.BossDefeted == true)
            {
                VictoryUI.SetActive(true);
            }
            else
                return;
            Pause();
        }

    }

    public void Resume()
    {
        InventoryUI.SetActive(false);
        PauseUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        gameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        gameIsPaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {

        GameManager.GameActive = true;
        PlayerStats.Dead = false;
        GameManager.BossDefeted = false;
        DefeatUI.SetActive(false);
        VictoryUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     
        

    }
}
