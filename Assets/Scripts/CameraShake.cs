using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera cameraobject;
    private float shaketimer;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        cameraobject = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin = cameraobject.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = intensity;
        shaketimer = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (shaketimer > 0)
        {
            shaketimer -= Time.deltaTime;
            if (shaketimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin channelPerlin = cameraobject.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                channelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
