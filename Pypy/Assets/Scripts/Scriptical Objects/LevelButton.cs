using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public StarsLevel[] stars = new StarsLevel[3];
    private void Start() {
        stars = GetComponentsInChildren<StarsLevel>();
    }
}
