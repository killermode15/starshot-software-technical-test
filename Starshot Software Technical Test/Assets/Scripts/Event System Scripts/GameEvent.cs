using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{

    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        #region Private Properties
        private List<GameEventListener> listeners = new List<GameEventListener>();
        #endregion

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}