using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace SpaceShip
{
    
    public class CameraController : MonoBehaviour
    {
        //private float mouseX;
        //private float mouseY;
        public bool cameraFocus;
        public Transform cameraRotator;
        public Transform planet;
        public Camera cam;
        private Vector3 targetRotation;
        private Vector3 currentRotation; 
        
        private Vector3 currentZoom;
        private Vector3 targetZoom;
        
        public SpaceShipInput input;
        public Transform spaceShip;
        public float followSpeed = 1.0f;
        [Range(0.01f, 1f)] public float mouseSensitivity = 0.5f;

        private void Awake()
        {
            input = new SpaceShipInput();

            currentZoom = cam.transform.localPosition;
            targetZoom = cam.transform.localPosition;
        }
        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, spaceShip.position, followSpeed * Time.deltaTime); 
            if (cameraFocus == true)
            {
                Vector3 surfaceDirection = planet.position - transform.position;
                transform.forward = surfaceDirection;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(), 1 * Time.deltaTime);
            }

            currentRotation = Vector3.Lerp(currentRotation, targetRotation , 7f * Time.deltaTime);
            
            currentRotation.x = Mathf.Clamp(currentRotation.x, -45f, 45f);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -45f, 45f);
            
            cameraRotator.transform.localEulerAngles = currentRotation;
            
            currentZoom = Vector3.Lerp(currentZoom, targetZoom, 3f * Time.deltaTime);
            cam.transform.localPosition = currentZoom;
        }

        private void OnEnable()
        {
            input.Enable();
            input.Gameplay.Enable();
            input.Gameplay.CameraRotation.performed += CameraRotation;
            input.Gameplay.Zoom.performed += ZoomOnPerformed;
        }

        private void ZoomOnPerformed(InputAction.CallbackContext zoom)
        {
            targetZoom.z += zoom.ReadValue<Vector2>().y * 0.1f;
            targetZoom.z = Mathf.Clamp(targetZoom.z, -500, -2); //To avoid zooming inside the player or too far away.
        }

        private void CameraRotation(InputAction.CallbackContext mouseDelta)
        {
            targetRotation.y += mouseDelta.ReadValue<Vector2>().x  * mouseSensitivity;
            targetRotation.x += mouseDelta.ReadValue<Vector2>().y  * mouseSensitivity;
        }
    }
    




}