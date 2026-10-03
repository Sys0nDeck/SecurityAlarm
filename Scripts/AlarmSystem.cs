using System.Collections.Generic;
using UnityEngine;

namespace SecurityAlarm
{
    public class AlarmSystem : MonoBehaviour
    {
        [SerializeField] private AlarmSoundController _alarmSoundController;
        [SerializeField] private AlarmZone _alarmZone;

        private void OnEnable()
        {
            _alarmZone.ZoneStateChanged += SetAlarmStatus;
        }

        private void OnDisable()
        {
            _alarmZone.ZoneStateChanged -= SetAlarmStatus;
        }

        private void SetAlarmStatus(int intrudersCount)
        {
            if (intrudersCount > 0)
                _alarmSoundController.PlayAlarm();
            else
                _alarmSoundController.StopAlarm();
        }
    }
}