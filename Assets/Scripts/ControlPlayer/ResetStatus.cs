using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatus : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void ChangeNewPosition()
    {
        //transform.parent.position = new Vector3(transform.parent.position.x, transform.GetChild(1).GetChild(0).position.y, transform.GetChild(1).GetChild(0).position.z);
        PlayerController.Instance.lockMove = false;
        PlayerController.Instance._isRun = true;
        PlayerController.Instance.speed = PlayerController.Instance.defaultspeed;
        //TestCamera.Instance.lookAt = TestCamera.Instance.player;
        PlayerController.Instance.lockMove = false;
    }
    public void EndAction()
    {
        PlayerController.Instance._isSliding = false;
        PlayerController.Instance.isAction = false;
        PlayerController.Instance.setdefaultSpeed();
        ManagerEffect.Instance.OnMoveSmoke();
    }
    private void ResetVuotRao()
    {
        anim.SetInteger(AnimParameter.vuotrao, 0);
    }
    public void ResetWallClimb()
    {
        anim.SetInteger(AnimParameter.wallclimb, 0);
    }
    public void ResetJump()
    {
        // anim.SetInteger(AnimParameter.jump, 0);
        
        PlayerController.Instance.Run();

    }

    public void StartRun()
    {
        PlayerController.Instance.rig.isKinematic = false;
    }    
    void Pause()
    {
        if (PlayerController.Instance.IsGrounded())
        {
            PlayerController.Instance.rig.isKinematic = true;
        }
        
    }
    public void ResetLeoTuong()
    {
        transform.parent.GetComponent<Rigidbody>().isKinematic = false;
        PlayerController.Instance.isAction = false;
        PlayerController.Instance._isRun = true;
        PlayerController.Instance.leoTuong = false;
        PlayerController.Instance.checkFirst = true;
        PlayerController.Instance.lockMove = false;
        //Debug.Log("hanh dong");
    }
    public void ClimbDie()
    {
        PlayerController.Instance.Die();
    }

    public void Roll()
    {
        PlayerController.Instance.m_LandRoll = true;
    }

    public void EndSlide()
    {
        PlayerController.Instance._isSliding = false;
    }

    public void reset()
    {
        PlayerController.Instance.rig.isKinematic = false;
    }
}
