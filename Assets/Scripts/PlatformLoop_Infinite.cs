using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLoop_Infinite : MonoBehaviour
{
    public float scrollSpeed_Map = 3.5f;
    public GameObject azone, bzone;
    public GameObject[] blocks;
    private int indexPlatform;

    // Update is called once per frame
    void Update()
    {
        azone.transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed_Map);
        bzone.transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed_Map);
        if (bzone.transform.position.x < 0)
        {
            //게임오브젝트 제거
            Destroy(azone);
            //Azone에 Bzone을 할당
            azone = bzone;
            //함수 호출
            RandomPlatform();
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
}
