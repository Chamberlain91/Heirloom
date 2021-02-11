using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DragonBones;

namespace Heirloom.Examples.Anim2D.Anim2D
{
    public class AnimEventDispatcher : IEventDispatcher<EventObject>
    {
        private readonly Dictionary<string, HashSet<ListenerDelegate<EventObject>>> _listeners = new Dictionary<string, HashSet<ListenerDelegate<EventObject>>>();

        public bool HasDBEventListener(string type)
        {
            return GetListeners(type).Count > 0;
        }

        public void DispatchDBEvent(string type, EventObject eventObject)
        {
            if (_listeners.TryGetValue(type, out var listeners))
            {
                foreach (var listener in listeners)
                {
                    listener(type, eventObject);
                }
            }
        }

        public void AddDBEventListener(string type, ListenerDelegate<EventObject> listener)
        {
            var listeners = GetListeners(type);
            listeners.Add(listener);
        }

        public void RemoveDBEventListener(string type, ListenerDelegate<EventObject> listener)
        {
            var listeners = GetListeners(type);
            listeners.Remove(listener);
        }

        private HashSet<ListenerDelegate<EventObject>> GetListeners(string type)
        {
            if (!_listeners.TryGetValue(type, out var listeners))
            {
                // Create new listener set
                listeners = new HashSet<ListenerDelegate<EventObject>>();

                // Store by type for next access
                _listeners[type] = listeners;
            }

            return listeners;
        }
    }
}
