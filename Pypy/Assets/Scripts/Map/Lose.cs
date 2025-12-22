using UnityEngine;
using Pypy.Consts;
using JetBrains.Annotations;

public class Lose : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject[] Off_Panels = new GameObject[2];
    // Add death music and sound effects as needed
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringConstants.PLAYER_TAG))
        {
            losePanel.SetActive(true);
            _camera.transform.SetParent(null);
            foreach(var panel in Off_Panels)
            {
                panel.SetActive(false);
            }
        }
    }
}
