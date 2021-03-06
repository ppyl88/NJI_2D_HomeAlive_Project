using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLoop_Mission : MonoBehaviour
{
    public float scrollSpeed_Map = 3.5f;
    public GameObject azone, bzone;
    public GameObject[] blocks;
    public GameObject home;
    private int indexPlatform;
    bool trigClear = false;

    // Update is called once per frame
    void Update()
    {
        azone.transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed_Map);
        bzone.transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed_Map);
        if (bzone.transform.position.x < 0 && !trigClear)
        {
            //게임오브젝트 제거
            Destroy(azone);
            //Azone에 Bzone을 할당
            azone = bzone;
            //함수 호출
            RandomPlatform();
        }
        else if (GameObject.Find("Player").GetComponent<PlayerCtrl_Mission>().chkstate && !trigClear)
        {
            //게임오브젝트 제거
            Destroy(azone);
            //Azone에 Bzone을 할당
            azone = bzone;
            azone.SetActive(false);
            ClearPlatform();
            trigClear = true;
        }
        else if (bzone.transform.position.x < 0 && trigClear)
        {
            scrollSpeed_Map = 0;
            GameObject.Find("Background").GetComponent<MapScroll>().scrollSpeed_BG = 0f;
            GameObject.Find("Player").GetComponent<PlayerCtrl_Mission>().playerAnim.SetBool("isClear", true);
        }

    }
    void RandomPlatform()
    {
        //난수를 발생 (범위는 배열 길이만큼)
        indexPlatform = Random.Range(0, blocks.Length);
        //오브젝트 생성(복사) 함수
        bzone = Instantiate(blocks[indexPlatform], new Vector3(bzone.transform.position.x + 15f, 0, -1), transform.rotation) as GameObject;
        //+Random.Range(10f,15f)
        //bzone.transform.position.x
    }
    void ClearPlatform()
    {
        bzone = Instantiate(home, new Vector3(bzone.transform.position.x + 8f, 0, -1), transform.rotation) as GameObject;
    }
}
