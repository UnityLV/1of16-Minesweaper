using UnityEngine;

[System.Serializable]
public class Sound
{
    [Range(0f, 1f)] [SerializeField] private float _volume = 1;

    [field: SerializeField] public AudioClip AudioClip { get; private set; }
    public float Volume => _volume;
}
