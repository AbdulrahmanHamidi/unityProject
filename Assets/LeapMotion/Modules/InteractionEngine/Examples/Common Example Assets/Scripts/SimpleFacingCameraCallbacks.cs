/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2018.                                 *
 * Leap Motion proprietary and confidential.                                  *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Leap.Unity.Examples
{

    [AddComponentMenu("")]
    public class SimpleFacingCameraCallbacks : MonoBehaviour
    {

        public Transform toFaceCamera;
        public Transform RtoFaceCamera;

        private bool _initialized = false;
        private bool _isFacingCamera = false;

        public UnityEvent OnBeginFacingCamera;
        public UnityEvent OnEndFacingCamera;
        private static bool check = false;

        void Start()
        {
            if (toFaceCamera != null) initialize();
        }

        private void initialize()
        {
            // Set "_isFacingCamera" to be whatever the current state ISN'T, so that we are
            // guaranteed to fire a UnityEvent on the first initialized Update().
            _isFacingCamera = !GetIsFacingCamera(toFaceCamera, RtoFaceCamera, Camera.main);
            _initialized = true;
        }

        void Update()
        {
            if (toFaceCamera != null && !_initialized)
            {
                initialize();
            }
            if (!_initialized) return;

            if (GetIsFacingCamera(toFaceCamera, RtoFaceCamera, Camera.main, _isFacingCamera ? 0.77F : 0.82F) != _isFacingCamera)
            {
                _isFacingCamera = !_isFacingCamera;

                if (_isFacingCamera)
                {
                    OnBeginFacingCamera.Invoke();
                }
                else
                {
                    OnEndFacingCamera.Invoke();
                }
            }
        }

        public static bool GetIsFacingCamera(Transform facingTransform, Transform rfacingTransform, Camera camera, float minAllowedDotProduct = 0.8F)
        {


            bool left = Vector3.Dot((camera.transform.position - facingTransform.position).normalized, facingTransform.forward) > minAllowedDotProduct;
            //bool right = Vector3.Dot((camera.transform.position - rfacingTransform.position).normalized, rfacingTransform.forward) > minAllowedDotProduct;
            bool right = true;
            if (left && right && !check)
            {
                check = true;
            }
            else if (left && check)
            {
                check = true;
            }
            else
            {
                check = false;
            }

            return check;

        }

    }

}
