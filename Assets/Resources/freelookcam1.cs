using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Cameras
{
    public class freelookcam1 : PivotBasedCameraRig
    {
        // This script is designed to be placed on the root object of a camera rig,
        // comprising 3 gameobjects, each parented to the next:

        //  Camera Rig
        //      Pivot
        //          Camera

        [SerializeField] private float m_MoveSpeed = 1f;                      // How fast the rig will move to keep up with the target's position.
        [Range(0f, 10f)] [SerializeField] private float m_TurnSpeed = 1.5f;   // How fast the rig will rotate from user input.
        [SerializeField] public float m_TurnSmoothing = 0.0f;                // How much smoothing to apply to the turn input, to reduce mouse-turn jerkiness
        [SerializeField] public float m_TiltMax = 75f;                       // The maximum value of the x axis rotation of the pivot.
        [SerializeField] public float m_TiltMin = 45f;                       // The minimum value of the x axis rotation of the pivot.
        [SerializeField] private bool m_LockCursor = false;                   // Whether the cursor should be hidden and locked.
        [SerializeField] private bool m_VerticalAutoReturn = false;           // set wether or not the vertical axis should auto return
        private bool tiltDir = true;
        public int tiltInt = 260;
        public bool convo;
        public bool lookAtPlanet = false;
        private bool tiltAboveIncrease = false;
        private bool tiltAboveDecrease = false;

        public float m_LookAngle;                    // The rig's y axis rotation.
        public float m_TiltAngle;                    // The pivot's x axis rotation.
        private const float k_LookDistance = 100f;    // How far in front of the pivot the character's look target is.
        public Vector3 m_PivotEulers;
        public Quaternion m_PivotTargetRot;
        public Quaternion m_TransformTargetRot;

        protected override void Awake()
        {
            base.Awake();
            // Lock or unlock the cursor.
            Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !m_LockCursor;
            m_PivotEulers = m_Pivot.rotation.eulerAngles;

            m_PivotTargetRot = m_Pivot.transform.localRotation;
            m_TransformTargetRot = transform.localRotation;
        }


        protected void Update()
        {
            if (convo == false)
            {
                HandleRotationMovement();

                if (m_LockCursor && Input.GetMouseButtonUp(0))
                {
                    Cursor.lockState = m_LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
                    Cursor.visible = !m_LockCursor;
                }
            }
        }


        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        protected override void FollowTarget(float deltaTime)
        {
            if (m_Target == null) return;
            // Move the rig towards target position.
            transform.position = Vector3.Lerp(transform.position, m_Target.position, deltaTime * m_MoveSpeed);
        }


        private void HandleRotationMovement()
        {
            if (Time.timeScale < float.Epsilon)
                return;

            // Read the user input
            var x = CrossPlatformInputManager.GetAxis("Mouse X");
            var y = CrossPlatformInputManager.GetAxis("Mouse Y");

            // Adjust the look angle by an amount proportional to the turn speed and horizontal input.
            m_LookAngle += x * m_TurnSpeed;
            if (m_TiltAngle <= m_TiltMax)
            {
                m_TiltAngle -= y * m_TurnSpeed;
            }
            if (m_TiltAngle < m_TiltMin)
            {
                m_TiltAngle = m_TiltMin;
            }
            if (m_TiltAngle > m_TiltMax)
            {
                m_TiltAngle = m_TiltMax;
            }
            if (m_TiltAngle >= m_TiltMin)
            {
                m_TiltAngle -= y * m_TurnSpeed;
            }

            // Rotate the rig (the root object) around Y axis only:
            m_TransformTargetRot = Quaternion.Euler(0f, m_LookAngle, 0f);

            if (m_VerticalAutoReturn)
            {
                // For tilt input, we need to behave differently depending on whether we're using mouse or touch input:
                // on mobile, vertical input is directly mapped to tilt value, so it springs back automatically when the look input is released
                // we have to test whether above or below zero because we want to auto-return to zero even if min and max are not symmetrical.
                m_TiltAngle = y > 0 ? Mathf.Lerp(0, -m_TiltMin, y) : Mathf.Lerp(0, m_TiltMax, -y);
            }

            else
            {
                /*
                if (m_TiltAngle <= 0){

                    tiltInt = -80;
                }
                else if (m_TiltAngle > 0)
                {

                    tiltInt = 260;
                }
                */
                // on platforms with a mouse, we adjust the current angle based on Y mouse input and turn speed
                if (tiltDir == true)
                {
                    m_LookAngle -= y * m_TurnSpeed;
                }
                if (tiltDir == false)
                {
                    m_LookAngle += y * m_TurnSpeed;
                }


                if (m_TiltAngle >= m_TiltMax ){
                    //m_TiltAngle = m_TiltMin;
                    tiltDir = !tiltDir;

                }
                if (m_TiltAngle <= m_TiltMin)
                {
                   // m_TiltAngle = m_TiltMax;
                    tiltDir = !tiltDir;


                }

                if (Input.GetMouseButtonUp(1)){
                    //print("ADSADASD");
                    m_TiltAngle = Mathf.Lerp(m_TiltAngle, m_TiltMin, 8);
                    //lookAtPlanet = false;

                }

                // and make sure the new value is within the tilt range
                //m_TiltAngle = Mathf.Clamp(m_TiltAngle, -m_TiltMin, m_TiltMax);
            }

            // Tilt input around X is applied to the pivot (the child of this object)
            m_PivotTargetRot = Quaternion.Euler(m_TiltAngle, m_PivotEulers.y, m_PivotEulers.z);

            if (m_TurnSmoothing > 0)
            {
                m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_PivotTargetRot, m_TurnSmoothing * Time.deltaTime);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, m_TransformTargetRot, m_TurnSmoothing * Time.deltaTime);
            }
            else
            {
                m_Pivot.localRotation = m_PivotTargetRot;
                transform.localRotation = m_TransformTargetRot;
            }
        }
    }
}
