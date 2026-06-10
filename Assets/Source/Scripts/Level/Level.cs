using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    public Action OnProgress;
    public Action<string> OnHintShowed;
    public Action<PuzzleButton> OnButtonSelect;

    [SerializeField] private List<PuzzlePlace> _places;
    [SerializeField] private string _nextLevel;

    private PuzzleButton _selectButton;
    private LevelSwitcher _switcher;

    public int PlaceCount => _places.Count;

    [Inject]
    public void Constructor(LevelSwitcher switcher)
    {
        _switcher = switcher;
    }


    public void RegisterPlace(PuzzlePlace place)
    {
        if (_places.Contains(place))
        {
            Debug.LogWarning("Place has in list.");
            return;
        }
        place.OnComplete += PlaceCompleteHandle;
        _places.Add(place);
    }

    public void Select(PuzzleButton place)
    {
        _selectButton = place;
        OnButtonSelect?.Invoke(place);
    }

    public void TryPut(PuzzleDrag part) 
    {
        foreach (PuzzlePlace place in _places)
        {
            if(place.Data.Id == part.Data.Id && Vector2.Distance(part.transform.position, place.transform.position) < part.Data.Distance)
            {
                part.Complete(place);
                return;
            }
        }
        part.Return();
    }

    public void ShowHint(string id)
    {
        OnHintShowed?.Invoke(id);
    }

    public void TryPutBySelect(PuzzlePlace place) 
    {
        if(place.Data.Id == _selectButton.Data.Id)
        {
            place.Complete();
            _selectButton.Complete(place);
        }
    }

    public int GetCompletedPlace() 
    {
        int rezult = 0;
        foreach (PuzzlePlace place in _places) 
        {
            if (place.Completed)
                rezult++;
        }
        return rezult;
    }

    public void PlaceCompleteHandle()
    {
        OnProgress?.Invoke();
        CompleteCheck();
    }

    public void CompleteCheck()
    {
        foreach (PuzzlePlace place in _places)
        {
            if (!place.Completed)
                return;
        }
        Complete();
    }

    private void Complete()
    {
        Debug.Log("LevelComplete");
        _switcher.Load(_nextLevel);
    }
}
