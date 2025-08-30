using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverEffect : MonoBehaviour
{
    public GameObject scoreText;
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        scoreText.transform.SetParent(transform, false);
        StartCoroutine(CoFadeIn());
        StartCoroutine(CoReStart());
    }

    public IEnumerator CoFadeIn()
    {
        float timer = 0f;
        float fadeDuration = 1f;        
        Color color = image.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }

    public IEnumerator CoReStart()
    {
        yield return new WaitForSeconds(3.7f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
