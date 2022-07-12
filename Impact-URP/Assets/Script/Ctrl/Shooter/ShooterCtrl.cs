using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using Imapct.Combact;

namespace Imapct.Ctrl
{
    public class ShooterCtrl : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
        [SerializeField] private float normalSensitivity;
        [SerializeField] private float aimSensitivity;
        [SerializeField] private LayerMask aimColliderLayer;
        [SerializeField] private Transform aimTransform;

        private StarterAssetsInputs _input;
        private ThirdPersonController thirdPersonController;
        private WeaponSetup weaponSetup;

        private void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
            thirdPersonController = GetComponent<ThirdPersonController>();
            weaponSetup = GetComponent<WeaponSetup>();
        }

        private void Update()
        {
            Vector3 mouseWorldPosition = GetWorldPosition();
            aimTransform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, mouseWorldPosition.z);
            GetInputToRotate(mouseWorldPosition);
        }

        private void GetInputToRotate(Vector3 mouseWorldPosition)
        {
            if (_input.fire2)
            {
                aimVirtualCamera.gameObject.SetActive(true);
                thirdPersonController.SetSensitivity(aimSensitivity);
                thirdPersonController.SetRotateOnMove(false);

                RotatePlayer(mouseWorldPosition);
            }
            else
            {
                aimVirtualCamera.gameObject.SetActive(false);
                thirdPersonController.SetSensitivity(normalSensitivity);
                thirdPersonController.SetRotateOnMove(true);
            }

            if (weaponSetup.isFire)
            {
                thirdPersonController.SetSensitivity(aimSensitivity);
                thirdPersonController.SetRotateOnMove(false);

                RotatePlayer(mouseWorldPosition);
            }
            else
            {
                thirdPersonController.SetSensitivity(normalSensitivity);
                thirdPersonController.SetRotateOnMove(true);
            }
        }

        private void RotatePlayer(Vector3 mouseWorldPosition)
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }

        private Vector3 GetWorldPosition()
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Ray ray = NewRay();
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.PositiveInfinity, aimColliderLayer))
            {
                mouseWorldPosition = raycastHit.point;
            }
            else
            {
                mouseWorldPosition = ray.GetPoint(10);
            }

            return mouseWorldPosition;
        }

        private static Ray NewRay()
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            return ray;
        }

        public void Fire(GameObject projectile, Transform firePoint)
        {
            Vector3 mouseWorldPosition = GetWorldPosition();
            Vector3 aimDir = (mouseWorldPosition - firePoint.position).normalized;
            Instantiate(projectile, firePoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }
    }
}
