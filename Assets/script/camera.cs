using UnityEngine;
using Unity.Cinemachine;
public class camera : MonoBehaviour
{
    public CinemachineImpulseSource Source;
    public void cameraShake()
    {
        if (Source != null)
        {
            Source.GenerateImpulse();
        }
    }
}
