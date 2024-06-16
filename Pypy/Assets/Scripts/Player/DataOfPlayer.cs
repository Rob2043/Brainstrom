using UnityEngine;

[CreateAssetMenu(fileName = "DataOfPlayer", menuName = "Pypy/DataOfPlayer")]
public class DataOfPlayer : ScriptableObject
{
    public GameObject @object;
    public int price;
    public string NameOfScins;
    public bool WasChousing;
    public bool WasBuying;
}
