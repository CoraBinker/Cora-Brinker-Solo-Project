using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerController player;

    Image healthBar;

    GameObject pauseMenu;
    public bool isPaused = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        { 

         player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();
        healthBar = GameObject.FindGameObjectWithTag("ui_health").GetComponent<Image>();

        pauseMenu = GameObject.FindGameObjectWithTag("pause");
        pauseMenu.SetActive(false);
        } 
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex>=1)
        healthBar.fillAmount = (float)player.health / (float)player.maxHealth;
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else resume();

    }

        public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        LoadLevel(0);
    }

    public void resume()
    {
         if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);

            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }



    public void gameOverScreen()
    {
      
    }
      
   
}
