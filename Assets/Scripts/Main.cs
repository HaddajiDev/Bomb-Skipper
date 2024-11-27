using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Main : MonoBehaviour
{
    public Player player;
    public Spawner spawner;
    
    public CanvasGroup Main_BG;

    public RectTransform Logo;
    public RectTransform[] Hearts;
    public RectTransform Timer;

    public TMP_Text Score;
    public GameObject Score_Object;

    private void Update()
    {
        if(PlayerPrefs.GetFloat("Score") <= 0)
        {
            Score_Object.SetActive(false);
        }
        else
        {
            Score_Object.SetActive(true);
        }
        DisplayTime(PlayerPrefs.GetFloat("Score"));
        if(player.Move == false)
        {
            GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
            for (int i = 0; i < bombs.Length; i++)
            {
                Destroy(bombs[i]);
            }
        }
    }

    public void Play()
    {
        player.Move = true;
        spawner.enabled = true;
        spawner.StartTimer = true;
        spawner.Timer = 0;
        spawner.Timer_Global = 0;
        spawner.Spawn_Rate = 3;
        spawner.gravity = 2;
        player.Speed = 5;
        Logo.DOAnchorPos(new Vector2(0, 130), 0.5f).SetEase(Ease.InOutElastic);

        Main_BG.DOFade(0, 0.5f);
        Main_BG.interactable = false;

        Timer.DOAnchorPos(new Vector2(-156, -50), 0.5f).SetEase(Ease.InOutElastic);
        var squence = DOTween.Sequence();
        
        foreach (var button in Hearts)
        {
            squence.Append(button.transform.DOScale(1, 0.2f));
        }
    }

    public void Dead()
    {
        player.Move = false;
        spawner.enabled = false;
        spawner.StartTimer = false;
        
        StartCoroutine(Back_ToMain());
    }

    IEnumerator Back_ToMain()
    {
        yield return new WaitForSeconds(2f);
        
        player.Health = 3;        
        player.anim.SetTrigger("Idle");
        Logo.DOAnchorPos(new Vector2(0, -82), 0.5f).SetEase(Ease.InOutElastic);
        Main_BG.DOFade(1, 0.5f);
        Main_BG.interactable = true;

        Timer.DOAnchorPos(new Vector2(145, -50), 0.5f).SetEase(Ease.InOutElastic);
        
    }
    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        Score.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
