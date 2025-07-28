using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class StartManager : MonoBehaviour
{
    private List<IStart> _startList = new List<IStart>();

    private PopupIStart _popupIStart;
    private DisableInputIStart _disableInputIStart;
    private EnableInputIIStart _enableInputIStart;
    private TutorialIStart _tutorialIStart;
    private TimerIStart _timerIStart;
    private FirstIStart _firstIStart;
    private BusIStart _busIStart;
    private GameStartIStart _gameStartIStart;
    
    [Inject]
    private void Construct(PopupIStart popupIStart, DisableInputIStart disableInputIStart, EnableInputIIStart enableInputIStart,
        TutorialIStart tutorialIStart, TimerIStart timerIStart, FirstIStart firstIStart,
        BusIStart busIStart, GameStartIStart gameIStart)
    {
        _popupIStart = popupIStart;
        _disableInputIStart = disableInputIStart;
        _enableInputIStart = enableInputIStart;
        _tutorialIStart = tutorialIStart;
        _timerIStart = timerIStart;
        _firstIStart = firstIStart;
        _busIStart = busIStart;
        _gameStartIStart = gameIStart;
        
        _startList.Add(_firstIStart); 
        _startList.Add(_disableInputIStart);
        _startList.Add(_popupIStart);
        _startList.Add(_tutorialIStart);
        _startList.Add(_enableInputIStart);
        _startList.Add(_busIStart);
        _startList.Add(_timerIStart);
        
        _startList.Add(_gameStartIStart);
    }

    private void Start()
    {
        StartCoroutine(PlayTicks());
    }
    
    private IEnumerator PlayTicks()
    {
        foreach (var tick in _startList)
        {
            yield return tick.Start();
        }
    }
}
