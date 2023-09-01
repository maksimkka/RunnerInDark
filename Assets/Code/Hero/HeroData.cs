using UnityEngine;

namespace Code.Hero
{
    public struct HeroData
    {
        public GameObject HeroGameObject;
        public Rigidbody HeroRigidbody;
        public float Speed;
        public float JumpForce;
        public float CollectRadius;
        public bool IsGround;
    }
}