using UnityEngine;
using Unity.Cinemachine;
public class camera : MonoBehaviour
{
    private CinemachineImpulseSource Source;
    void Start()
    {
        Source = GetComponent<CinemachineImpulseSource>();
    }
    public void cameraShake(float strength)
    {
        if (Source != null)
        {
            Source.GenerateImpulse(strength);
        }
    }
}
