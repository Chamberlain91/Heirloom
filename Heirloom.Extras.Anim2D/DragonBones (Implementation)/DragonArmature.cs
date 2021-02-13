using System.Collections.Generic;

using DragonBones;

using DBArmature = DragonBones.Armature;

namespace Heirloom.Extras.Anim2D
{
    internal sealed class DragonArmature : IArmatureProxy
    {
        private readonly DragonEventDispatcher _events = new DragonEventDispatcher();

        public DBArmature Armature { get; internal set; }

        public Animation Animation => Armature?.Animation;

        internal List<DragonSlot> Slots { get; } = new List<DragonSlot>();

        public void DBInit(DBArmature armature)
        {
            Armature = armature;
            DragonFactory.Factory.Clock.Add(Armature);
        }

        public void DBClear()
        {
            if (Armature != null)
            {
                DragonFactory.Factory.Clock.Remove(Armature);
                Armature = null;
            }
        }

        public void DBUpdate()
        {
            // Nothing to do
        }

        #region Event Dispatcher

        public bool HasDBEventListener(string type)
        {
            return _events.HasDBEventListener(type);
        }

        public void DispatchDBEvent(string type, EventObject eventObject)
        {
            _events.DispatchDBEvent(type, eventObject);
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
