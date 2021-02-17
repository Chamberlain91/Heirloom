using System;
using System.Collections.Generic;

using DragonBones;

namespace Heirloom.Extras.Animation
{
    internal class DragonEventDispatcher : IEventDispatcher<EventObject>
    {
        private readonly Dictionary<string, HashSet<ListenerDelegate<EventObject>>> _listeners = new Dictionary<string, HashSet<ListenerDelegate<EventObject>>>();
        private readonly List<EventObject> _events = new List<EventObject>();

        public bool HasDBEventListener(string type)
        {
            return GetListeners(type).Count > 0;
        }

        internal void Clear()
        {
            foreach (var ev in _events) { ev.ReturnToPool(); }
            _events.Clear();
        }

        internal void ProcessEvents()
        {
            if (_events.Count > 0)
            {
                for (var i = 0; i < _events.Count; ++i)
                {
                    var eventObject = _events[i];

                    var type = eventObject.type;

                    // 
                    if (_listeners.TryGetValue(type, out var listeners))
                    {
                        foreach (var listener in listeners)
                        {
                            listener(type, eventObject);
                        }
                    }

                    // Recycle event object
                    eventObject.ReturnToPool();
                }

                _events.Clear();
            }
        }

        public void DispatchDBEvent(string type, EventObject eventObject)
        {
            if (!_events.Contains(eventObject))
            {
                _events.Add(eventObject);
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
