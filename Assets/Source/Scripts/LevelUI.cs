using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private TextMeshProUGUI _text;
    private Level _level;
    private Tweener _fillTween;

    [Inject]
    public void Constructor(Level level)
    {
        _level = level;
    }

    private void OnEnable()
    {
        _level.OnProgress += UpdateUI;
    }

    private void OnDisable()
    {
        _level.OnProgress -= UpdateUI;
    }

    private void UpdateUI() 
    {
        _text.text = $"{_level.GetCompletedPlace()}/{_level.PlaceCount}";
        float progress = (float)_level.GetCompletedPlace() / _level.PlaceCount;
        _fillTween = _bar.DOFillAmount(progress, 0.4f)
            .SetEase(Ease.OutCubic)
            .From(_bar.fillAmount);
    }
}
