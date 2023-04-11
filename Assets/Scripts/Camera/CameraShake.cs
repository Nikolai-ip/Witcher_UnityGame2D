using System.Threading.Tasks;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    private float duration;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;

    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;

    private void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
        duration = shakeDuration;
        FindObjectOfType<HitController>().onHit += ShakeCamera;
    }

    private void Update()
    {
        originalPos = camTransform.position;
    }

    private async void ShakeCamera()
    {
        while (duration > 0)
        {
            camTransform.position = originalPos;
            camTransform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            camTransform.position = new Vector3(camTransform.position.x, camTransform.position.y, -10);
            duration -= Time.deltaTime * decreaseFactor;
            await Task.Yield();
        }
        duration = shakeDuration;
    }
    public async void ShakeCameraDoubleForce()
    {
        while (duration > 0)
        {
            camTransform.position = originalPos;
            camTransform.position = originalPos + (Random.insideUnitSphere * shakeAmount)*2;
            camTransform.position = new Vector3(camTransform.position.x, camTransform.position.y, -10);
            duration -= Time.deltaTime * decreaseFactor;
            await Task.Yield();
        }
        duration = shakeDuration;
    }
}