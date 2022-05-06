using System;
using TMPro;
using UnityEngine;

namespace Player
{
    public class EcomincDamageFloaty : MonoBehaviour
    {
        public TextMeshPro impact;
        [SerializeField] private float lifetime;

        private float spawntime;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        public void CreateFlaoty(float damage, Vector3 location)
        {
            spawntime = Time.time;
            impact.SetText($"{damage}.eth");
            transform.position = location;
        }

        private void Update()
        {
            transform.LookAt(_camera.transform);
            if (Time.time - spawntime > lifetime)
            {
                Destroy(gameObject);
            }
        }
    }
}