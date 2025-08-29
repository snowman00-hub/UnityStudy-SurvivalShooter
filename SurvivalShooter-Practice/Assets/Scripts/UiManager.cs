using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public RawImage RedFlash;
    public Text scoreText;

    private void OnEnable()
    {
        RedFlash.enabled = false;
    }

    private void Start()
    {
        playerHealth.OnHurt += () => StartCoroutine(CoRedFlashEffect());
    }

    public void SetUpdateScore(float score)
    {
        scoreText.text = $"Score: {score}";
    }

    public IEnumerator CoRedFlashEffect()
    {
        RedFlash.enabled = true;

        yield return new WaitForSeconds(0.075f);

        RedFlash.enabled = false;
    }
}
