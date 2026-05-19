using UnityEngine;

[RequireComponent(typeof(FlyEnemyAI))]
public class FlyShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    private FlyEnemyAI _ai;

    void OnEnable()
    {
        _ai = GetComponent<FlyEnemyAI>();
        _ai.Fire += Shoot; // se subscribe al evento Fire
    }

    void OnDisable()
    {
        if (_ai != null) _ai.Fire -= Shoot; // se desuscribe (importante para evitar memory leaks)
    }

    private void Shoot()
    {
        if (_ai.Player == null || bulletPrefab == null) return;

        Vector2 dir = (_ai.Player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}
