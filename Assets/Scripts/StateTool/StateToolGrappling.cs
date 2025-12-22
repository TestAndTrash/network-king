using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.StringConstant;
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
        [SerializeField] private LineRenderer lr;

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
            if(Input.GetKeyDown(grappleKey)) StartGrapple();

            if (grapplingCdTimer > 0)
                grapplingCdTimer -= Time.deltaTime;

        }

        public void LateUpdate()
        {
            if (grappling)
                lr.SetPosition(0, gunTip.position);
        }

        private void StartGrapple()
        {
            if (grapplingCdTimer > 0) return;

            grappling = true;

            RaycastHit hit;
            if(Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, isGrappleable)) 
            {
                grapplePoint = hit.point;

                Invoke(nameof(ExecuteGrapple), grappleDelayTime);
            }
            else
            {
                grapplePoint = cam.position + cam.forward * maxGrappleDistance;

                Invoke(nameof(StopGrapple), grappleDelayTime);
            }

            lr.enabled = true;
            lr.SetPosition(1, grapplePoint);

        }

        private void ExecuteGrapple()
        {

        }

        private void StopGrapple()
        {
            grappling = false;
            grapplingCdTimer = grapplingCd;
            lr.enabled = false;
        }

    }
}
