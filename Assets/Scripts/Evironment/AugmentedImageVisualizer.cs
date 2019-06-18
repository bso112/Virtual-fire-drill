using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine;

public class AugmentedImageVisualizer : MonoBehaviour
{



    /// <summary>
    /// The AugmentedImage to visualize.
    /// </summary>
    public AugmentedImage Image;

    /// <summary>
    /// A model for the lower left corner of the frame to place when an image is detected.
    /// </summary>
    public GameObject FrameLowerLeft;

    /// <summary>
    /// A model for the lower right corner of the frame to place when an image is detected.
    /// </summary>
    public GameObject FrameLowerRight;

    /// <summary>
    /// A model for the upper left corner of the frame to place when an image is detected.
    /// </summary>
    public GameObject FrameUpperLeft;

    /// <summary>
    /// A model for the upper right corner of the frame to place when an image is detected.
    /// </summary>
    public GameObject FrameUpperRight;

    /// <summary>
    /// The Unity Update method.
    /// </summary>
    public void Update()
    {
        if (Image == null || Image.TrackingState != TrackingState.Tracking)
        {
            FrameLowerLeft.SetActive(false);
            FrameLowerRight.SetActive(false);
            FrameUpperLeft.SetActive(false);
            FrameUpperRight.SetActive(false);
            return;
        }

        float halfWidth = Image.ExtentX / 2;
        float halfHeight = Image.ExtentZ / 2;
        FrameLowerLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);
        FrameLowerRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.back);
        FrameUpperLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.forward);
        FrameUpperRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);

        FrameLowerLeft.SetActive(true);
        FrameLowerRight.SetActive(true);
        FrameUpperLeft.SetActive(true);
        FrameUpperRight.SetActive(true);
    }
}

