using CustomEventBus;
using Pypy;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] dataOfPlayer;
    [SerializeField] private GameObject _camera;

    private void Awake()
    {
        EventBus.GetSave.Invoke();
        for(int i = 0; i < dataOfPlayer.Length; i++)
        {
            DataOfPlayer spawnobject = dataOfPlayer[i].GetComponent<IInstansePlayer>().DataOfPlayer;
            if(spawnobject.WasBuying && spawnobject.WasChousing)
            {
                GameObject newObject = Instantiate(dataOfPlayer[i], transform);
                newObject.transform.localPosition = new Vector3(0,0,0);
                _camera.transform.SetParent(newObject.transform);
            }
        }
    }
}
