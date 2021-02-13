namespace Heirloom.Extras.Anim2D
{
    public static class WorldClock
    {
        /// <summary>
        /// The time (in seconds) between frames.
        /// </summary>
        public static float Delta { get; private set; }

        /// <summary>
        /// The time since application start.
        /// </summary>
        public static float Time
        {
            get => DragonFactory.Factory.DragonBones.Clock.Time;
            private set => DragonFactory.Factory.DragonBones.Clock.Time = value;
        }

        /// <summary>
        /// The time rate multiplier. Used to control global animation speed.
        /// </summary>
        public static float TimeScale
        {
            get => DragonFactory.Factory.DragonBones.Clock.TimeScale;
            set => DragonFactory.Factory.DragonBones.Clock.TimeScale = value;
        }

        public static void AdvanceTime(float dt)
        {
            // Advance time on dragon bones backend
            DragonFactory.Factory.DragonBones.AdvanceTime(dt);
            Delta = dt;

            // todo: more per-frame ticks, co-routines?
        }
    }
}
