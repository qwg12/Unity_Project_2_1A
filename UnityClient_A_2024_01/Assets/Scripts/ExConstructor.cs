using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExConstructor : MonoBehaviour                            //생성자 예제
{
    private int value;                                              //사용할 변수 설정

    //생성자
    public ExConstructor(int _value)                                
    {
        value = _value;
        Debug.Log("객체가 생성되었습니다 . 초기값 : " + value);
    }

    // Start is called before the first frame update
    void Start()
    {
        ExConstructor ex = new ExConstructor(10);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(this.gameObject);
        }
    }

    //Unity 에서는 명시적으로 소멸자를 호출 하는 것이 아니라 OnDestroy()메서드를 활용하여 객체 파괴 될때 필요한 작업수행
    void OnDestroy()
    {
        Debug.Log("객체가 파괴되었습니다.");
    }
}
