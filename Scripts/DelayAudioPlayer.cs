using System.Collections;
using UnityEngine;

namespace SecurityAlarm
{
    public class DelayAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _delay;

        private Coroutine _playCoroutine;

        public float Volume
        {
            get { return _audioSource.volume; }
            set { _audioSource.volume = value; }
        }

        public void Play()
        {
            if (_playCoroutine != null)
                StopCoroutine(_playCoroutine);

            _playCoroutine = StartCoroutine(PlayWithDelay());
        }

        public void Stop()
        {
            if (_playCoroutine != null)
                StopCoroutine(_playCoroutine);
        }

        private IEnumerator PlayWithDelay()
        {
            var time = new WaitForSeconds(_delay);

            bool isPlay = true;

            while (isPlay)
            {
                _audioSource.Play();
                yield return time;
            }
        }
    }
}