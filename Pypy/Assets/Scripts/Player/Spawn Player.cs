using Pypy;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] dataOfPlayer;
    [SerializeField] private GameObject _camera;

    private void Awake()
    {
        for(int i = 0; i < dataOfPlayer.Length; i++)
        {
            DataOfPlayer spawnobject = dataOfPlayer[i].GetComponent<IInstansePlayer>().DataOfPlayer;
            if(spawnobject.WasBuying && spawnobject.WasChousing)
            {
                GameObject newObject = Instantiate(dataOfPlayer[i], transform);
                _camera.transform.SetParent(newObject.transform);
            }
        }
    }
}
