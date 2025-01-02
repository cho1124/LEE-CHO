using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class CameraEffect : MonoBehaviour
{
    [SerializeField] private Vignette vig;

    private void Awake()
    {
        GetComponent<Volume>().profile.TryGet(out vig);
    }

    public IEnumerator ChangeIntencity(float target)
    {
        float start = vig.intensity.value;
        float lerpFactor = 0.0f;
        while (Mathf.Abs(target - vig.intensity.value) > 0.1f)
        {

            vig.intensity.value = Mathf.Lerp(start, target, lerpFactor);
            lerpFactor += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
