using System.Collections.Generic;
using UnityEngine;

namespace SecurityAlarm
{
    [RequireComponent(typeof(Collider))]
    public class SecurityZone : MonoBehaviour
    {
        [SerializeField] private AlarmSoundController _alarmSoundController;

        private HashSet<Collider> _activeIntruders;
        private Collider _collider;

        private void Awake()
        {
            _activeIntruders = new HashSet<Collider>();
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Thief>(out _)  && _activeIntruders.Add(other))
                SetAlarmStatus();
        }

        private void OnTriggerExit(Collider other)
        {
            if (_activeIntruders.Remove(other))
                SetAlarmStatus();
        }

        private void SetAlarmStatus()
        {
            if (_activeIntruders.Count > 0)
                _alarmSoundController.PlayAlarm();
            else
                _alarmSoundController.StopAlarm();
        }
    }
}