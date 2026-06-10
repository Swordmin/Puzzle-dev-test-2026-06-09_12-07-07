using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class PuzzlePlace : MonoBehaviour
{
    public Action OnComplete;

    [field: SerializeField] public PuzzleData Data { get; private set; }
    [field: SerializeField] public bool Completed { get; private set; }

    [SerializeField] private GameObject _hint;

    private Button _button;
    private Level _level;
    private Image _image;

    [Inject]
    public void Constructor(Level level) 
    {
        _level = level;
        _level.RegisterPlace(this);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void OnValidate()
    {
        if (Data)
        {
            gameObject.name = Data.Id + "Place";
        }
    }

    private void OnEnable()
    {
        _level.OnHintShowed += ShowHintHandle;
        _button.onClick.AddListener(Select);
    }

    private void OnDisable()
    {
        _level.OnHintShowed -= ShowHintHandle;
        _button.onClick.RemoveListener(Select);
    }

    public void ShowHintHandle(string id) 
    {
        if(Data.Id == id)
            _hint.SetActive(true);
        else
            _hint.SetActive(false);
    }

    private void Select() 
    {
        _level.TryPutBySelect(this);
    }

    public void Complete() 
    {
        _level.OnHintShowed -= ShowHintHandle;
        Completed = true;
        OnComplete?.Invoke();
        _hint.SetActive(false);
    }
}
