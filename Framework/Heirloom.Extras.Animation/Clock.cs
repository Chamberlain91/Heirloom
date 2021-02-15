using System;
using System.Collections.Generic;

namespace Heirloom.Extras.Animation
{
    //public class Clock
    //{
    //    /// <summary>
    //    /// A globally available clock.
    //    /// </summary>
    //    public static Clock Global { get; } = new Clock();

    //    private readonly List<IAnimatable> _animatables = new List<IAnimatable>();
    //    private bool _isUpdating;

    //    public float TimeScale { get; set; } = 1F;

    //    public float Delta { get; private set; }

    //    public float Time { get; private set; }

    //    public void Update(float dt)
    //    {
    //        // Mark updating state, to help prevent mutation.
    //        _isUpdating = true;

    //        // Compute time scaled delta
    //        var scaleDelta = dt * TimeScale;
    //        Delta = scaleDelta;

    //        // Accumulate time
    //        Time += scaleDelta;

    //        // Update each animatable
    //        foreach (var animatable in _animatables)
    //        {
    //            animatable.Update(scaleDelta);
    //        }

    //        // No longer updating
    //        _isUpdating = false;
    //    }

    //    #region Add/Remove/Contains IAnimatable

    //    public void Add(IAnimatable animatable)
    //    {
    //        if (_isUpdating) { throw new InvalidOperationException("Unable to modify animatable collection, executing in update."); }
    //        else
    //        {
    //            if (animatable.Clock == null)
    //            {
    //                _animatables.Add(animatable);
    //                animatable.Clock = this;
    //            }
    //            else
    //            {
    //                throw new InvalidOperationException("Unable to add animatable to clock, already attached to another clock");
    //            }
    //        }
    //    }

    //    public void Remove(IAnimatable animatable)
    //    {
    //        if (_isUpdating) { throw new InvalidOperationException("Unable to modify animatable collection, executing in update."); }
    //        else
    //        {
    //            if (animatable.Clock == this)
    //            {
    //                _animatables.Remove(animatable);
    //                animatable.Clock = null;
    //            }
    //            else
    //            {
    //                throw new InvalidOperationException("Unable to remove animatable from clock, attache to a different clock");
    //            }
    //        }
    //    }

    //    public bool Contains(IAnimatable animatable)
    //    {
    //        if (animatable.Clock == this)
    //        {
    //            return _animatables.Contains(animatable);
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    #endregion

    //    // todo: coroutines ( uint StartCoroutine(...), StopCoroutine(handle) )
    //    // todo: intervals ( uint StartInterval(..., duration), StopInterval(handle) )
    //    // todo: 
    //}

    //public interface IAnimatable
    //{
    //    Clock Clock { get; internal set; }

    //    void Update(float dt);
    //}
}
