using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {

        [SerializeField] private float health = 100f;

        private bool isDead;

        private Animator animator;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        private void Start() {
            isDead = false;
        }

        public void TakeDamage(float damage) {
            health = Mathf.Max(health - damage, 0);
            
            if (health == 0) {
                Die();
            }
        }

        private void Die() {
            if (!isDead) {
                animator.SetTrigger("die");
                isDead = true;
            }
        }
    }
}