using System.Collections.Generic;

using DragonBones;

using DBArmature = DragonBones.Armature;
using DBAnimation = DragonBones.Animation;

namespace Heirloom.Extras.Animation
{
    internal sealed class DragonArmatureProxy : IArmatureProxy
    {
        private readonly DragonEventDispatcher _events = new DragonEventDispatcher();

        public DBArmature Armature { get; internal set; }

        public DBAnimation Animation => Armature?.Animation;

        internal List<DragonSlot> Slots { get; } = new List<DragonSlot>();

        public void DBInit(DBArmature armature)
        {
            Armature = armature;
        }

        public void DBClear()
        {
            _events.Clear();
        }

        public void DBUpdate()
        {
            _events.ProcessEvents();
        }

        #region Event Dispatcher

        public void DispatchDBEvent(string type, EventObject eventObject)
        {
            _events.DispatchDBEvent(type, eventObject);
        }

        public bool HasDBEventListener(string type)
        {
            return _events.HasDBEventListener(type);
        }

        public void AddDBEventListener(string type, ListenerDelegate<EventObject> listener)
        {
            _events.AddDBEventListener(type, listener);
        }

        public void RemoveDBEventListener(string type, ListenerDelegate<EventObject> listener)
        {
            _events.RemoveDBEventListener(type, listener);
        }

        #endregion

        public void Dispose(bool disposeProxy)
        {
            Log.Warning("Disposing Armature Implementation");

            Armature?.Dispose();
            Armature = null;
        }
    }
}
