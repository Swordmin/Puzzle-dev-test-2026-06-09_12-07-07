using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CompleteEffect : MonoBehaviour
{
    [SerializeField] private GameObject _effectPart;

    private PuzzlePlace _puzzlePlace;

    private void Awake()
    {
        _puzzlePlace = GetComponent<PuzzlePlace>();
    }

    private void OnEnable()
    {
        _puzzlePlace.OnComplete += Show;
    }

    private void OnDisable()
    {
        _puzzlePlace.OnComplete -= Show;
    }

    private void Show()
    {
        int count = 10;
        float radius = 200f;
        float spread = 20f;
        float flyDuration = 0.4f;
        float fadeDuration = 0.25f;
        float scaleUpDuration = 0.15f;

        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i + Random.Range(-spread, spread);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 targetLocal = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            GameObject obj = Instantiate(_effectPart, transform.position, Quaternion.identity, transform);
            RectTransform rt = obj.GetComponent<RectTransform>();
            Image img = obj.GetComponent<Image>();

            // Начальное состояние
            rt.localPosition = Vector3.zero;
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);


            Sequence seq = DOTween.Sequence();

            // Появляется и летит одновременно
            seq.Append(rt.DOLocalMove(targetLocal, flyDuration).SetEase(Ease.OutQuad));
            seq.Join(rt.DOScale(0.3f, scaleUpDuration).SetEase(Ease.OutBack));

            // Fade после полёта
            seq.Append(img.DOFade(0f, fadeDuration).SetEase(Ease.InQuad));
            seq.OnComplete(() => Destroy(obj));
        }
    }
}
