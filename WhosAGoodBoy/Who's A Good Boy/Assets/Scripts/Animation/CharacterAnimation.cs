using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour {

        [SerializeField]
        public CharacterMovement Movement;
        [SerializeField]
        public Animator Animator;

        void Update ()
        {
            SetIsWalking();
            SetDirection();
        }

        void SetDirection()
        {
            var isMoving = Movement.IsMoving;
            var facingVec = Movement.Facing.ToVector2();

            Animator.SetFloat(Parameters.XDir, facingVec.x);
            Animator.SetFloat(Parameters.YDir, facingVec.y);
        }

        void SetIsWalking()
        {
            Animator.SetBool(Parameters.IsWalking, Movement.IsMoving);
        }
    }
}
