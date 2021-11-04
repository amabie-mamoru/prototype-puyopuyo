using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puyopuyo.UI {
    public interface ISkeltonColliderCollection {
        void ToLeft();
        void ToRight();
        void ToDown();
    }
    public class SkeltonColliderCollection : ISkeltonColliderCollection
    {
        private enum Direction {
            UpperLeft,
            LowerLeft,
            LowerRight,
            UpperRight
        }
        private Dictionary<int, Vector3> OFFSETS = new Dictionary<int, Vector3>()
        {
            { (int)Direction.UpperLeft, new Vector3(-1, 0, 0) },
            { (int)Direction.LowerLeft, new Vector3(-1, -1, 0) },
            { (int)Direction.LowerRight, new Vector3(1, -1, 0) },
            { (int)Direction.UpperRight, new Vector3(1, 0, 0) }
        };
        private Transform fieldTransform;
        private Vector3 landmarkPosition;
        private Dictionary<int, SkeltonCollider> skeltonColliders = new Dictionary<int, SkeltonCollider>();

        public SkeltonColliderCollection(Transform fieldTransform, Vector3 landmarkPosition, Puyo targetPuyo)
        {
            foreach (var kvp in OFFSETS)
            {
                skeltonColliders.Add(kvp.Key, Application.SkeltonColliderGenerator.Instance.Generate(fieldTransform, landmarkPosition + kvp.Value, targetPuyo));
            }
        }

        private Direction ToDirection(int key)
        {
            return (Direction)Enum.ToObject(typeof(Direction), key);
        }

        public bool CanToLeft()
        {
            bool hasCollision = false;
            foreach (var kvp in skeltonColliders)
            {
                if (ToDirection(kvp.Key) != Direction.UpperLeft) { continue; }
                if (kvp.Value.HasCollision) { hasCollision = true; }
            }
            return !hasCollision;
        }

        public bool CanToRight()
        {
            bool hasCollision = false;
            foreach (var kvp in skeltonColliders)
            {
                if (ToDirection(kvp.Key) != Direction.UpperRight) { continue; }
                if (kvp.Value.HasCollision) { hasCollision = true; }
            }
            return !hasCollision;
        }

        // TODO: 現状壁側で下げる,接地直前で下げる動作に対応していない（後者は仕様として無視可能）
        public bool CanToDown()
        {
            bool hasCollision = false;
            foreach (var kvp in skeltonColliders)
            {
                if (ToDirection(kvp.Key) == Direction.UpperLeft || ToDirection(kvp.Key) == Direction.UpperRight) { continue; }
                if (kvp.Value.HasCollision) { hasCollision = true; }
            }
            return !hasCollision;
        }

        public void ToLeft()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToLeft();
            }
        }

        public void ToRight()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToRight();
            }
        }

        public void ToDown()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToDown();
            }
        }

        public void ToFall()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToFall();
            }
        }

        public void ToJustTouch()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToJustTouch();
            }
        }

        public void ToCancelTouching()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToCancelTouching();
            }
        }

        public void TryToKeepTouching()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.TryToKeepTouching();
            }
        }

        public void ToJustStay()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToJustStay();
            }
        }

        public void ToStay()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.ToStay();
            }
        }

        public void Dispose()
        {
            foreach (var kvp in skeltonColliders)
            {
                kvp.Value.Destroy();
            }
            skeltonColliders = new Dictionary<int, SkeltonCollider>();
        }
    }
}
