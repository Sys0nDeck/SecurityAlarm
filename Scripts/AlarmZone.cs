using SecurityAlarm;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecurityAlarm
{
    [RequireComponent(typeof(Collider))]
    public class AlarmZone : MonoBehaviour
    {
        private Collider _collider;
        private HashSet<Collider> _activeIntruders;

        public event Action<int> ZoneStateChanged;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _activeIntruders = new HashSet<Collider>();
        }

        private void Start()
        {
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Thief>(out _) && _activeIntruders.Add(other))
                ZoneStateChanged?.Invoke(_activeIntruders.Count);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_activeIntruders.Remove(other))
                ZoneStateChanged?.Invoke(_activeIntruders.Count);
        }
    }
}