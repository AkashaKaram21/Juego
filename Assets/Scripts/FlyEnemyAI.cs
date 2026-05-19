using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyEnemyAI : MonoBehaviour
{
    enum EFly { Move, Attack }

    public Transform Player;
    private Rigidbody2D _rigidbody;
    public float speed = 3.0f;
    [SerializeField] Vector2 direction = new Vector2(1, 0.25f);
    [SerializeField] LayerMask Ground;

    public Transform UpPoint;
    public Transform LateralPoint;
    public Transform DownPoint;

    bool upHit, lateralHit, downHit;
    float attackDistance = 10.0f;
    float timerToShoot;
    public float radiusDetectWalls = 0.25f;

    // Evento al que se subscriben otros scripts (ej. FlyShoot)
    public Action Fire;

    FSM<EFly> brain;

    void Start()
    {
        InitSFM();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        brain.Update();
    }

    void InitSFM()
    {
        brain = new FSM<EFly>(EFly.Move);
        brain.SetOnEnter(EFly.Attack, () => timerToShoot = 0.0f);
        brain.SetOnStay(EFly.Move, MoveUpdate);
        brain.SetOnStay(EFly.Attack, AttackUpdate);
    }

   void MoveUpdate()
{
    _rigidbody.linearVelocity = direction * speed;

    // Girar sprite según dirección de patrulla
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    if (direction.x > 0)
        sr.flipX = false;
    else
        sr.flipX = true;

    if (DetectCollision())
    {
        ChangeDirection();
    }
    if (IsPlayerCloseByDistance(attackDistance))
    {
        brain.ChangeState(EFly.Attack);
    }
}

    void AttackUpdate()
{
    Vector2 dirAttack = Player.position - transform.position;
    dirAttack = dirAttack.normalized;
    _rigidbody.linearVelocity = dirAttack * speed;

    // Girar el sprite según la dirección
    if (dirAttack.x > 0)
        GetComponent<SpriteRenderer>().flipX = false;
    else
        GetComponent<SpriteRenderer>().flipX = true;

    timerToShoot += Time.deltaTime;
    if (timerToShoot >= 1.5f)
    {
        Fire?.Invoke();
        timerToShoot = 0.0f;
    }
    if (!IsPlayerCloseByDistance(attackDistance))
    {
        brain.ChangeState(EFly.Move);
    }
}

    bool DetectCollision()
    {
        upHit      = Physics2D.OverlapCircle(UpPoint.position,      radiusDetectWalls, Ground);
        lateralHit = Physics2D.OverlapCircle(LateralPoint.position, radiusDetectWalls, Ground);
        downHit    = Physics2D.OverlapCircle(DownPoint.position,    radiusDetectWalls, Ground);
        return upHit || lateralHit || downHit;
    }

    void ChangeDirection()
    {
        if (lateralHit)
        {
            transform.Rotate(0, 180, 0);
            direction.x = -direction.x;
        }
        if (upHit   && direction.y > 0) direction.y = -direction.y;
        if (downHit && direction.y < 0) direction.y = -direction.y;
    }

    private bool IsPlayerCloseByDistance(float distance)
    {
        float d = Vector2.Distance(transform.position, Player.position);
        return d < distance;
    }

    // Colisión directa con el jugador → reinicia la escena
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
