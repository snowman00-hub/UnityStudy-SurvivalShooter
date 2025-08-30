using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ZombieSpawner zombieSpawner;
    public PlayerShooter shooter;
    public PlayerMovement playerMovement;
    public UiManager uiManager;
    public GameObject uiPanel;

    private float score = 0;
    private bool isPause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePauseOnOff();
        }
    }

    public void AddScore(float add)
    {
        score += add;
        uiManager.SetUpdateScore(score);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GamePauseOnOff()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            uiPanel.SetActive(true);
            zombieSpawner.enabled = false;
            shooter.enabled = false;
            playerMovement.enabled = false;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
            uiPanel.SetActive(false);
            zombieSpawner.enabled = true;
            shooter.enabled = true;
            playerMovement.enabled = true;
        }
    }
}
