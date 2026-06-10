using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PuzzleDrag : MonoBehaviour
{
    private Image _image;
    [field: SerializeField] public PuzzleData Data { get; private set; }

    private PuzzleButton _puzzleButton;
    private Transform _container;

    void Awake()
    {
        _image = GetComponent<Image>(); // GetComponent, не GetComponentInParent!
    }

    public void Setup(PuzzleData puzzleData, PuzzleButton puzzleButton, DragingObjectContainer container) 
    {
        Data = puzzleData;
        _image.sprite = Data.Sprite;
        _image.SetNativeSize();
        _puzzleButton = puzzleButton;
        _container = container.transform;
    }

    public void Complete(PuzzlePlace puzzlePlace)
    {
        transform.DOMove(puzzlePlace.transform.position, 0.25f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            puzzlePlace.Complete();
            transform.parent = _container;
        });
    }

    public void Return()
    {
        transform.DOMove(_puzzleButton.transform.position, 0.8f).SetEase(Ease.InBack).OnComplete(() =>
        {
            _puzzleButton.gameObject.SetActive(true);
            _puzzleButton.Show();
            Destroy(gameObject);
        }
        );
    }

}