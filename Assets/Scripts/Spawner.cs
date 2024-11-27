using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject Bomb;
    public float Spawn_Rate = 3;
    bool CanSpawn = true;
    public float Timer_Global;
    public float Timer = 0;
    public float interval = 10f;
    public float gravity = 2;
    public TMP_Text Time_Text;
    public TMP_Text Mode;

    public bool StartTimer = true;
    public Player player;

    void Update()
    {
        if (CanSpawn)
        {
            StartCoroutine(Spawn_Bomb());
        }
        if (StartTimer)
        {
            Timer_Global += 1 * Time.deltaTime;
            DisplayTime(Timer_Global);
        }
        

        Timer += 1 * Time.deltaTime;
        

        if (Timer >= interval)
        {
            Timer = 0;
            if(Spawn_Rate >= 0.5f)
            {
                Spawn_Rate -= 0.1f;
            }
            if(gravity <= 5)
            {
                gravity += 0.1f;
            }
            if(player.Speed <= 7)
            {
                player.Speed += 0.05f;
            }
        }

        if(Spawn_Rate <= 3 && Spawn_Rate > 2.5f)
        {
            Mode.text = "Easy";
        }
        else if(Spawn_Rate <= 2.5f && Spawn_Rate > 2f)
        {
            Mode.text = "Normal";
        }
        else if (Spawn_Rate <= 2f && Spawn_Rate > 1.5f)
        {
            Mode.text = "Hard";
        }
        else if (Spawn_Rate <= 1.5f && Spawn_Rate > 1f)
        {
            Mode.text = "Very Hard";
        }
        else if (Spawn_Rate <= 1f && Spawn_Rate > 0.7f)
        {
            Mode.text = "Insane";
        }
        else if (Spawn_Rate <= 0.7f && Spawn_Rate > 0.5f)
        {
            Mode.text = "Impossible";
        }

    }

    void Spawn()
    {
        int r = Random.Range(0, SpawnPoints.Length);
        GameObject ob = Instantiate(Bomb, SpawnPoints[r].position, SpawnPoints[r].rotation);
        Rigidbody2D rb = ob.GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
    }

    IEnumerator Spawn_Bomb()
    {
        CanSpawn = false;
        yield return new WaitForSeconds(Spawn_Rate);
        Spawn();
        CanSpawn = true;
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        Time_Text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}
