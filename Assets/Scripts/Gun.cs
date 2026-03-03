using UnityEngine;

public class Gun : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip clip;
    Quaternion defRotation;
    bool backToNormal;
    float delay;
    [SerializeField] GameObject gunshotVFX;
    [SerializeField] Transform shootPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
        defRotation = transform.rotation;
        InvokeRepeating(nameof(Shoot), 0.3f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (backToNormal)
        {
            delay += Time.deltaTime;

            if (delay > 0.06f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, defRotation, Time.time * 0.001f);
            }
            if (transform.rotation == defRotation)
            {
                backToNormal = false;
                delay = 0f;
            }
        }
    }

    void Shoot()
    {
        GameObject gunshotPrefab = Instantiate(gunshotVFX, shootPoint.transform);
        transform.Rotate(Vector3.down * 15);
        source.PlayOneShot(clip);
        Destroy(gunshotPrefab, 0.6f);
        backToNormal = true;
    }
}
