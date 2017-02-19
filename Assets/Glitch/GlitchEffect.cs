using UnityEngine;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
public class GlitchEffect : ImageEffectBase
{
    float flicker, flickerTime = 0.5f;
    public float intensity = 0.3f;

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Intensity", intensity);
        material.SetFloat("filterRadius", 0.74f);
        material.SetFloat("flip_up", 0);
        material.SetFloat("flip_down", 1);
        material.SetFloat("displace", 0);

        flicker += Time.deltaTime * intensity;

        if (flicker > flickerTime)
        {
            float random = Random.Range(-3f, 3f) * intensity;
            if (random <= 0.1f)
                random = 0.1f;

            material.SetFloat("filterRadius", random);
            flicker = 0;
            flickerTime = Random.value;
        }

        Graphics.Blit(source, destination, material);
    }
}