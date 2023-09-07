using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class PlayerController : MonoBehaviourSingleton<PlayerController>
{
    public GameObject blackScreenCamera;
    public bool _isLive;
    public bool _isRun;
    public bool _isGround;
    public bool _isSliding;
    public bool _isPowerUp;
    public float speed;
    [SerializeField] private float m_JumpFoce = 2f;
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
    public Transform JumpLeft;
    public Transform JumpRight;
    public Transform checkRight;
    public Transform checkLeft;
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
    public float defaultspeed;

    public int HitCount = 0;

    [Header("PlayeSkins")] [SerializeField]
    private List<GameObject> Characters;
    private void Start()
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_GameStart);

        Characters[SaveLoadData.m_data.LastSelectedCharacter].SetActive(true);
        anim = Characters[SaveLoadData.m_data.LastSelectedCharacter].GetComponent<Animator>();
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        defaultspeed = speed;
        JumpAction();
        //Slide();
        setIdle();
        batXa = false;
        capsuleCollider = GetComponent<CapsuleCollider>();
        posCheckPoint = transform.position;
    }

    public void setdefaultSpeed()
    {
        rig.isKinematic = false;
        speed = defaultspeed;
    }
    void setIdle()
    {
        anim.Play("Idle",-1,0);
    }
    public void SetValuePlayer(bool checkFirebase)
    {
        speed = /*GameManager.instance.speed_Player +*/1.6f + PlayerprefSave.speedUpgrade;
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
        // coroutinePower = StartCoroutine(UpValuePower());
        Run();
        checkFirst = true;
        _isGround = IsGrounded();
        _isRun = true;
    }

    #region Power Up
    /*IEnumerator UpValuePower()
    {
        yield return new WaitForSeconds(2f);
        valuePowerUp += .1f;
        if (valuePowerUp < maxPower)
        {
            coroutinePower = StartCoroutine(UpValuePower());
        }
        else
        {
            valuePowerUp = maxPower;
            if (!isWin && !fullBooster)
            {
                UIController.Instance.ReadyBooster();
                fullBooster = true;
            }
        }
        UIController.Instance.fillPower.DOFillAmount(valuePowerUp / maxPower, .2f).SetEase(Ease.Linear);
    }*/
    /*public void EatItemPower(float value)
    {
        valuePowerUp += value;
        if (valuePowerUp >= maxPower)
        {
            valuePowerUp = maxPower;
            if (!isWin && !fullBooster)
            {
                UIController.Instance.ReadyBooster();
                fullBooster = true;
                UIController.Instance.fillPower.DOFillAmount(valuePowerUp / maxPower, .2f).SetEase(Ease.Linear);
            }
        }
        else
        {
            UIController.Instance.fillPower.DOFillAmount(valuePowerUp / maxPower, .2f).SetEase(Ease.Linear);
        }

    }*/
    /*public void UsePowerUp()
    {
        if (!_isPowerUp)
            if (valuePowerUp > 0)
            {
                anim.SetFloat("changeStatus", 1);
                anim.SetFloat("speedBooster", 2);
                TestCamera.Instance.CamBooster();
                ManagerEffect.Instance.OnPowerUp();
                tempPower += speed_Booster;
                if (coroutinePower != null)
                    StopCoroutine(coroutinePower);
                _isPowerUp = true;
                UIController.Instance.fillPower.DOFillAmount(0, time_Booster).SetEase(Ease.Linear).OnComplete(() =>
                {
                    fullBooster = false;
                    anim.SetFloat("speedBooster", 1);
                    anim.SetFloat("changeStatus", 0);
                    _isPowerUp = false;
                    valuePowerUp = 0;
                    tempPower -= speed_Booster;
                    coroutinePower = StartCoroutine(UpValuePower());
                    ManagerEffect.Instance.OffPowerUp();
                    TestCamera.Instance.CamNormal();
                    UIController.Instance.disbleffPower();
                });
                if (reduceVelocity)
                {
                    reduceVelocity = false;
                    tempReduce = 0;
                }
            }
    }*/
    #endregion
    public bool IsGrounded()
    {
        return Physics.CheckCapsule(capsuleCollider.bounds.center, new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.min.y, capsuleCollider.bounds.center.z),
            capsuleCollider.radius / 3, layerGround);
    }
    //check truoc mat
    public bool IsFrontUp()
    {
        /*turn Physics.CheckCapsule(new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.max.z),
            new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.center.y, capsuleCollider.bounds.max.z), capsuleCollider.radius / 3, layerGround);
    */
        RaycastHit hit;
        //print(Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.forward, out hit, 0.09f, layerGround));
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector3.forward, Color.red);
        return Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.forward, out hit, 0.1f, layerGround);
    }

    public void CheckClimb()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, capsuleCollider.radius / 3, layerGround))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
    //check truoc mat ben trai
    public bool IsFrontUpLeft()
    {
        return Physics.CheckCapsule(new Vector3(capsuleCollider.bounds.min.x, capsuleCollider.bounds.max.y, capsuleCollider.bounds.max.z),
            new Vector3(capsuleCollider.bounds.min.x, capsuleCollider.bounds.max.y, capsuleCollider.bounds.max.z), capsuleCollider.radius, layerGround);
    }
    //check truoc mat ben phai
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
                    else if (!wallRunRight && !wallRunLeft)
                    {
                        if (checkAnimPlay("Run"))
                        {
                            if (checkWallLeft() && variableJoystick.Horizontal < 0)
                                rig.velocity = new Vector3(0, rig.velocity.y, rig.velocity.z);
                            else if (checkWallRight() && variableJoystick.Horizontal > 0)
                                rig.velocity = new Vector3(0, rig.velocity.y, rig.velocity.z);
                            else
                                rig.velocity = new Vector3(variableJoystick.Horizontal * speedMoveX, rig.velocity.y, rig.velocity.z);
                        }
                        else
                            rig.velocity = new Vector3(variableJoystick.Horizontal * speedMoveX, rig.velocity.y, rig.velocity.z);
                    }
                }
                else
                {
                    Vector3 direction = Vector3.right * variableJoystick.Horizontal;
                    if (checkWallLeft() && variableJoystick.Horizontal < 0)
                        transform.Translate(direction * 0 * Time.deltaTime);
                    else if (checkWallRight() && variableJoystick.Horizontal > 0)
                        transform.Translate(direction * 0 * Time.deltaTime);
                    else
                        transform.Translate(direction * speedMoveX * Time.deltaTime);
                }
            }
            else
            {
                transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, 0), .5f);
            }
            if (_isRun)
            {
                //if (batXa)
                //    rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y, rig.velocity.z);
                //else
                rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y, speed + tempPower + tempReduce);
                //transform.Translate(Vector3.forward * (speed + tempPower) * Time.deltaTime);
            }
        }
    }

    public bool m_LandRoll = false; 
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
                        if (m_LandRoll)
                        {
                            anim.Play("LandRoll", -1, 0);
                            m_LandRoll = false;
                        }
                        
                        if (batXa)
                        {
                            print("come here");
                            
                            
                            batXa = false;
                            tempPower -= 1f;
//                            TestCamera.Instance.CameraShake();
                            checkFirst = false;
                            isAction = false;
                            // SoundManager.Instance.PlaySoundLonVongTiepDat();
                        }
                        else
                        {
                            rig.isKinematic = false;
                            _isRun = true;
                            //Debug.Log("Lan dau cham dat");
                            isAction = false;
                            checkFirst = false;
                            if (checkLonvong)
                            {
                                if (Time.time - timeTemp > 1.3f)
                                {
                                    anim.Play("LandRoll", -1, 0);
                                    //SoundManager.Instance.PlaySoundLonVongTiepDat();
                                }
                                else
                                    Run();
                            }
                            else
                            {
                                Run();
                            }
                            checkLonvong = false;
                            /*ManagerEffect.Instance.EffectTiepDat();
                            ManagerEffect.Instance.OnMoveSmoke();*/
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
                        //anim.SetTrigger("run");
                    }
                    else
                    {
                        /*if (!isAction)
                            RaycastCheckLeft();
                        if (!isAction)
                            RaycastCheckRight();*/
                        if (!isAction)
                            CheckIsJump();
                    }
                }
                else
                {
                    checkFirst = true;

                    if (!isAction)
                    {
                        anim.Play("Fall", -1, 0);
                        _isRun = false;
                    }
                    
                    //chay tren tuong
                    
                }
                if (IsFrontUp() /*&& !wallRunRight*/)
                {
                    StartClimbing();
                }

                //CheckClimb();
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
            if (!checkAnimPlay("WallClimbingEnd0")/* && !checkAnimPlay("WallClimbingEnd0")*/)
            {
                lockMove = true;
                anim.Play("WallClimbingEnd0", -1, 0);
                //leoTuong = false;
                endLeoTuong = true;
                _isRun = false;
                checkFirst = false;
                transform.DOMoveY(transform.position.y + .51f, .1f).SetEase(Ease.InQuad);
                transform.DOMoveZ(transform.position.z + .1f, .2f);
            }
        }
        
    }

    void StartClimbing()
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_Climb);

        rig.velocity = Vector3.zero;
        //Debug.Log("leo tuong");
        _isRun = false;
        leoTuong = true;
        endLeoTuong = false;
        rig.isKinematic = true;
        anim.Play("WallClimbing", -1, 0);
        isAction = true;
        wallRunLeft = false;
        wallRunRight = false;
        checkLonvong = false;
        if (batXa)
        {
            batXa = false;
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
            //Debug.Log("Jump");
            //Jump();
            //ManagerEffect.Instance.EffectJump();
        }
        //if (!Physics.CheckCapsule(capsuleCollider.bounds.min,
        //     new Vector3(capsuleCollider.bounds.center.x, capsuleCollider.bounds.min.y, capsuleCollider.bounds.max.z), capsuleCollider.radius, layerGround))
        //{
        //    Jump();
        //    ManagerEffect.Instance.EffectJump();
        //}
    }
    bool checkWallRight()
    {
        Vector3 direction = checkRight.position - originJump.position;
        if (Physics.Raycast(originJump.position, direction, 0.12f, layerGround))
            return true;
        else
            return false;
    }
    bool checkWallLeft()
    {
        Vector3 direction = checkLeft.position - originJump.position;
        if (Physics.Raycast(originJump.position, direction, 0.12f, layerGround))
            return true;
        else
            return false;
    }
    public bool checkAnimPlay(string nameAnim)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(nameAnim);
    }
    #region anim player
    public void Jump()
    {
        if (!isAction)
        {
            SoundManager.Instance.PlaySoundNhay();
            isAction = true;
            anim.SetInteger(AnimParameter.jump, Random.Range(1, 4));
            rig.velocity = Vector3.zero;
            rig.AddForce(0, heigh_Jump, 0, ForceMode.Impulse);
            ManagerEffect.Instance.OffMoveSmoke();
            timeTemp = Time.time;
            //checkLonvong = true;
        }
    }
    public void Climb(int idType)
    {
        //if (!isAction)
        //{
        rig.velocity = Vector3.zero;
        _isRun = false;
        isAction = true;
        lockMove = true;
        anim.SetInteger(AnimParameter.wallclimb, idType);
        ManagerEffect.Instance.OffMoveSmoke();
        // }
    }

    public void Run()
    {
        rig.isKinematic = false;
        anim.Play("Run");
    }
    public void Slide()
    {
        if (checkAnimPlay("WallClimbingEnd0")  || checkAnimPlay("WallClimbing") || checkAnimPlay("JumpRoll") || checkAnimPlay("JumpRoll3") || checkAnimPlay("JumpRoll2") || checkAnimPlay("Slide"))
        {
            return;
        }
        //if (!isAction)
        //{
        _isSliding = true;
        // SoundManager.Instance.PlaySoundSlide();
        //EatItemPower(0.5f);
        isAction = true;
        anim.SetTrigger(AnimParameter.slide);
        /*ManagerEffect.Instance.OffMoveSmoke();
        ManagerEffect.Instance.ShowFxSongAm(transform.position);
        ManagerEffect.Instance.EffectJump2();*/
        //}
    }
    public void Die()
    {
        GameManager.Instance.touchManager.SetActive(false);
        checkLonvong = false;
        anim.Play("Die", -1, 0);
        rig.drag = 0;
        rig.velocity = Vector3.zero;
        transform.DOMoveZ(transform.position.z - .3f, .5f);
        checkFirst = false;
        lockMove = true;
//        TestCamera.Instance.DontCameraFollow();
        _isLive = false;
        //anim.SetInteger(AnimParameter.wallclimb, 0);
        //this.PostEvent(EventID.PauseAI);
        //Invoke("DelayReSpawn", 1f);
        Lose();
    }
    void DelayReSpawn()
    {
        lockMove = false;
//        TestCamera.Instance.CameraFollow();
        _isLive = true;
        transform.position = posCheckPoint;
        ManagerEffect.Instance.ShowFxUpgrade();
        isAction = false;
        _isRun = true;
        if (batXa)
        {
            batXa = false;
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
        //this.PostEvent(EventID.ContinueAI);
        SoundManager.Instance.PlaySoundHoiSinh();
    }
    public bool isWin;

    public void Win()
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_GameWon);
        rig.isKinematic = true;
        _isRun = false;
        _isLive = false;
        isWin = true;
        lockMove = true;

        Invoke("DelaySlow", 2.4f);

        GameManager.instance.GameOver();
        TestCamera.Instance.camWin();
        transform.DORotate(new Vector3(0, 90, 0), 0.3f);
        //Camera.main.transform.DORotate(new Vector3(12, -120, 0), 2f).SetEase(Ease.InQuad);
        //Camera.main.transform.DOPath(PlayerTrigger.Instance.posPath, 2f, PathType.CatmullRom);
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

        /*});*/
        checkLonvong = false;
        rig.isKinematic = false;

    }
    
    public void Lose()
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_GameLose);

        FirebaseEvents.logEvent("Level Fail " + (LevelSelection.m_LevelNum+1));
        rig.isKinematic = true;
        _isRun = false;
        _isLive = false;
        isWin = true;
        lockMove = true;

        checkLonvong = false;
        
        UIController.Instance.LosingPanel();
    }

    void DelaySlow()
    {
        SlowMotion.Instance.SlowNoAudio(2f, .3f);
        AdHandler.ShowInterstitial();
        AdHandler.ShowRectBanner();
        blackScreenCamera.SetActive(true);
    }
    public void NhayVuotRao()
    {
        SoundManager.Instance.PlaySoundNhay();
        isAction = true;
        //EatItemPower(0.5f);
        anim.SetInteger(AnimParameter.vuotrao, 1);
        rig.velocity = Vector3.zero;
        rig.AddForce(new Vector3(0f, heigh_Jump, 0), ForceMode.Impulse);
        ManagerEffect.Instance.EffectJump2();
        ManagerEffect.Instance.EffectJump();
        ManagerEffect.Instance.OffMoveSmoke();
        ManagerEffect.Instance.ShowFxSongAm(transform.position);
        if (!reduceVelocity)
        {
            reduceVelocity = true;
            tempReduce = -speed / 3;
        }
    }
    public void Nhayxa()
    {
        ManagerEffect.Instance.EffectJump2();
        ManagerEffect.Instance.EffectJump();
        SoundManager.Instance.PlaySoundNhay();
        //EatItemPower(0.5f);
        isAction = true;
        anim.SetTrigger(AnimParameter.nhayxa);
        rig.velocity = Vector3.zero;
        rig.AddForce(new Vector3(0f, /*heigh_Jump*/4, 0), ForceMode.Impulse);
        ManagerEffect.Instance.OffMoveSmoke();
        ManagerEffect.Instance.ShowFxSongAm(transform.position);
        if (!reduceVelocity)
        {
            reduceVelocity = true;
            tempReduce = -speed / 3;
        }
    }
    bool batXa;
    public void JumpAction() // high Jump Function
    {
        SoundHandler.Instence.playSound(SoundHandler.Instence.m_Jump);

        if (checkAnimPlay("WallClimbingEnd0") || checkAnimPlay("WallClimbing"))
        {
            return;
        }
        rig.isKinematic = false;
        // SoundManager.Instance.PlaySoundBatXa();
        //EatItemPower(0.5f);
        isAction = true;
        // checkFirst = true;
        int i = Random.Range(0, 3);
        if (i == 0)
            anim.Play("JumpRoll", -1, 0);
        else if(i == 1)
            anim.Play("JumpRoll2", -1, 0);
        else
        {
            anim.Play("JumpRoll3", -1, 0);
        }
        batXa = true;
        rig.velocity = Vector3.zero;
        tempPower += 1f;
        rig.AddForce(1, m_JumpFoce, 0, ForceMode.Impulse);
        // ManagerEffect.Instance.OffMoveSmoke();
        // ManagerEffect.Instance.ShowFxBatXa();
        // ManagerEffect.Instance.ShowFxSongAm(transform.position);
    }
    Vector3 posCheckPoint;
    public void CheckPoint(Vector3 target)
    {
        posCheckPoint = target;
    }
    #endregion
}