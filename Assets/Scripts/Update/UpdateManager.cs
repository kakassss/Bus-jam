using UnityEngine;
using Zenject;

public class UpdateManager : MonoBehaviour
{
    private UpdateProvider _updateProvider;
    private GameStartIStart _gameStartIıStart;
    
    [Inject]
    private void Construct(UpdateProvider updateProvider, GameStartIStart gameStartIıStart)
    {
        _updateProvider = updateProvider;
        _gameStartIıStart = gameStartIıStart;
    }
    
    private void Update()
    {
        if(_gameStartIıStart.GameStarted == false) return;
        
        _updateProvider.UpdateBehavior();
    }
}
