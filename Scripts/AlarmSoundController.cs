using System.Collections;
using UnityEngine;

namespace SecurityAlarm
{
    public class AlarmSoundController : MonoBehaviour
    {
        [SerializeField] private DelayAudioPlayer _delayAudioPlayer;
        [SerializeField] private float _duration = 5f;

        private Coroutine _alarmCoroutine;
        private bool _isIncrease;

        private void Start()
        {
            _delayAudioPlayer.Stop();
            _isIncrease = false;
        }

        public void PlayAlarm()
        {
            if (_isIncrease == false)
            {
                LaunchAlarmCoroutine(0, 1);
                _isIncrease = true;
            }  
        }

        public void StopAlarm()
        {
            if (_isIncrease == true)
            {
                LaunchAlarmCoroutine(1, 0);
                _isIncrease = false;
            }    
        }

        private void LaunchAlarmCoroutine(float start, float end)
        {
            if (_alarmCoroutine != null)
                StopCoroutine(_alarmCoroutine);

            _alarmCoroutine = StartCoroutine(ChangeAlarmVolume(start, end));
        }

        private IEnumerator ChangeAlarmVolume(float start, float end)
        {
            if (_delayAudioPlayer.Volume == 0f)
                _delayAudioPlayer.Play();

            var elapsedTime = 0f;

            while (_delayAudioPlayer.Volume != end)
            {
                elapsedTime += Time.deltaTime;
                _delayAudioPlayer.Volume = Mathf.MoveTowards(start, end, elapsedTime / _duration);
                yield return null;
            }

            if (_delayAudioPlayer.Volume == 0f)
                _delayAudioPlayer.Stop();
        }
    }
}