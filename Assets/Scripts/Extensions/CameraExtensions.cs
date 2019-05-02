using System.Collections;
using UnityEngine;

namespace Extensions
{
    internal static class CameraExtensions
    {
        public static IEnumerator Shake(this Camera camera, float duration, float magnitude)
        {
            var originalPos = camera.transform.localPosition;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                var x = Random.Range(-1f, 1f) * magnitude;

                camera.transform.localPosition = new Vector3(x, originalPos.y, originalPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            camera.transform.localPosition = originalPos;
        }
    }
}
