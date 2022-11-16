using System.Collections;
using UnityEngine;

namespace Nebby
{
    public class SpriteShaker : MonoBehaviour
    {
        public bool shakeAtStart = true;
        public bool shakeInPlace = true;
        public AnimationCurve defaultShakeCurve;
        public float defaultDuration = 1f;

        private float usedDuration;
        private AnimationCurve usedAnimationCurve;
        private void Start()
        {
            if(shakeAtStart)
            {
                Shake();
            }
        }

        [ContextMenu("Shake")]
        public void Shake()
        {
            Shake(defaultDuration, defaultShakeCurve);
        }

        public void Shake(float dur, AnimationCurve curve)
        {
            usedDuration = dur;
            usedAnimationCurve = curve;

            StartCoroutine(nameof(ShakeAsync));
        }

        public IEnumerator ShakeAsync()
        {
            var startPosition = transform.localPosition;
            float elapsedTime = 0f;

            while(elapsedTime < usedDuration)
            {
                elapsedTime += Time.deltaTime;
                float str = usedAnimationCurve.Evaluate(elapsedTime / usedDuration);
                var random = Random.insideUnitCircle * str;
                var newPos = new Vector3
                {
                    x = shakeInPlace ? startPosition.x + random.x : transform.localEulerAngles.y + random.x,
                    y = shakeInPlace ? startPosition.y + random.y : transform.localPosition.y + random.y,
                    z = shakeInPlace ? startPosition.z : transform.localPosition.z
                };
                transform.localPosition = newPos;
                yield return null;
            }

            transform.localPosition = startPosition;
        }
    }
}
