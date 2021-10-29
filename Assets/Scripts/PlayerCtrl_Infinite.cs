using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl_Infinite : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator playerAnim;
    Sound playerSound;
    private float jumpForce = 27f;
    private int cntCollision = 0;
    private int cntHeart = 3;
    public bool chkstate = false;
    private int score = 0;
    public Text scoreTxt;
    public Text totalScoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSound = GetComponent<Sound>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Player").transform.Find("Weapon").gameObject.SetActive(false);
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            GameObject.Find("Canvas").transform.Find("PauseUI").gameObject.SetActive(true);
        }
        if (cntHeart == 0)
        {
            Die();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Run();
        }
        else
        {
            Walk();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    void Walk()
    {
        GameObject.Find("Background").GetComponent<MapScroll>().scrollSpeed_BG = 0.25f;
        GameObject.Find("Ground").GetComponent<PlatformLoop_Infinite>().scrollSpeed_Map = 3.5f;
        playerAnim.SetBool("isRun", false);
    }
    
    void Run()
    {
        GameObject.Find("Background").GetComponent<MapScroll>().scrollSpeed_BG = 0.8f;
        GameObject.Find("Ground").GetComponent<PlatformLoop_Infinite>().scrollSpeed_Map = 10f;
        playerAnim.SetBool("isRun", true);
    }
    void Jump()
    {
        if (cntCollision == 1)
        {
            playerAnim.SetTrigger("isJump");
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerSound.SoundPlay(0);
        }
    }
    void Attack()
    {
        playerAnim.SetTrigger("isAttack");
        GameObject.Find("Player").transform.Find("Weapon").gameObject.SetActive(true);
        playerSound.SoundPlay(1);
    }
    void Hurt()
    {
        playerAnim.SetTrigger("isHurt");
        cntHeart--;
        HeartDisplay(cntHeart);
        playerSound.SoundPlay(2);
    }
    void Die()
    {
        playerAnim.SetTrigger("isDie");
        if (!chkstate)
        {
            GameObject.Find("Background").GetComponent<MapScroll>().scrollSpeed_BG = 0f;
            GameObject.Find("Ground").GetComponent<PlatformLoop_Infinite>().scrollSpeed_Map = 0f;
            playerSound.SoundPlay(3);
            chkstate = true;
            Invoke("AfterDie", 2f);
        }
    }
    void AfterDie()
    {
        playerSound.SoundPlay(6);
        totalScoreTxt.text = "Total Score : " + score.ToString();
        GameObject.Find("Canvas").transform.Find("GameOverUI").gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            cntCollision++;
        }
        if (collision.transform.tag == "Monster")
        {
            collision.gameObject.SetActive(false);
            Hurt();
            if (score > 0)
            {
                score -= 200;
                if (score < 0)
                {
                    score = 0;
                }
            }
            scoreTxt.text = score.ToString();
        }
        if(collision.transform.tag == "Obstacle")
        {
            collision.gameObject.SetActive(false);
            Hurt();
            if (score > 0)
            {
                score -= 100;
                if (score < 0)
                {
                    score = 0;
                }
            }
            scoreTxt.text = score.ToString();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            cntCollision--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            playerSound.SoundPlay(4);

            Destroy(collision.gameObject);
            score += 200;
            scoreTxt.text = score.ToString();
        }
        if (collision.transform.tag == "Heart")
        {
            playerSound.SoundPlay(5);
            if (cntHeart < 3)
            {
                cntHeart++;
                HeartDisplay(cntHeart);
            }
            else
            {
                score += 1000;
                scoreTxt.text = score.ToString();
            }
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Monster")
        {
            collision.gameObject.SetActive(false);
            score += 500;
            scoreTxt.text = score.ToString();
        }
    }
    private void HeartDisplay(int cntH)
    {
        switch (cntH)
        {
            case 0:
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_1").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_2").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_3").gameObject.SetActive(false);
                break;
            case 1:
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_1").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_2").gameObject.SetActive(false);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_3").gameObject.SetActive(false);
                break;
            case 2:
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_1").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_2").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_3").gameObject.SetActive(false);
                break;
            case 3:
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_1").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_2").gameObject.SetActive(true);
                GameObject.Find("Canvas").transform.Find("HeartUI").transform.Find("Heart_3").gameObject.SetActive(true);
                break;
        }
           

    }
}
