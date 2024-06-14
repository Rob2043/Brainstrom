using System;
using UnityEngine;


namespace CustomEventBus
{
    public static class EventBus
    {
        #region OptionsAndSaves 
        public static Func<int> CheckStars;
        public static Action CheckEnd;
        public static Action<GameObject> ScinCheckButton;
        public static Action<GameObject> ChangeNameText;
        #endregion OptionsAndSaves
        #region  MovingPlayer

        public static Action<Vector2> WasMoving;
        #endregion MovingPlayer
        #region  Audio
        public static Action<bool> CheckButton;
        #endregion Audio
        #region  Economy
        public static Action<int> AddStars;
        public static Action AddStarsInPlay;
        public static Func<int> GetStars;
        #endregion Economy
    }

}
