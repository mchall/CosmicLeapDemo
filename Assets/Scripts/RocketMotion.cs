using UnityEngine;

public class RocketMotion : MonoBehaviour
{
    public float rotationFrequency = 0.2f;
    public float rotationAmount = 30.0f;
    public Vector3 rotationComponents = new Vector3(1, 0, 0);
    public int rotationOctave = 3;

    float timeRotation;

    Vector2[] noiseVectors;

    Quaternion initialRotation;

    void Awake()
    {
        timeRotation = Random.value * 10;

        noiseVectors = new Vector2[6];

        for (var i = 0; i < 6; i++)
        {
            var theta = Random.value * Mathf.PI * 2;
            noiseVectors[i].Set(Mathf.Cos(theta), Mathf.Sin(theta));
        }

        initialRotation = transform.localRotation;
    }

    void Update()
    {
        timeRotation += Time.deltaTime * rotationFrequency;

        if (rotationAmount != 0.0f)
        {
            var r = new Vector3(
                Fbm(noiseVectors[3] * timeRotation, rotationOctave),
                Fbm(noiseVectors[4] * timeRotation, rotationOctave),
                Fbm(noiseVectors[5] * timeRotation, rotationOctave)
            );
            r = Vector3.Scale(r, rotationComponents) * rotationAmount * 2;
            transform.localRotation = Quaternion.Euler(r) * initialRotation;
        }
    }

    static float Fbm(Vector2 coord, int octave)
    {
        var f = 0.0f;
        var w = 1.0f;
        for (var i = 0; i < octave; i++)
        {
            f += w * (Mathf.PerlinNoise(coord.x, coord.y) - 0.5f);
            coord *= 2;
            w *= 0.5f;
        }
        return f;
    }
}