# ShootingGame
AlienShootingGame


 - Start Scene


![image](https://github.com/Yeonnnii/ShootingGame/assets/141755349/28d2d29f-1ed8-449b-900d-ba2e7ba96e06)

> Player의 name을 텍스트로 입력 받기 위해, Input FIeld UI를 사용.
>
> // Main game으로 불러오는 것은 구현 못함


![image](https://github.com/Yeonnnii/ShootingGame/assets/141755349/1c4cd82b-a998-41df-a1cb-1fed94fd844f)

> Start버튼(Join)으로 게임 화면으로 전환하게끔, Button 항목 추가하여 Script 작성 후, 객체 넣기
> 
> 생동감을 넣기 위해, Animation을 이미지에 넣고 움직이는 모습으로 구현



 - Main Scene


![image](https://github.com/Yeonnnii/ShootingGame/assets/141755349/e8fa9afd-74ae-4388-8f9f-c57fa1df72d1)
> Input Action 추가하여 키보드로 이동.
>
> 이동 거리
> 
    private CharacterController _controller;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovment(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovment(Vector2 direction)
    {
        direction = direction * 5;

        _rigidbody.velocity = direction;
    }


ㅡ

     private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);

    }

    public void OnLook(InputValue value)
    {

        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }
    public void OnFire(InputValue value)
    {
        Debug.Log("OnFire" + value.ToString());
    }

ㅡ

    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
        public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }


![image](https://github.com/Yeonnnii/ShootingGame/assets/141755349/f5a87f5f-702d-4ada-a0ab-1dcb432cf9a6)
> Tilemap으로 게임 틀 구현




> 충돌 확인
> 
> Player에 Rigidbody 2D, Box Collider 2D 추가
> 
> 타일맵 Collider2D 추가



> 시간 표시
> GameManager 추가하여 스크립 작성

    internal static object instance;
    public TMP_Text timeTxt;

    float alive = 0f;

    void Update()
    {
        alive += Time.deltaTime;
        timeTxt.text = alive.ToString("N2");
    }


> 플레이어 카메라 이동

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed=5.0f; // 카메라가 따라갈 속도
    private Vector3 targetPosition; // 대상의 현재 위치

    void Update()
    {
        // 대상이 있는지 체크
        if (target.gameObject != null)
        {
            // this는 카메라를 의미 (z값은 카메라값을 그대로 유지)
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            // vectorA -> B까지 T의 속도로 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }



> 마우스 포인트에 따른, 캐릭터 방향
>
> 좌우 대칭 및 무기 회전
> 
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;
    [SerializeField] private SpriteRenderer characterRenderer;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAimDirection)
    {
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = armRenderer.flipY;
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }



- 기존 Unity에 UI.Text 추가와 다른 점


UI Text가 없어서 TextMeshPro를 사용

Text를 사용하려면 using UnityEngine.UI; 지만,

엄연히 다른 것이라고 분류를 하는 것 같음


using TMPro;

public TMP_Text timeTxt;


