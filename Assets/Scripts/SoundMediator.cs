using UnityEngine;



public class SoundMediator : MonoBehaviour
{
    [SerializeField] private Sound _gameLoadingCompleateSound;    
    [SerializeField] private Sound _gameStartSound;    
    [SerializeField] private Sound _gameOverSound;    
    [SerializeField] private Sound _markRemoveSound;    
    [SerializeField] private Sound[] _markPlacedSounds;    
    [SerializeField] private Sound _openZerosSound;    
    [SerializeField] private AudioSource[] _sources;
    [SerializeField] private PlatesGrid _plateGrid;
    
    private CoroutineCounter _zeroOpenedCounter = new();

    private void OnEnable()
    {
        _plateGrid.StartedGame += OnStartedGame;
        _plateGrid.GameOver += OnGameOver;
        _plateGrid.PreGenerationComleated += OnPreGenerationCompleated;
        _plateGrid.OpenNeerbyZerosCoroutineStarted += OnOpenNeerbyZerosCoroutineStarted;
        _plateGrid.OpenNeerbyZerosCoroutineStoped += OnOpenNeerbyZerosCoroutineStoped;
        _plateGrid.PlatesMarkChanged += OnPlatesMarkChanged;
    }    

    private void OnDisable()
    {
        _plateGrid.StartedGame -= OnStartedGame;
        _plateGrid.GameOver -= OnGameOver;
        _plateGrid.PreGenerationComleated -= OnPreGenerationCompleated;
        _plateGrid.OpenNeerbyZerosCoroutineStarted -= OnOpenNeerbyZerosCoroutineStarted;
        _plateGrid.OpenNeerbyZerosCoroutineStoped -= OnOpenNeerbyZerosCoroutineStoped;
        _plateGrid.PlatesMarkChanged -= OnPlatesMarkChanged;
    }    

    private void OnPreGenerationCompleated()
    {
        PlayOneShot(_gameLoadingCompleateSound);       
    }    
    private void OnOpenNeerbyZerosCoroutineStarted()
    {
        if (_zeroOpenedCounter.IsZeroCoroutineEnebled)
        {
            PlayOneShot(_openZerosSound);
        }
        _zeroOpenedCounter.OneStarted();
        

    }

    private void OnStartedGame()
    {
        _zeroOpenedCounter.Reset();//HACK: если будут проблема поместить в обработчик события окончания игры
        PlayOneShot(_gameStartSound);
    }

    private void OnGameOver()
    {
        PlayOneShot(_gameOverSound);
    }

    private void OnOpenNeerbyZerosCoroutineStoped()
    {
        _zeroOpenedCounter.OneEnded();        
    }

    private void OnPlatesMarkChanged(bool placed)
    {
        if (placed)
        {
            var sound = _markPlacedSounds[Random.Range(0, _markPlacedSounds.Length)];
            PlayOneShot(sound);
        }
        else
        {
            PlayOneShot(_markRemoveSound);
        }
    }

    private void PlayOneShot(Sound sound)
    {
        foreach (var source in _sources)
        {
            if (source.isPlaying == false)
            {
                source.volume = sound.Volume;
                source.PlayOneShot(sound.AudioClip);
                return;
            }
        }               
    }

}
