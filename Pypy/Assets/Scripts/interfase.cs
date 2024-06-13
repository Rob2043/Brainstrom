
using UnityEngine;

namespace Pypy
{
    public interface IInstansePlayer
    {
        public DataOfPlayer DataOfPlayer {get; set;}
    }
    public interface ISetButtons
    {
        public GameObject[] stars {get; set;}
        public void ActiveStars(int amount);
    }
    public interface INextGame
    {
        public string sceneSelect{get; set;}
    }
}

