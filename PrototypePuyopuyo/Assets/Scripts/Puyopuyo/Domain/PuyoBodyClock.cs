using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Puyopuyo.Domain {
    public interface IPuyoBodyClock {
        bool ShouldFallAction { get; }
        bool ShouldStayAction { get; }
        void UpdateAboutFall();
        void UpdateAboutTouch();
        void NotifyBeginToFall();
        void NotifyBeginToTouch();
        void NotifyFinishFallAction();
        void NotifyFinishStayAction();
    }
    public class PuyoBodyClock : Puyopuyo.Domain.IPuyoBodyClock
    {
        private float MOVE_FALL_WAITING_SECONDS = 1f;
        private float MOVE_TOUCH_WAITING_SECONDS = 1f;
        private Puyopuyo.Domain.IClock fallClock;
        private Puyopuyo.Domain.IClock touchClock;

        public bool ShouldFallAction { get { return fallClock.Alarm.IsRing; } }
        public bool ShouldStayAction { get { return touchClock.Alarm.IsRing; } }

        public PuyoBodyClock()
        {
            fallClock = new Puyopuyo.Domain.Clock(MOVE_FALL_WAITING_SECONDS);
            touchClock = new Puyopuyo.Domain.Clock(MOVE_TOUCH_WAITING_SECONDS);
        }

        public void UpdateAboutFall()
        {
            if (!fallClock.CanTikTok) { return; }
            fallClock.TikTok();
        }

        public void UpdateAboutTouch()
        {
            if (!touchClock.CanTikTok) { return; }
            touchClock.TikTok();
        }

        public void NotifyBeginToFall()
        {
            fallClock.SetHandsToZero();
        }

        public void NotifyBeginToTouch()
        {
            fallClock.ReturnShippingState();
            touchClock.SetHandsToZero();
        }

        public void NotifyFinishFallAction()
        {
            fallClock.Alarm.Stop();
        }

        public void NotifyFinishStayAction()
        {
            touchClock.ReturnShippingState();
        }
    }
}