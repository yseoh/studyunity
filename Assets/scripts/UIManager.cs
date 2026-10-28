using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _mapNameText;
    
    void Start()
    {
        GameManager.Instance.OnMapLoaded += OnMapLoaded;
        OnMapLoaded(GameManager.Instance.GetCurrentMapName());
    }

    void OnDestroy()
    {
        GameManager.Instance.OnMapLoaded -= OnMapLoaded;
    }

    void OnMapLoaded(string mapName)
    {
        _mapNameText.text = mapName;
    }
}