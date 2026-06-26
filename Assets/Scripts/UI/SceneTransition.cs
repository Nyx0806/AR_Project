using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime = 1f;

    private void Start()
    {
        fadeImage.DOFade(0, fadeTime);
    }

    public void LoadScene(string sceneName)
    {
        fadeImage.DOFade(1, fadeTime).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}