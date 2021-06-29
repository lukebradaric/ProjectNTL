using UnityEngine;

namespace NTL.Gameplay
{
    public class TankHealth : MonoBehaviour
    {
        [Space]
        [Header("Health")]
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int currentHealth;

        [Space]
        [Header("Statuses")]
        [SerializeField] protected bool isDead;
        [SerializeField] protected bool isImmune;

        [Space]
        [Header("Status Effects")]
        [SerializeField] private GameObject immuneShield;

        [Space]
        [Header("Particles")]
        [SerializeField] private GameObject deathParticle;

        private BoolTimer immuneTimer = new BoolTimer();
        public void AddImmuneTime(float time) => SetIsImmune(immuneTimer.AddTime(time));
        public void RemoveImmuneTime(float time) => SetIsImmune(immuneTimer.RemoveTime(time));

        private void SetIsImmune(bool value)
        {
            immuneShield.SetActive(value);
            isImmune = value;
        }

        public virtual void Reset()
        {
            currentHealth = maxHealth;
            isDead = false;
        }

        public virtual void TakeDamage(int damage)
        {
            if (isImmune || isDead)
                return;

            currentHealth -= damage;

            if (currentHealth <= 0)
                Die();
        }

        public virtual void Heal(int heal)
        {
            currentHealth += heal;
        }

        public virtual void Die()
        {
            isDead = true;

            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
