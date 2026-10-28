using System.Collections;
using UnityEngine;

public class MinotauroBehaviour : MonoBehaviour
{
    [SerializeField] GameObject alvo;
    [SerializeField] GameObject posIni;
    [SerializeField] private float distPerseguicao;
    [SerializeField] private float distAtaque;
    [SerializeField] private float speed;
    [SerializeField] private int vida;
    [SerializeField] private float coolDown;

    private Animator anim;
    private SpriteRenderer sR;
    private bool atacando = false;
    private bool caminhando = false;
    private bool ataqueAnim = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Retornar();
        Ataque();
        AnimManager();
    }
    void Move()
    {
        if (!atacando && Vector2.Distance(transform.position, alvo.transform.position)< distPerseguicao)
        {
            caminhando = true;
            transform.position = Vector2.MoveTowards(transform.position, alvo.transform.position, speed*Time.deltaTime);
            if (alvo.transform.position.x > transform.position.x)
            {
               sR.flipX = false;
            }
            else if (alvo.transform.position.x< transform.position.x)
            {
                sR.flipX = true;
            }
        }
    }
    void Retornar()
    {
        if (!atacando && Vector2.Distance(transform.position, alvo.transform.position) > distPerseguicao && Vector2.Distance(transform.position, posIni.transform.position) != 0)
        {
            caminhando = true;
            transform.position = Vector2.MoveTowards(transform.position, posIni.transform.position, speed * Time.deltaTime);
            if (posIni.transform.position.x > transform.position.x)
            {
                sR.flipX = false;
            }
            else if (posIni.transform.position.x < transform.position.x)
            {
                sR.flipX = true;
            }
        }
        else if (Vector2.Distance(transform.position, posIni.transform.position) == 0) caminhando = false;
    }
    void Ataque()
    {
        coolDown -= Time.deltaTime;
        if (Vector2.Distance(transform.position, alvo.transform.position) < distAtaque)
        {
            if (alvo.transform.position.x > transform.position.x)
            {
                sR.flipX = false;
            }
            if (alvo.transform.position.x < transform.position.x)
            {
                sR.flipX = true;
            }
            atacando = true;
            caminhando = false;
            if (coolDown <= 0)
            {
                StartCoroutine(Golpe());
            }
        }
        else atacando = false;
    }
    void AnimManager()
    {
        anim.SetBool("EstaAndando", caminhando);
        anim.SetBool("EstaAtacando", ataqueAnim);
    }
    private void OnTriggerEnter2D(Collider2D colision)
    {
        if(colision.tag == "Ataque")
        {
            StartCoroutine(Dano());
        }
    }
    private IEnumerator Golpe()
    {
        ataqueAnim = true;
        yield return new WaitForSeconds(0.5f);
        ataqueAnim = false;
        coolDown = 1.5f;
    }
    private IEnumerator Dano()
    {
        vida--;
        if (vida > 0)
        {
            sR.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            sR.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
