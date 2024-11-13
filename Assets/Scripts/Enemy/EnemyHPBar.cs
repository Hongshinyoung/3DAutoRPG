using UnityEngine;

namespace Enemy
{
    public class EnemyHPBar : MonoBehaviour
    {
        private Transform camera;
        public Condition health;

        private void Start()
        {
            camera = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
        }
    }
}