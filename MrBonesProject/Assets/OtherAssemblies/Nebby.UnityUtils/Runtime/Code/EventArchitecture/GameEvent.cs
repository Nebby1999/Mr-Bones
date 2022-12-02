using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    [CreateAssetMenu(menuName = "Nebby/GameEvent", fileName = "New GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for(int i = listeners.Count -1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if(listeners.Contains(listener))
            {
                return;
            }
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if(!listeners.Contains(listener))
            {
                return;
            }
            listeners.Remove(listener);
        }
    }
}