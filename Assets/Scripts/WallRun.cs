using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float wallRunDuration = 2f;
    [SerializeField] private float wallRunGravity = -2f;
    [SerializeField] private float wallRunSpeed = 5f;
    [SerializeField] private float wallJumpUpForce = 8f;
    [SerializeField] private float wallJumpSideForce = 6f;
    private bool isWallRunning = false;
    private GameObject currentWall;
    private Vector3 currentWallNormal;
    private Vector3 directionForward;

    [SerializeField] private int jumpCharges = 1;
    private int currentJumpCharges;

    void Update()
    {
        HandleWallRun();
        if (isWallRunning)
        {
            Vector3 move = directionForward * wallRunSpeed + Vector3.up * 0.5f;
            playerController.characterController.Move(move * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isWallRunning)
        {
            WallJump();
        }
    }
    private void ResetWallRun()
    {
        isWallRunning = false;
        playerController.SetGravity();
    }

    void WallJump()
    {
        if (currentJumpCharges > 0)
        {
            Vector3 lateral = Vector3.Cross(Vector3.up, currentWallNormal);
            Vector3 jumpDir = lateral * wallJumpSideForce + Vector3.up * wallJumpUpForce;
            playerController.SetVelocity(jumpDir);
            currentJumpCharges--;
            ResetWallRun();
        }
    }
    private void HandleWallRun()
    {
        //TODO CHECK IF A MID-AIR STATE EXIST OR USE ISGROUNDED FROM PLAYERCONTROLLER
        if (!playerController.IsGrounded())
        {
            Physics.Raycast(transform.position, transform.right, out RaycastHit rightWallHit, 1f);
            Physics.Raycast(transform.position, -transform.right, out RaycastHit leftWallHit, 1f);
            if (rightWallHit.collider != null && rightWallHit.collider.CompareTag("WallRunable"))
            {
                GetDirection(rightWallHit.normal);
                if (currentWall != rightWallHit.collider.gameObject)
                {
                    currentWall = rightWallHit.collider.gameObject;
                    currentJumpCharges = jumpCharges;
                }
            }
            else if (leftWallHit.collider != null && leftWallHit.collider.CompareTag("WallRunable"))
            {
                GetDirection(leftWallHit.normal);
                if (currentWall != leftWallHit.collider.gameObject)
                {
                    currentWall = leftWallHit.collider.gameObject;
                    currentJumpCharges = jumpCharges;
                }
            }
            else
            {
                if (isWallRunning)
                {
                    ResetWallRun();
                }
            }
        }
        else
        {
            currentWall = null;
            if (isWallRunning)
            {
                ResetWallRun();
            }
        }
    }

    private void GetDirection(Vector3 wallNormal)
    {
        currentWallNormal = Vector3.Cross(wallNormal, Vector3.up);
        directionForward = Vector3.Dot(currentWallNormal, transform.forward) > 0 ? currentWallNormal : -currentWallNormal;
        if (!isWallRunning)
        {
            playerController.SetGravity(wallRunGravity);
        }
        isWallRunning = true;
    }
}
