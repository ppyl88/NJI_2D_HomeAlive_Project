using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    //스크롤 속도를 정할 변수를 인스펙터 창에서 확인가능하도록 public 선언
    public float scrollSpeed_BG = 0.25f;
    //Renderer에 접근하기 위해 변수에 미리 할당할 수 있도록 변수 선언
    //제대로 들어갔는지 확인하기 위해 public으로 선언하였음
    public Renderer rend;
    float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        //변수에 컴포넌트를 가져와 할당시킴 --> 그래야 접근할 수 있어요
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //실수형 지역변수에 시간 * 속도값을 할당
        offset += Time.deltaTime * scrollSpeed_BG;
        //Renderer의 Material 속성에 접근하여 SetTextureOffset 함수를 호출
        //인자를 전달(문자열, Vector2(x,y)); -> x축 이동을 위해 x축에만 미리 생성한 offset 값을 전달
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
