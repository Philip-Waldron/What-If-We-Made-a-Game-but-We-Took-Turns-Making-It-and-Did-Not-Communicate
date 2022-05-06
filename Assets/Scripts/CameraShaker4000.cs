using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShaker4000 : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float RestSpeed;
    [SerializeField] private float shake = 1f;
    [SerializeField] private CinemachineVirtualCamera _camera;

    public static CameraShaker4000 Instance;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        Instance = this;
        noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update() => noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, 0f, RestSpeed);

    public void ShakeCamera() => noise.m_AmplitudeGain += shake;
}
