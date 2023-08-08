using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using Cinemachine;

public class PlayerController : MonoBehaviourSingleton<PlayerController>
{
    public GameObject blackScreenCamera;
    public bool _isLive;
    public bool _isRun;
    public bool _isGround;
    public bool _isSliding;
    public bool _isPowerUp;
    public float speed;
    private float speedMoveX;
    private float heigh_Jump;
    private float speed_Booster;
    private float time_Booster;
    private float speedLeoTuong = .8f;
    public Rigidbody rig;

    public Animator anim;
    //public Transform posCheckFace;
    //public Transform posCheckGround;
    public Transform posCheckJump;
    public Transform originJump;
    // public Transform JumpLeft;
    // public Transform JumpRight;
    // public Transform checkRight;
    // public Transform checkLeft;
    public LayerMask layerGround;
    private float distanceJump = .3f;
    public VariableJoystick variableJoystick;
    public bool lockMove;
    CapsuleCollider capsuleCollider;
    private float minPower = 0;
    public const float maxPower = 3;
    private float tempPower = 0;
    private float valuePowerUp;
    Coroutine coroutinePower;
    [SerializeField] Material mat_player;
    public bool leoTuong;
    public bool endLeoTuong;
    public bool wallRunLeft;
    public bool wallRunRight;
    [SerializeField] GameObject txtText;
    public bool fullBooster;
    private float timeTemp;
    private bool checkLonvong;
    private bool reduceVelocity;
    private float tempReduce;


    public int HitCount = 0;
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        // BatNhay();
        // setIdle();
        
        anim.Play("Idle",-1,0);
        
        hasJumped = false;
        capsuleCollider = GetComponent<CapsuleCollider>();
        posCheckPoint = transform.position;
        
