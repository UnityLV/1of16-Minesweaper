using UnityEngine;
[CreateAssetMenu(fileName = "SpriteBin", menuName = "ScriptableObject/SpriteBin")] 
public sealed class SpriteBin : ScriptableObject
{
    [SerializeField] private Sprite[] _colors;
    public Sprite this[int index]=> _colors[index];
}
