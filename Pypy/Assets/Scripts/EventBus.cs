using System;
using UnityEngine;


namespace CustomEventBus
{
    public static class EventBus
    {
        public static Action<bool> CheckButton;
        public static Action<int> CheckStars;
        public static Action CheckEnd;
        public static Action<GameObject> ScinCheckButton;
        public static Action<GameObject> ChangeNameText;
    }

}
