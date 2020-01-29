using System;
using System.Collections.Generic;

namespace Heirloom.Game
{
    public class EventDispatcher
    {
        private readonly Dictionary<Type, List<Delegate>> _actionMap;

        public EventDispatcher()
        {
            _actionMap = new Dictionary<Type, List<Delegate>>();
        }

        public void Notify<X>(X eventData) where X : Event
        {
            if (_actionMap.TryGetValue(typeof(X), out var actions))
            {
                foreach (Action<X> action in actions)
                {
                    action(eventData);

                    // If event was consume, terminate
                    if (eventData.IsConsumed)
                    {
                        break;
                    }
                }
            }
        }

        public void Listen<X>(Action<X> action) where X : Event
        {
            // Try to get actions list, creating the list on first access
            if (_actionMap.TryGetValue(typeof(X), out var actions) == false)
            {
                // Create action list
                actions = new List<Delegate>();
            }

            actions.Add(action);
        }
    }

    public abstract class Event
    {
        public bool IsConsumed { get; private set; } = false;

        public void Consume()
        {
            IsConsumed = true;
        }
    }
}
