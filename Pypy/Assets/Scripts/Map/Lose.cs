using UnityEngine;
using Pypy.Consts;
using CustomEventBus;

public class Lose : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject[] Off_Panels = new GameObject[2];

    void OnEnable()
    {
        EventBus.DeathPlayer += OnLosePanel;
    }
    // Add death music and sound effects as needed
    private void OnLosePanel()
    {
        losePanel.SetActive(true);
        _camera.transform.SetParent(null);
        foreach (var panel in Off_Panels)
        {
            panel.SetActive(false);
        }
    }
    void OnDisable()
    {
        EventBus.DeathPlayer -= OnLosePanel;
    }
}
