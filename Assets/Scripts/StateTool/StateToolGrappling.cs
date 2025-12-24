using System;
using Assets.Scripts.StateMovement;
using UnityEngine;

namespace Assets.Scripts.StateTool
{
    internal class StateToolGrappling : StateTool
    {

        [Header("References")]
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform cam;
        [SerializeField] private Transform gunTip;
        [SerializeField] private LayerMask isGrappleable;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private StateMovementManager stateMovementManager;

        [Header("Grappling")]
        [SerializeField] private float maxGrappleDistance;
        [SerializeField] private float grappleDelayTime;
        [SerializeField] private Vector3 grapplePoint;

        [Header("Cooldown")]
        [SerializeField] private float grapplingCd;
        [SerializeField] private float grapplingCdTimer;

        [Header("Input")]
        [SerializeField] private KeyCode grappleKey = KeyCode.Mouse1;

        private bool grappling = false;



        public void Update()
        {
            if (Input.GetKeyDown(grappleKey))
            {
                if (grappling)
                {
                    StopGrapple();
                } else
                {
                    StartGrapple();

                }

            }

            if (grapplingCdTimer > 0)
                grapplingCdTimer -= Time.deltaTime;

        }

        public void LateUpdate()
        {
            if (grappling)
                lineRenderer.SetPosition(0, gunTip.position);
        }

        private void StartGrapple()
        {
            if (grapplingCdTimer > 0) return;

            grappling = true;

            RaycastHit hit;
            if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, isGrappleable)) 
            {
                grapplePoint = hit.point;

                stateMovementManager.SetStateFreeze();
                Invoke(nameof(ExecuteGrapple), grappleDelayTime); 
            }
            else
            {
                grapplePoint = cam.position + cam.forward * maxGrappleDistance;

                Invoke(nameof(StopGrapple), grappleDelayTime);
            }

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, grapplePoint);

        }

        private void ExecuteGrapple()
        {
            stateMovementManager.SetStateGrappling(grapplePoint);
        }

        private void StopGrapple()
        {
            
            stateMovementManager.SetStateGrounded();
            grappling = false;
            grapplingCdTimer = grapplingCd;
            lineRenderer.enabled = false;
        }

    }
}
