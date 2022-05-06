using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class EcomomicTracker : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Transform _sliderParent;
        [SerializeField] private Slider _valueSlider;
        [SerializeField] private float lifetime = .25f;

        private Camera _camera;
        float offset;

        private void Start()
        {
            _camera = Camera.main;
            offset = Vector3.Distance(_sliderParent.position, _enemy.transform.position);
        }

        private void Update()
        {
            Vector3 position = _enemy.transform.position;
            _sliderParent.LookAt(_camera.transform);
            _sliderParent.position = Vector3.Lerp(_sliderParent.position, new Vector3(position.x, position.y + offset, position.z), .5f);
            _valueSlider.value = _enemy.RemainingValue();
            
            _sliderParent.gameObject.SetActive(show());
        }

        private bool show() => Time.time - _enemy.lastHitTime < lifetime;
    }
}