using System.Collections;
using UnityEngine;

namespace NTL.Gameplay
{
    [RequireComponent(typeof(TankWeapon))]
    public class TankWeaponController : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private TankWeapon tankWeapon;

        [Space]
        [Header("Repeat")]
        // time between shooting
        [SerializeField] private float timeBetweenShots;
        // minimum time between shots (stacking powerups)
        [SerializeField] private float minTimeBetweenShots;
        [SerializeField] private float maxTimeBetweenShots;

        // coroutine of current shoot loop
        private Coroutine shootCoroutine;

        // start shooting on spawn
        private void Start() => EnableShooting();

        public void AddTimeBetweenShots(float time)
        {
            timeBetweenShots += time;

            if(timeBetweenShots > maxTimeBetweenShots)
                timeBetweenShots = maxTimeBetweenShots;
        }
        public void RemoveTimeBetweenShots(float time)
        {
            timeBetweenShots -= time;

            if (timeBetweenShots < minTimeBetweenShots)
                timeBetweenShots = minTimeBetweenShots;
        }

        // turns repeat shooting on
        private void EnableShooting() => shootCoroutine = StartCoroutine(ShootTimer());
        // turns repeat shooting off
        private void DisableShooting() => StopCoroutine(shootCoroutine);

        // Wait seconds and repeat shooting
        private IEnumerator ShootTimer()
        {
            yield return new WaitForSeconds(timeBetweenShots);

            tankWeapon.Shoot();
            shootCoroutine = StartCoroutine(ShootTimer());
        }
    }
}
