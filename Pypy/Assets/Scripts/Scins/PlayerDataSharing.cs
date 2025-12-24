using System.Collections;
using System.Collections.Generic;
using Pypy;
using UnityEngine;

public class PlayerDataSharing : MonoBehaviour, IInstansePlayer
{
    [SerializeField] DataOfPlayer dataOfPlayer;
    public DataOfPlayer DataOfPlayer { get; set; }

    private void Awake()
    {
        DataOfPlayer = dataOfPlayer;
    }
    
}
