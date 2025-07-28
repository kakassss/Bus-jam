using TMPro;
using UnityEngine;
using Zenject;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    private string _level = "Level:";
    
    private LevelDataManager _levelDataManager;

    [Inject]
    private void Construct(LevelDataManager levelDataManager)
    {
        _levelDataManager = levelDataManager;
    }

    private void OnEnable()
    {
        _levelText.text = _level + (_levelDataManager.GetCurrentLevel() + 1);
    }
}
