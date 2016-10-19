using System;
using UnityEngine;

namespace Assets.Scripts.Space2Module.CameraControllers
{
    public class CamPanner : MonoBehaviour
    {
        public float SpeedH = 2.0f;
        public float SpeedV = 2.0f;

        public float Yaw = 0.0f;
        public float Pitch = 0.0f;

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Yaw = SpeedH * Input.GetAxis("Mouse X");
                Pitch = SpeedV * Input.GetAxis("Mouse Y");
                transform.Rotate(Pitch, Yaw, 0);
            }

            if (Math.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0.001f)
            {
                transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel"));
            }
        }
    }
}
