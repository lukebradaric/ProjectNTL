using UnityEngine;
using TinyTools.Audio;

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
        [SerializeField] protected bool isTargeted;

        [Space]
        [Header("Status Effects")]
        [SerializeField] private GameObject immuneShield;
        [SerializeField] private GameObject targetedIndicator;

        [Space]
        [Header("Particles")]
        [SerializeField] private GameObject deathParticle;

        [Space]
        [Header("Sound")]
        [SerializeField] private SoundSO deathSoundSO;

        private BoolStack immuneStack = new BoolStack();
        public void AddImmune() => SetIsImmune(immuneStack.Add());
        public void RemoveImmune() => SetIsImmune(immuneStack.Remove());
        private void SetIsImmune(bool value)
        {
            immuneShield.SetActive(value);
            isImmune = value;
        }

        private BoolStack targetedStack = new BoolStack();
        public void AddTargeted() => SetIsTargeted(targetedStack.Add());
        public void RemoveTargeted() => SetIsTargeted(targetedStack.Remove());
        private void SetIsTargeted(bool value)
        {
            targetedIndicator.SetActive(value);
            isTargeted = value;
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

            deathSoundSO.Play(transform.position);

            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
