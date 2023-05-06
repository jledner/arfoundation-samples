using UnityEngine;
using System.Collections;
using System;
using System.Numerics;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class PinchRotate : MonoBehaviour
    {
        const float pinchTurnRatio = Mathf.PI / 2;
        const float minTurnAngle = 0;
        const float pinchRatio = 1;

        // <summary> // The delta of the angle between two touch points // </summary> 
        private float turnAngleDelta;

        // <summary> // The angle between two touch points // </summary> 
        private float turnAngle;

        /// <summary> ///   Calculates Pinch and Turn - This should be used inside LateUpdate /// </summary>

        private void Calculate()
        {
            turnAngle = turnAngleDelta = 0;

            // if two fingers are touching the screen at the same time ... 
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                // ... if at least one of them moved ... 
                if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    // ... or check the delta angle between them ... 
                    turnAngle = Angle(touch1.position, touch2.position);
                    float prevTurn = Angle(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                    turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                    // ... if it's greater than a minimum threshold, it's a turn! 
                    if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                    {
                        turnAngleDelta *= pinchTurnRatio;
                    }
                    else
                    {
                        turnAngle = turnAngleDelta = 0;
                    }
                }
            }
        }

        private float Angle(Vector2 pos1, Vector2 pos2)
        {
            Vector2 from = pos2 - pos1;
            Vector2 to = new Vector2(1, 0);
            float result = Vector2.Angle(from, to);
            Vector3 cross = Vector3.Cross(from, to);

            if (cross.z > 0)
            {
                result = 360f - result;
            }
            return result;
        }

        void LateUpdate()
        {
            //float pinchAmount = 0;
            Quaternion desiredRotation = transform.rotation;
            Calculate();

            if (Mathf.Abs(turnAngleDelta) > 0)
            {
                Vector3 rotationDeg = Vector3.zero;
                rotationDeg.y = -turnAngleDelta;
                desiredRotation *= Quaternion.Euler(rotationDeg);
            }

            transform.rotation = desiredRotation;
        }
    }
}
