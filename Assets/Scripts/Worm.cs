using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _bulletPrefab;
    [SerializeField] private Transform _currGun;
    [SerializeField] private WormHealth _wormHealth;

    public float walkSpeed = 1f;
    public float maxRelativeVelocity = 6f;
    public float misileForce = 5f;

    public int wormID;

    private SpriteRenderer _spriteRenderer;
    private Camera _maincam;


    public bool IsTurn { get { return WormManager.Instance.IsMyTurn(wormID); } }

    private Vector3 diff;


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _maincam = Camera.main;
    }
    void Update()
    {
        if (!IsTurn)
            return;

        RotateGun();
        MovementAndShooting();
    }

    private void RotateGun()
    {
        diff = _maincam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _currGun.rotation = Quaternion.Euler(0, 0, rot_z + 180f);
    }

    private void MovementAndShooting()
    {
        float hor = Input.GetAxis("Horizontal");

        if (hor == 0)
        {
            _currGun.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.Space))
            {
                Rigidbody2D bullet = Instantiate(_bulletPrefab,
                    _currGun.position - _currGun.right,
                    _currGun.rotation);
                bullet.AddForce(-_currGun.right * misileForce, ForceMode2D.Impulse);

                if (IsTurn)
                {
                    WormManager.Instance.NextWorm();
                }
            }
        }
        else
        {
            _currGun.gameObject.SetActive(false);
            transform.position += Vector3.right * hor * Time.deltaTime * walkSpeed;
            _spriteRenderer.flipX = Input.GetAxis("Horizontal") > 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            _wormHealth.ChangeHealth(-10);

            if (IsTurn)
                WormManager.Instance.NextWorm();
        }
    }
}
