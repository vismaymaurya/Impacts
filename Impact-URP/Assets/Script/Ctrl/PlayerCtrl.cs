using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Imapct.Ctrl
{
    public class PlayerCtrl : MonoBehaviour
    {
        [SerializeField] CharacterController controller;
        [SerializeField] Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            animator.SetFloat("Speed", direction.magnitude);
        }
    }
}
