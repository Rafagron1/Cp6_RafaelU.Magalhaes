
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject ataque;
    private Vector2 direction;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Ataque();
    }
    void FixedUpdate()
    {
        OnMove();
    }
    private void Move()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void OnMove()
    {
        rb.linearVelocity = direction.normalized * speed;
    }
    private void Ataque()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Atacando());
        }
    }
    IEnumerator Atacando()
    {
        ataque.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ataque.gameObject.SetActive(false);
    }
}
