using UnityEngine;

namespace Code.Hero
{
    public struct HeroData
    {
        public GameObject HeroGameObject;
        public Light Light;
        public Rigidbody HeroRigidbody;
        public float Speed;
        public float JumpForce;
        public bool IsGround;
    }
}