        anim.SetFloat("playerSpeed", speed);
    }
    public void SetValuePlayer(bool checkFirebase)
    {
        speed = 1.6f + PlayerprefSave.speedUpgrade;
        time_Booster = GameManager.instance.time_Booster + PlayerprefSave.boosterUpgrade;
        speedMoveX = GameManager.instance.sensity_Move;
        heigh_Jump = GameManager.instance.heigh_Jump;
        speed_Booster = GameManager.instance.speed_Booster;
        //maxPower = time_Booster;
        if (checkFirebase)
        {
            this.PostEvent(EventID.SpeedMain, speed);
            this.PostEvent(EventID.SetHeighJump, heigh_Jump);
        }
        this.PostEvent(EventID.SetTextAI);
        Debug.Log("speed: " + speed + "----------" + "time booster: " + time_Booster);
    }
    public void StartGame()
    {
       // Run();
        checkFirst = true;
        _isGround = IsGrounded();
        _isRun = true;
    }

    public bool IsGrounded()
    {
        return Physics.CheckCapsule(capsuleCollider.bounds.center, new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.min.y, capsuleCollider.bounds.center.z),
            capsuleCollider.radius / 3, layerGround);
    }
    public bool IsFrontUp()
    {
        return Physics.CheckCapsule(new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.max.z),
            new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.max.z), capsuleCollider.radius / 3, layerGround);
    }
    public bool IsFrontUpLeft()
    {
        return Physics.CheckCapsule(new Vector3(capsuleCollider.bounds.min.x, capsuleCollider.bounds.max.y, capsuleCollider.bounds.max.z),
            new Vector3(capsuleCollider.bounds.min.x, capsuleCollider.bounds.max.y, capsuleCollider.bounds.max.z), capsuleCollider.radius, layerGround);
    }
    public bool IsFrontUpRight()
    {
        return Physics.CheckCapsule(new Vector3(capsuleCollider.bounds.max.x, capsuleCollider.bounds.max.y, capsuleCollider.bounds.max.z),
            new Vector3(capsuleCollider.bounds.max.x, capsuleCollider.bounds.max.y, capsuleCollider.bounds.max.z), capsuleCollider.radius, layerGround);
    }
    public bool IsLeft()
    {
        return Physics.CheckCapsule(new Vector3(capsuleCollider.bounds.min.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.min.z),
            new Vector3(capsuleCollider.bounds.min.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.min.z), capsuleCollider.radius, layerGround);
    }
    public bool IsRight()
    {
        return Physics.CheckCapsule(new Vector3(capsuleCollider.bounds.max.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.min.z),
            new Vector3(capsuleCollider.bounds.max.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.min.z), capsuleCollider.radius, layerGround);
    }
    private void FixedUpdate()
    {
        if (_isLive)
        {
            if (!lockMove)
            {
                if (!leoTuong)
                {
                    if (variableJoystick.Horizontal > 0)
                        transform.GetChild(0).DOLocalRotate(new Vector3(0, 20, 0), .5f);
                    else if (variableJoystick.Horizontal < 0)
                        transform.GetChild(0).DOLocalRotate(new Vector3(0, -20, 0), .5f);
                    else
                        transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, 0), .5f);
                    if (wallRunLeft)
                    {
                        if (variableJoystick.Horizontal < 0)
                            rig.velocity = new Vector3(0, rig.velocity.y, rig.velocity.z);
                        else
                            rig.velocity = new Vector3(variableJoystick.Horizontal * speedMoveX, rig.velocity.y, rig.velocity.z);
                    }
                    else if (wallRunRight)
                    {
                        if (variableJoystick.Horizontal > 0)
                            rig.velocity = new Vector3(0, rig.velocity.y, rig.velocity.z);
                        else
                            rig.velocity = new Vector3(variableJoystick.Horizontal * speedMoveX, rig.velocity.y, rig.velocity.z);
                    }
                    // else if (!wallRunRight && !wallRunLeft)
                    // {
                    //     if (checkAnimPlay("Run"))
                    //     {
                    //         if (checkWallLeft() && variableJoystick.Horizontal < 0)
                    //             rig.velocity = new Vector3(0, rig.velocity.y, rig.velocity.z);
                    //         else if (checkWallRight() && variableJoystick.Horizontal > 0)
                    //             rig.velocity = new Vector3(0, rig.velocity.y, rig.velocity.z);
                    //         else
                    //             rig.velocity = new Vector3(variableJoystick.Horizontal * speedMoveX, rig.velocity.y, rig.velocity.z);
                    //     }
                    //     else
                    //         rig.velocity = new Vector3(variableJoystick.Horizontal * speedMoveX, rig.velocity.y, rig.velocity.z);
                    // }
                }
                // else
                // {
                //     Vector3 direction = Vector3.right * variableJoystick.Horizontal;
                //     if (checkWallLeft() && variableJoystick.Horizontal < 0)
                //         transform.Translate(direction * 0 * Time.deltaTime);
                //     else if (checkWallRight() && variableJoystick.Horizontal > 0)
                //         transform.Translate(direction * 0 * Time.deltaTime);
                //     else
                //         transform.Translate(direction * speedMoveX * Time.deltaTime);
                // }
            }
            else
            {
                transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, 0), .5f);
            }
            if (_isRun)
            {
                rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y, speed + tempPower + tempReduce);
            }
        }
    }
    private void Update()
    {
        if (_isLive)
        {
            if (!leoTuong)
            {
                if (IsGrounded())
                {
                    if (checkFirst)
                    {
                        if (hasJumped)
                        {
                            anim.Play("LandRoll", -1, 0);
                            hasJumped = false;
                            tempPower -= 1f;
                            checkFirst = false;
                            isAction = false;
                           
                        }
                        else
                        {
                            rig.isKinematic = false;
                            _isRun = true;

                            isAction = false;
                            checkFirst = false;
                            if (checkLonvong)
                            {
                                if (Time.time - timeTemp > .7f)
                                    anim.Play("LandRoll", -1, 0);
                                else
                                    anim.Play("Run");
                            }
                            else
                                anim.Play("Run");
                            
                            checkLonvong = false;

                        }
                        wallRunLeft = false;
                        wallRunRight = false;
                        rig.drag = 0;
                        rig.mass = 1;
                        if (reduceVelocity)
                        {
                            reduceVelocity = false;
                            tempReduce = 0;
                        }
                    }
                    else if (!isAction)
                        CheckIsJump();
                }
                else
                    checkFirst = true;
                
                if (IsFrontUp() )
                    StartClimbing();
                
            }
            else
            {
                if (!endLeoTuong)
                {
                    if (!_isPowerUp)
                        transform.Translate(Vector3.up * speedLeoTuong * Time.deltaTime);

                    if (!checkAnimPlay("WallClimbing"))
                    {
                        anim.Play("WallClimbing", -1, 0);
                    }
                }

                ClimbEnd();

            }
        }
        if (rig.velocity.y < -4f)
            rig.velocity = new Vector3(rig.velocity.x, -4f, rig.velocity.z);
    }
    public bool checkFirst;
    public bool isAction;

    public void ClimbEnd()
    {
        if (!IsFrontUp())
        {
            if (!checkAnimPlay("WallClimbingEnd"))
            {
                lockMove = true;
                anim.Play("WallClimbingEnd", -1, 0);
                endLeoTuong = true;
                _isRun = false;
                checkFirst = false;
                transform.DOMoveY(transform.position.y + .12f, .2f);
                transform.DOMoveZ(transform.position.z + .02f, .2f);
            }
        }
        
    }

    void StartClimbing()
    {
        rig.velocity = Vector3.zero;
        _isRun = false;
        leoTuong = true;
        endLeoTuong = false;
        rig.isKinematic = true;
        anim.Play("WallClimbing", -1, 0);
        isAction = true;
        wallRunLeft = false;
        wallRunRight = false;
        checkLonvong = false;
        if (hasJumped)
        {
            hasJumped = false;
            tempPower -= 1f;
        }
        if (reduceVelocity)
            rig.drag = 0;
    }
    
    void CheckIsJump()
    {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(posCheckJump.position - originJump.position);
        if (!Physics.Raycast(originJump.position, direction, out hit, distanceJump, layerGround))
        {
            Jump();
            ManagerEffect.Instance.EffectJump();
        }
       
    }

    // bool checkWallRight()
    // {
    //     Vector3 direction = checkRight.position - originJump.position;
    //     if (Physics.Raycast(originJump.position, direction, 0.12f, layerGround))
    //         return true;
    //     else
    //         return false;
    // }
    // bool checkWallLeft()
    // {
    //     Vector3 direction = checkLeft.position - originJump.position;
    //     if (Physics.Raycast(originJump.position, direction, 0.12f, layerGround))
    //         return true;
    //     else
    //         return false;
    // }
    public bool checkAnimPlay(string nameAnim)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(nameAnim);
    }
    #region anim player
    // public void Jump()
    // {
    //     if (!isAction)
    //     {
    //         SoundManager.Instance.PlaySoundNhay();
    //         isAction = true;
    //         anim.SetInteger(AnimParameter.jump, Random.Range(1, 4));
    //         rig.velocity = Vector3.zero;
    //         rig.AddForce(0, heigh_Jump, 0, ForceMode.Impulse);
    //         ManagerEffect.Instance.OffMoveSmoke();
    //         timeTemp = Time.time;
    //     }
    // }
    public void Climb(int idType)
    {
        rig.velocity = Vector3.zero;
        _isRun = false;
        isAction = true;
        lockMove = true;
        anim.SetInteger(AnimParameter.wallclimb, idType);
        ManagerEffect.Instance.OffMoveSmoke();
    }
    public void Slide()
    {
        _isSliding = true;

        isAction = true;
        anim.SetTrigger(AnimParameter.slide);
    }
    public void Die()
    {
        checkLonvong = false;
        anim.Play("Die", -1, 0);
        rig.drag = 0;
        rig.velocity = Vector3.zero;
        transform.DOMoveZ(transform.position.z - .3f, .5f);
        checkFirst = false;
        lockMove = true;

        _isLive = false;

        Invoke("DelayReSpawn", 1f);
    }
    void DelayReSpawn()
    {
        lockMove = false;

        _isLive = true;
        transform.position = posCheckPoint;
        ManagerEffect.Instance.ShowFxUpgrade();
        isAction = false;
        _isRun = true;
        if (hasJumped)
        {
            hasJumped = false;
            tempPower -= 1f;
        }
        if (reduceVelocity)
        {
            reduceVelocity = false;
            tempReduce = 0;
        }
        wallRunLeft = false;
        wallRunRight = false;
        leoTuong = false;
        anim.Play("Run");
        rig.isKinematic = false;

        SoundManager.Instance.PlaySoundHoiSinh();
    }
    public bool isWin;
    public void Win()
    {
        rig.isKinematic = true;
        _isRun = false;
        _isLive = false;
        isWin = true;
        lockMove = true;
        //Jump();
        txtText.SetActive(false);
        this.PostEvent(EventID.OffText);
        this.PostEvent(EventID.PauseAI);
        Invoke("DelaySlow", 2.4f);
        GameManager.instance.GameOver();

        TestCamera.Instance.camWin();
        TestCamera.Instance.DontCameraFollow();
        Camera.main.GetComponent<CinemachineBrain>().enabled = false;
        TestCamera.Instance.bg.SetActive(false);
        Camera.main.orthographic = false;
        Camera.main.fieldOfView = 60f;

        Camera.main.transform.DORotate(new Vector3(12, -120, 0), 2f).SetEase(Ease.InQuad);
        
        if (GameManager.Instance.playerPos == 1)
        {
            anim.SetTrigger("win");
        }
        else
        {
            if (Random.Range(0, 2) == 0)
                anim.Play("lose1", -1, 0);
            else
                anim.Play("lose2", -1, 0);
        }

        checkLonvong = false;
        UIController.Instance.btnPowerUp.gameObject.SetActive(false);
    }
    void DelaySlow()
    {
        SlowMotion.Instance.SlowNoAudio(2f, .3f);
        blackScreenCamera.SetActive(true);
    }
    // public void NhayVuotRao()
    // {
    //     SoundManager.Instance.PlaySoundNhay();
    //     isAction = true;
    //     //EatItemPower(0.5f);
    //     anim.SetInteger(AnimParameter.vuotrao, 1);
    //     rig.velocity = Vector3.zero;
    //     rig.AddForce(new Vector3(0f, heigh_Jump, 0), ForceMode.Impulse);
    //     ManagerEffect.Instance.EffectJump2();
    //     ManagerEffect.Instance.EffectJump();
    //     ManagerEffect.Instance.OffMoveSmoke();
    //     ManagerEffect.Instance.ShowFxSongAm(transform.position);
    //     if (!reduceVelocity)
    //     {
    //         reduceVelocity = true;
    //         tempReduce = -speed / 3;
    //     }
    // }
    // public void Nhayxa()
    // {
    //     ManagerEffect.Instance.EffectJump2();
    //     ManagerEffect.Instance.EffectJump();
    //     SoundManager.Instance.PlaySoundNhay();
    //
    //     isAction = true;
    //     anim.SetTrigger(AnimParameter.nhayxa);
    //     rig.velocity = Vector3.zero;
    //     rig.AddForce(new Vector3(0f, /*heigh_Jump*/4, 0), ForceMode.Impulse);
    //     ManagerEffect.Instance.OffMoveSmoke();
    //     ManagerEffect.Instance.ShowFxSongAm(transform.position);
    //     if (!reduceVelocity)
    //     {
    //         reduceVelocity = true;
    //         tempReduce = -speed / 3;
    //     }
    // }
    bool hasJumped;
    public void Jump() // high Jump Function
    {
        isAction = true;

        if (Random.Range(0, 2) == 0)
            anim.Play("JumpRoll", -1, 0);
        else
            anim.Play("JumpRoll2", -1, 0);
        hasJumped = true;
        rig.velocity = Vector3.zero;
        tempPower += 1f;
        rig.AddForce(0, 4, 0, ForceMode.Impulse);

    }
    Vector3 posCheckPoint;
    public void CheckPoint(Vector3 target)
    {
        posCheckPoint = target;
    }
    #endregion
}