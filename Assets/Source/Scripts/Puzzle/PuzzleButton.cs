using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PuzzleButton : MonoBehaviour, IPointerDownHandler
{
    [field: SerializeField] public PuzzleData Data { get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private PuzzleDrag _puzzle;
    [SerializeField] private Button _buttonHint;
    private Image _background;
    private Level _level;
    private DragManager _dragManager;
    [SerializeField] private DragingObjectContainer _container;
    
    [Inject]
    public void Constructor(Level level, DragManager dragManager, DragingObjectContainer dragingObjectContainer)
    {
        _level = level;
        _dragManager = dragManager;
        _container = dragingObjectContainer;
    }

    private void Awake()
    {
        _background = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _level.OnButtonSelect += LevelButtonSelectHandler;
        _buttonHint.onClick.AddListener(ShowHint);
    }

    private void OnDisable()
    {
        _level.OnButtonSelect -= LevelButtonSelectHandler;
        _buttonHint.onClick.RemoveListener(ShowHint);
    }

    private void OnValidate()
    {
        if(Data)
        {
            _icon.sprite = Data.Sprite;
            _icon.SetNativeSize();
        }
    }

    private void Select()
    {
        _level.Select(this);
        _background.color = Color.yellow;
    }

    private void ShowHint() 
    {
        _level.ShowHint(Data.Id);
    }

    private void LevelButtonSelectHandler(PuzzleButton puzzleButton)
    {
        if (puzzleButton == this)
            return;
        _background.color = Color.white;
    }

    public void Show()
    {
        transform.DOScale(1, 0.25f).SetEase(Ease.InOutBack);
    }

    public void Hide()
    {
        transform.DOScale(0, 0.25f).SetEase(Ease.InOutBack).OnComplete(() => gameObject.SetActive(false));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Select();
    }

    public void Complete(PuzzlePlace puzzlePlace) 
    {
        Hide();
        PuzzleDrag piece = Instantiate(_puzzle, GetComponent<RectTransform>().position, Quaternion.identity, _dragManager.transform);
        piece.Setup(Data, this, _container);
        piece.Complete(puzzlePlace);
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        PuzzleDrag piece = Instantiate(_puzzle, _dragManager.transform);

        piece.Setup(Data, this, _container);

        _dragManager.StartDrag(piece);

        Hide();
    }
}