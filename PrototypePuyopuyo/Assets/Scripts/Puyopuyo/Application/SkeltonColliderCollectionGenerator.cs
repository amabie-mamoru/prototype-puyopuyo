using com.amabie.SingletonKit;
using System.Collections.Generic;
using UnityEngine;

namespace Puyopuyo.Application {
    public class SkeltonColliderCollectionGenerator : SingletonMonoBehaviour<SkeltonColliderCollectionGenerator>
    {
        public UI.SkeltonColliderCollection Generate(Transform fieldTransform, Vector3 landMarkPosition)
        {
            return new UI.SkeltonColliderCollection(fieldTransform, landMarkPosition);
        }
    }
}
