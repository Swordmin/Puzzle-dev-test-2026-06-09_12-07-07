using DG.Tweening;
using UnityEngine;

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
        float radius = 170f;
        float spread = 25f;
        float duration = 1f;
        float fadeDuration = 1.5f;

        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i + Random.Range(-spread, spread);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 targetLocal = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            GameObject obj = Instantiate(_effectPart, transform.position, Quaternion.identity, transform);
            CanvasGroup cg = obj.GetComponent<CanvasGroup>() ?? obj.AddComponent<CanvasGroup>();

            // Стартуем с localPosition = zero (центр родителя)
            obj.transform.localPosition = Vector3.zero;

            Sequence seq = DOTween.Sequence();
            seq.Append(obj.transform.DOLocalMove(targetLocal, duration).SetEase(Ease.OutCubic));
            seq.Join(cg.DOFade(0f, fadeDuration).SetDelay(duration - fadeDuration));
            seq.OnComplete(() => Destroy(obj));
        }
    }
}
