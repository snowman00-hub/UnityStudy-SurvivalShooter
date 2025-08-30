using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject gameOverEffect;
    public RawImage RedFlash;
    public Slider hpSlider;
    public Text scoreText;

    private void OnEnable()
    {
        RedFlash.enabled = false;
    }

    private void Start()
    {
        playerHealth.OnHurt += () => StartCoroutine(CoRedFlashEffect());
        playerHealth.OnHurt += () => SetUpdateHpSlider(playerHealth.HP / playerHealth.maxHP);
        playerHealth.OnDeath += () => gameOverEffect.SetActive(true);
    }

    public void SetUpdateScore(float score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void SetUpdateHpSlider(float value)
    {
        hpSlider.value = value;
    }

    public IEnumerator CoRedFlashEffect()
    {
        RedFlash.enabled = true;

        yield return new WaitForSeconds(0.075f);

        RedFlash.enabled = false;
    }
}
