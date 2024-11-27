using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int index;

    Vector3 movment;

    public float Speed;
    
    public Animator anim;

    public int Health = 3;

    public bool Move = false;
    SpriteRenderer sr;

    public GameObject[] Hearts;
    public GameObject Heart_Lost;
    public Main main;

    public Spawner spawnrer;
    float Score;
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Score = PlayerPrefs.GetFloat("Score", Score);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        movment = new Vector3(x, 0, 0);
        if (Move)
        {
            transform.Translate(movment * Speed * Time.deltaTime);

            if (x == 1)
            {
                anim.SetTrigger("Run");
                transform.localScale = new Vector3(2, 2, transform.localScale.z);
            }
            if (x == 0)
            {
                anim.SetTrigger("Idle");
            }
            if (x == -1)
            {
                anim.SetTrigger("Run");
                transform.localScale = new Vector3(-2, 2, transform.localScale.z);
            }
        }
            
    }
    IEnumerator Damaged()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
    private void Get_Damaged()
    {
        Health -= 1;
        Instantiate(Heart_Lost, Hearts[Health].transform.position, Hearts[Health].transform.rotation);
        Hearts[Health].transform.localScale = new Vector3(0, 0, 1);
        StartCoroutine(Damaged());
        if(Health <= 0)
        {
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Run");
            anim.SetTrigger("Dead");
            Move = false;
            main.Dead();
            spawnrer.StartTimer = false;
            Score = spawnrer.Timer_Global;
            if(Score > PlayerPrefs.GetFloat("Score"))
            {
                PlayerPrefs.SetFloat("Score", Score);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bomb")
        {
            Get_Damaged();
        }
    }
}
