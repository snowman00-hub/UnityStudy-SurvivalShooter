using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UiManager uiManager;    

    private float score = 0;

    public void AddScore(float add)
    {
        score+= add;  
        uiManager.SetUpdateScore(score);
    }
}
