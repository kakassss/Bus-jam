using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PopupCloseListener : MonoBehaviour
{
    [SerializeField] private PopupData _popupData;
    [SerializeField] private Button _closeButton;
    
    private PopupManager _popupManager;
    
    [Inject]
    private void Construct(PopupManager popupManager)
    {
        _popupManager = popupManager;
    }
    
    private void OnEnable()
    {
        OnClick();
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

    private void OnClick()
    {
        _closeButton.onClick.AddListener(ClosePopup);
    }

    private void ClosePopup()
    {
        _popupManager.ClosePopupByName();
    }
}
