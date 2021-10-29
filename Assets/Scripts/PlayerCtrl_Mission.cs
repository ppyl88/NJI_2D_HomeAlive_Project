using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl_Mission : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    public Animator playerAnim;
    Sound playerSound;
    private float jumpForce = 27f;
    private int cntCollision = 0;
    private int cntHeart = 3;
    public bool chkstate = false;
    private string[] missionNameArray = {"몬스터 죽이기", "코인 모으기" };
    private int missionIndex;
    private int missionTotal;
    private int missionCnt = 0;
    public Text missionTxt;
    public Text missionCount;
    public Text missionTitle;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSound = GetComponent<Sound>();
        missionTotal = Random.Range(10, 20);
        missionIndex = Random.Range(0, missionNameArray.Length);
        switch (missionIndex)
        {
            case 0:
                missionTitle.text = "몬스터를 " + missionTotal.ToString() + "마리 죽여라.";
                break;
            case 1:
                missionTitle.text = "코인을 " + missionTotal.ToString() + "개 획득하라.";
                break;
        }
        missionTxt.text = missionNameArray[missionIndex];
        missionCount.text = missionCnt + "/" + missionTotal.ToString();
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
        else if (missionCnt == missionTotal)
        {
            Clear();
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
        GameObject.Find("Ground").GetComponent<PlatformLoop_Mission>().scrollSpeed_Map = 3.5f;
        playerAnim.SetBool("isRun", false);
    }

    void Run()
    {
        GameObject.Find("Background").GetComponent<MapScroll>().scrollSpeed_BG = 0.8f;
        GameObject.Find("Ground").GetComponent<PlatformLoop_Mission>().scrollSpeed_Map = 10f;
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
            GameObject.Find("Ground").GetComponent<PlatformLoop_Mission>().scrollSpeed_Map = 0f;
            playerSound.SoundPlay(3);
            Invoke("AfterDie", 2f);
            chkstate = true;
        }
    }
    void AfterDie()
    {
        playerSound.SoundPlay(7);
        GameObject.Find("Canvas").transform.Find("GameOverUI").gameObject.SetActive(true);
    }
    void Clear()
    {
        playerAnim.SetBool("isRun", false);
        if (!chkstate)
        {
            GameObject.Find("Background").GetComponent<MapScroll>().scrollSpeed_BG = 0.25f;
            GameObject.Find("Ground").GetComponent<PlatformLoop_Mission>().scrollSpeed_Map = 3.5f;
            playerSound.SoundPlay(6);
            chkstate = true;
            Invoke("AfterClear", 7f);
        }
    }
    void AfterClear()
    {
        playerSound.SoundPlay(8);
        GameObject.Find("Canvas").transform.Find("ClearUI").gameObject.SetActive(true);
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
        }
        if (collision.transform.tag == "Obstacle")
        {
            collision.gameObject.SetActive(false);
            Hurt();
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
            if (missionIndex == 1)
            {
                missionCnt++;
                missionCount.text = missionCnt + "/" + missionTotal.ToString();
            }
        }
        if (collision.transform.tag == "Heart")
        {
            playerSound.SoundPlay(5);
            if (cntHeart < 3)
            {
                cntHeart++;
                HeartDisplay(cntHeart);
            }
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Monster")
        {
            collision.gameObject.SetActive(false);
            if (missionIndex == 0)
            {
                missionCnt++;
                missionCount.text = missionCnt + "/" + missionTotal.ToString();
            }
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
