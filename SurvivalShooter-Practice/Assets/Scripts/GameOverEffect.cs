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
        StartCoroutine(CoReStart());
    }

    // 가리는게 FadeOut
    // 서서히 본영상이 나오면 FadeIn
    public IEnumerator CoFadeOut()
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
        yield return StartCoroutine(CoFadeOut());

        yield return new WaitForSeconds(2.7f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
