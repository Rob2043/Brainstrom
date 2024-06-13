using Pypy;
using UnityEngine;
public class LevelButton : MonoBehaviour, ISetButtons
{
    [SerializeField] private GameObject[] _stars = new GameObject[3];
    public GameObject[] stars {get => _stars; set => stars = value;}
    public void ActiveStars(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
