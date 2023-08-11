using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviourSingleton<PlayerTrigger>
{
    public PlayerController playerController;
    public Transform posStarEat;
    public LayerMask layer;
    public Vector3[] posPath = new Vector3[5];
    #region OnCollision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Die"))
        {
            if (playerController._isPowerUp)
            {
                collision.gameObject.SetActive(false);
                ManagerEffect.Instance.ShowFxStar(collision.transform.position);
                TestCamera.Instance.CameraShake();
                Vibration.Vibrate(15);
            }
            else
            {
                TestCamera.Instance.CameraShake();
                SoundManager.Instance.PlaySoundDieVaCham();
                playerController.Die();
                ManagerEffect.Instance.VatCanFX();
            }
        }
        if (collision.gameObject.CompareTag("Die2"))
        {
            //TestCamera.Instance.CameraShake();
            SoundManager.Instance.PlaySoundDie();
            playerController.Die();
            playerController.transform.position = new Vector3(playerController.transform.position.x, playerController.transform.position.y - 2, playerController.transform.position.z);
        }

        if (collision.gameObject.CompareTag("RoleOverHurdle"))
        {
            if (playerController.HitCount < 2)
            {
                playerController.HitCount++;
                playerController.anim.Play("HurdleJump", -1, 0);
                playerController.rig.AddForce(new Vector3(0f, 2f, 0), ForceMode.Impulse);
                //playerController.Run();
            }
            else
            {
                playerController.Die();
                playerController.HitCount = 0;
            }
        }
        if (collision.gameObject.CompareTag("LongRoll"))
        {
            if (playerController.HitCount < 2)
            {
                playerController.HitCount++;
                playerController.anim.Play("LongRoll", -1, 0);
                playerController.rig.AddForce(new Vector3(0f, 2f, 0), ForceMode.Impulse);
                //playerController.Run();
            }
            else
            {
                playerController.Die();
                playerController.HitCount = 0;
            }
        }

        /*if (collision.gameObject.CompareTag("Slide"))
        {
            playerController.anim.Play("SlideLoop", -1, 0);
        }
        else
        {
            if(playerController._isRun)
            playerController.Run();
        }*/
    }

    #endregion
    private void Update()
    {
        RaycastStar();
    }
    #region OnTrigger
    
    private void OnTriggerEnter(Collider other)
    {
        if (playerController._isLive)
            switch (other.tag)
            {
                //case "vatcan":
                //    if (playerController.checkAnimPlay("Jump"))
                //    {
                //        playerController.Die();
                //    }
                //    break;
                /*case "slide":
                    if (!playerController._isPowerUp)
                        playerController.Slide();
                    else
                    {
                        Vibration.Vibrate(15);
                        TestCamera.Instance.CameraShake();
                        other.gameObject.SetActive(false);
                        ManagerEffect.Instance.ShowFxStar(other.transform.position);
                    }
                    break;*/
                case "climb":
                    playerController.Climb(other.gameObject.GetComponent<TypeWallClimb>().typeClimb);
                    break;
                /*case "nhayxa":
                    if (!playerController._isPowerUp)
                        playerController.Nhayxa();
                    else
                    {
                        TestCamera.Instance.CameraShake();
                        other.gameObject.SetActive(false);
                        ManagerEffect.Instance.ShowFxStar(other.transform.position);
                        Vibration.Vibrate(15);
                    }

                    break;*/
                /*case "NhayVuotRao":
                    if (!playerController._isPowerUp)
                        playerController.NhayVuotRao();
                    else
                    {
                        TestCamera.Instance.CameraShake();
                        other.gameObject.SetActive(false);
                        ManagerEffect.Instance.ShowFxStar(other.transform.position);
                        Vibration.Vibrate(15);
                    }
                    break;*/
                case "Win":
                    gameObject.GetComponent<SetTopChart>().OffOder();
                    GameManager.Instance.playerPos++;
                    playerController.Win();
                    break;
                
                case "checkPoint":
                    playerController.CheckPoint(other.transform.position);
                    break;
                //case "booster":
                //    playerController.EatItemPower(0.5f);
                //    break;
                case "coin":
                    //ManagerEffect.Instance.EffectTrigger(other.transform.GetChild(0).position);
                    GameManager.instance.EatCoin(100);
                    other.gameObject.SetActive(false);
                    break;
                case "BatNhay":
                    playerController.BatNhay();
                    break;
                case "jump":
                    playerController.Die();
                    break;
                case "rotatecam":
                    TestCamera.Instance.ChangeRotateCamera();
                    break;
                case "slide":
                    if (!playerController._isSliding)
                    {
                        playerController.Die();
                    }
                    break;
            }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("rotatecam"))
        {
            TestCamera.Instance.ChangeDefaultRotateCamera();
        }
    }
    void RaycastStar()
    {
        RaycastHit hit;
        Vector3 direction = transform.GetChild(0).position + Vector3.forward;
        if (Physics.SphereCast(transform.GetChild(0).position, .1f, direction, out hit, .1f, layer))
        {
            hit.collider.transform.DOScale(.03f, 0.2f);
            hit.collider.transform.DOMove(posStarEat.position, .2f).OnComplete(() =>
            {
                //GameManager.instance.EatCoin(1);
                ManagerEffect.Instance.EffectTrigger(hit.collider.gameObject.transform.position);
            });
            hit.collider.enabled = false;
            SoundManager.Instance.PlaySoundCoin();
        }
    }
    #endregion
}
