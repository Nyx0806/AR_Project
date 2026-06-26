using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CreatureCardIntro : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image fireImage;
    [SerializeField] TMP_Text[] texts;

    void Start()
    {
        PlayIntro();
    }

    void PlayIntro()
    {
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.one * 0.85f;

        if (fireImage != null)
            fireImage.transform.localScale = Vector3.zero;

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i] == null) continue;

            texts[i].alpha = 0;
            texts[i].transform.localScale = Vector3.one * 0.95f;
        }

        Sequence seq = DOTween.Sequence();

        seq.Append(canvasGroup.DOFade(1f, 0.6f));
        seq.Join(transform.DOScale(1f, 0.9f).SetEase(Ease.OutBack));

        seq.AppendInterval(0.2f);

        seq.AppendCallback(() =>
        {
            PlayTextsSmooth();
        });

        seq.AppendInterval(0.3f);

        seq.AppendCallback(() =>
        {
            PlayNumbers();
        });

        seq.AppendInterval(0.3f);

        seq.AppendCallback(() =>
        {
            PlayFire();
        });

        seq.AppendInterval(0.2f);

        seq.AppendCallback(() =>
        {
            transform.DOScale(1.02f, 2f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        });
    }

    void PlayTextsSmooth()
    {
        float delay = 0f;

        for (int i = 0; i < texts.Length; i++)
        {
            var txt = texts[i];
            if (txt == null) continue;

            txt.DOFade(1f, 0.8f).SetDelay(delay);
            txt.transform.DOScale(1f, 0.8f)
                .SetEase(Ease.OutBack)
                .SetDelay(delay);

            delay += 0.08f;
        }
    }

    void PlayNumbers()
    {
        CountNumber(texts[2], 300f, " KG", true, 1.4f);
        CountNumber(texts[5], 3.81f, " M", false, 1.6f);
        CountNumber(texts[7], 8.2f, " M", false, 1.6f);
    }

    void PlayFire()
    {
        if (fireImage == null) return;

        fireImage.transform.localScale = Vector3.zero;

        fireImage.transform
            .DOScale(1f, 0.7f)
            .SetEase(Ease.OutBack)
            .SetDelay(0.2f);
    }

    void CountNumber(TMP_Text txt, float target, string suffix, bool isInt, float duration)
    {
        float value = 0;

        DOTween.To(
            () => value,
            x =>
            {
                value = x;

                if (isInt)
                    txt.text = Mathf.RoundToInt(value) + suffix;
                else
                    txt.text = value.ToString("0.00") + suffix;
            },
            target,
            duration
        ).SetEase(Ease.OutCubic);
    }
}