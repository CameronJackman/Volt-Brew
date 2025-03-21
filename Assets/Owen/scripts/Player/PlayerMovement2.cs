using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{

    [SerializeField] private RobotScript _rs;

    public float moveSpeed = 1.5f;
    

    public float acceleration = 11f;   
    public float deceleration = 15f;

    //Dash Settings
    public float dashDuration = 10f;
    public float dashSpeedMultiplier = 3f;
    public float dashCoolDownTime = 1.5f;

    [HideInInspector] public float dashCoolDownTimer = 0f;

    public bool isoToggle = false;
    private Vector2 isoRight = new Vector2(-1.75f, 1.0f);
    private Vector2 isoUp = new Vector2(1.75f, 1.0f);

    private Rigidbody2D rb;

    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 rawInput = Vector2.zero;

    
    public bool isDashing = false;
    private bool spawnLeft = true;

    private PlayerDefault _PD;
    private System.Action<InputAction.CallbackContext> _onAimWithController;
    private bool _usingMouseInput = true;
    private PlayerInput _playerInput;
    private string _currentControlScheme;
    public string _mouseInputName = "MouseKey";
    public string _controllerInputName = "Controller";

    private GameManager gameManager;

    public GameObject shieldBreakParticle;
    public Transform particlePoint;
    private bool shieldCoroutineFinished;


    [SerializeField]
    private SpriteRenderer playerGFX;

    //Projectile shield variables
    public GameObject shieldPrefab;
    private GameObject activeProjectileShield;

    [HideInInspector] public bool isProjectileShieldOwned;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        _PD = new PlayerDefault();
        _rs = GetComponent<RobotScript>();
        Transform rotatePoint = transform.Find("RotatePoint");
        _rs = rotatePoint.GetComponent<RobotScript>();
        _onAimWithController = ctx => _rs.AimAtScreenPosition(ctx.ReadValue<Vector2>());
        gameManager = FindAnyObjectByType<GameManager>();

    }

  

    public void UpdateCurrentControlScheme()
    {
        if (_playerInput != null)
        {
            _currentControlScheme = _playerInput.currentControlScheme;

            if (_mouseInputName == _currentControlScheme)
            {
                _usingMouseInput = true;
                Debug.Log("Using Mouse Input");
            }
            else
            {
                _usingMouseInput = false;
                Debug.Log("Using Controller input");
            }
        }
    }

    void Update()
    {
        //shield
        if (Input.GetKeyDown(KeyCode.C) && activeProjectileShield == null && isProjectileShieldOwned == true)
        {
            ActivateProjectileShield();
        }

        if (shieldCoroutineFinished)
        {
            BreakShield();
        }


        if (dashCoolDownTimer > 0)
        {
            dashCoolDownTimer -= Time.deltaTime;
        }

    }

    void MovePlayer(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        float horizontal = input.x;
        float vertical = input.y;
        rawInput = new Vector2(horizontal, vertical).normalized;

        if (horizontal >= 1)
        {
            playerGFX.flipX = true;
        }
        else if (horizontal <= -1)
        {
            playerGFX.flipX = false;
        }
    }

   

    void FixedUpdate()
    {

        if (isDashing) return;

        if (isoToggle)
        {
            Vector2 desiredMove = rawInput.x * -isoRight + rawInput.y * isoUp;


            Vector2 targetVelocity = (desiredMove * moveSpeed);



            if (desiredMove != Vector2.zero)
            {

                currentVelocity = Vector2.MoveTowards(
                    currentVelocity,
                    targetVelocity,
                    acceleration * Time.fixedDeltaTime
                );
            }
            else
            {

                currentVelocity = Vector2.MoveTowards(
                    currentVelocity,
                    Vector2.zero,
                    deceleration * Time.fixedDeltaTime
                );
            }


            rb.velocity = currentVelocity;
        }
        else
        {
            Vector2 desiredMove = new Vector2 (rawInput.x * 2 , rawInput.y * 2);


            Vector2 targetVelocity = desiredMove * moveSpeed;



            if (desiredMove != Vector2.zero)
            {

                currentVelocity = Vector2.MoveTowards(
                    currentVelocity,
                    targetVelocity,
                    acceleration * Time.fixedDeltaTime
                );
            }
            else
            {

                currentVelocity = Vector2.MoveTowards(
                    currentVelocity,
                    Vector2.zero,
                    deceleration * Time.fixedDeltaTime
                );
            }


            rb.velocity = currentVelocity;
        }

        
        
    }



    private void OnDashPerformed(InputAction.CallbackContext ctx)
    {
        if (dashCoolDownTimer <= 0f && !isDashing)
        {
            StartCoroutine(PerformDash());
        }
    }
    private void OnAimWithMouse(InputAction.CallbackContext ctx)
    {
        // This is for actual mouse SCREEN coordinates
        _rs.AimAtScreenPosition(ctx.ReadValue<Vector2>());
    }

    

    private void OnFiring(InputAction.CallbackContext ctx)
    {
        _rs.Firing();
    }
    private void OnEnable()
    {
        _PD.Enable();

        _PD.DefaultMovement.Movement.performed += MovePlayer;
        _PD.DefaultMovement.Movement.canceled += MovePlayer;

        _PD.DefaultMovement.Dash.performed += OnDashPerformed;

        _PD.DefaultMovement.Aiming.performed += OnAimWithMouse;
        _PD.DefaultMovement.Aiming.canceled += OnAimWithMouse;

        _PD.DefaultMovement.AimingController.performed += OnAimWithController;
        _PD.DefaultMovement.AimingController.canceled += OnAimWithController;

        _PD.DefaultMovement.firing.started += OnFiring;
    }
    private void OnDisable()
    {
        _PD.Disable();

        _PD.DefaultMovement.Movement.performed -= MovePlayer;
        _PD.DefaultMovement.Movement.canceled -= MovePlayer;

        _PD.DefaultMovement.Dash.performed -= OnDashPerformed;

        _PD.DefaultMovement.Aiming.performed -= OnAimWithMouse;
        _PD.DefaultMovement.Aiming.canceled -= OnAimWithMouse;

        _PD.DefaultMovement.Aiming.performed -= OnAimWithController;
        _PD.DefaultMovement.Aiming.canceled -= OnAimWithController;

        _PD.DefaultMovement.firing.started -= OnFiring;

    }

    private void OnAimWithController(InputAction.CallbackContext ctx)
    {
        _rs.AimWithController(ctx.ReadValue<Vector2>());
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;

        Vector2 originalVelocity = rb.velocity;
        rb.velocity = originalVelocity * dashSpeedMultiplier;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = originalVelocity;

        dashCoolDownTimer = dashCoolDownTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("leftTrig"))
        {
            spawnLeft = true;
        }

        if (collision.CompareTag("rightTrig"))
        {
            spawnLeft = false;
        }

        if (collision.CompareTag("Coin"))
        {
            gameManager.coins++;
            Destroy(collision.gameObject);
        }
    }

    public bool spawnLocationCheck()
    {
        return spawnLeft;
    }

    public void setLocation(GameObject targetLocation)
    {
        gameObject.transform.position = targetLocation.transform.position;
    }







    //Shield Stuff

    //Activates projectile shield
    public void ActivateProjectileShield()
    {
        activeProjectileShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        //shieldBreak = Instantiate(shieldBreak, transform.position, Quaternion.identity);

        //Set proper scale of shield 
        activeProjectileShield.transform.localScale = Vector3.one * 0.4f;

        //Start coroutine to follow player
        StartCoroutine(FollowPlayerForSeconds(5f));

        //if (shieldCoroutineFinished)
        //{
        //    BreakShield();
        //}
    }

    //Makes the projectile shield follow the player 
    IEnumerator FollowPlayerForSeconds(float seconds)
    {
        float timer = 0f;
        while (timer < seconds)
        {
            if (activeProjectileShield != null)
            {
                //Keeps shield on player
                activeProjectileShield.transform.position = gameObject.transform.position;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if (activeProjectileShield != null)
        {
            Destroy(activeProjectileShield);

            Debug.Log("shield broke");

            shieldCoroutineFinished = false;

        }
        shieldCoroutineFinished = true;
    }

    public void BreakShield()
    {
        if (particlePoint != null && shieldBreakParticle != null)
        {
            GameObject shieldBreak = Instantiate(shieldBreakParticle, particlePoint.position, particlePoint.rotation, particlePoint);
            //Destroy(shieldBreak);
            Debug.Log("Shield particlessss");
        }
    }
}

