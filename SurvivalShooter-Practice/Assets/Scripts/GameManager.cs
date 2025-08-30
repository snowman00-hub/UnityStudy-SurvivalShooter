using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        score+= add;  
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
            uiPanel.SetActive(true);
        }
        else
        {
            isPause = false;
            uiPanel.SetActive(false);
        }
    }
}
