using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CameraFrameLock : CinemachineExtension
{
    [Tooltip("Lock the camera's x and y position to this value")]
    public float maxxpos = 35f;
    public float maxypos = 15f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;

            if (pos.x > maxxpos)
            {
                pos.x = maxxpos;
            }
            if (pos.x < -maxxpos)
            {
                pos.x = -maxxpos;
            }

            if (pos.y > maxypos)
            {
                pos.y = maxypos;
            }
            if (pos.y < -maxypos)
            {
                pos.y = -maxypos;
            }

            state.RawPosition = pos;
        }
    }
}