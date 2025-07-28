using TMPro;
using UnityEngine;
using Zenject;

public class LevelTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private string _timer = "Timer: ";
    
    private LevelTimerManager _levelTimerManager;
    private GameStartIStart _gameStartIıStart;

    [Inject]
    private void Construct(LevelTimerManager levelTimerManager)
    {
        _levelTimerManager = levelTimerManager;
    }
    
    private void Update()
    {
        _timerText.text = _timer + _levelTimerManager.CurrentTime;
    }
}
