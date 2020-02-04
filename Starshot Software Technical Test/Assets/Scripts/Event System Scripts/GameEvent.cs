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

        /// <summary>
        /// Raises the game event and updates all listeners
        /// </summary>
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        /// <summary>
        /// Attach a listener to this event
        /// </summary>
        /// <param name="listener"></param>
        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        /// <summary>
        /// Removes a listener to this event
        /// </summary>
        /// <param name="listener"></param>
        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}