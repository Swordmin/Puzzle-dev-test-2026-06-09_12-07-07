using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Puzzle", order = 1)]

public class PuzzleData : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public float Distance { get; private set; }
}
