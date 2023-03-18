using UnityEngine;



public class SoundMediator : MonoBehaviour
{
    [SerializeField] private Sound _gameLoadingCompleateSound;    
    [SerializeField] private Sound _gameStartSound;    
    [SerializeField] private Sound[] _loseInGameSounds;   
    [SerializeField] private Sound[] _winInGameSounds;
    [SerializeField] private Sound _markRemoveSound;    
    [SerializeField] private Sound[] _markPlacedSounds;    
    [SerializeField] private Sound _openZerosSound;    
    [SerializeField] private AudioSource[] _sources;
    [SerializeField] private PlatesGrid _plateGrid;
    
    private CoroutineCounter _zeroOpenedCounter = new();

    private void OnEnable()
    {
        _plateGrid.StartedGame += OnStartedGame;
        _plateGrid.LoseInGame += OnLoseInGame;
        _plateGrid.WinInGame += OnWinInGame; 

        _plateGrid.PreGenerationComleated += OnPreGenerationCompleated;
        _plateGrid.OpenNeerbyZerosCoroutineStarted += OnOpenNeerbyZerosCoroutineStarted;
        _plateGrid.OpenNeerbyZerosCoroutineStoped += OnOpenNeerbyZerosCoroutineStoped;
        _plateGrid.PlatesMarkChanged += OnPlatesMarkChanged;
    }

    private void OnDisable()
    {
        _plateGrid.StartedGame -= OnStartedGame;
        _plateGrid.LoseInGame -= OnLoseInGame;
        _plateGrid.WinInGame -= OnWinInGame;

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

    private void OnLoseInGame()
    {
        var sound = GetRandom(_loseInGameSounds);
        PlayOneShot(sound);
    }

    private void OnWinInGame()
    {
        var sound = GetRandom(_winInGameSounds);
        PlayOneShot(sound);        
    }


    private void OnOpenNeerbyZerosCoroutineStoped()
    {
        _zeroOpenedCounter.OneEnded();        
    }

    private void OnPlatesMarkChanged(bool placed,Vector2Int _)
    {
        if (placed)
        {
            var sound = GetRandom(_markPlacedSounds);
            PlayOneShot(sound);
        }
        else
        {
            PlayOneShot(_markRemoveSound);
        }
    }

    private Sound GetRandom(Sound[] sounds)
    {
        return sounds[Random.Range(0, sounds.Length)];
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
