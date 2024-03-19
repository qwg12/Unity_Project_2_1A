using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAccessControl : MonoBehaviour
{
    //public으로 선언된 변수는 다른 스크립트에서 직접접근 가능
    public int publicVlaue;

    //private으로 선언된 변수는 같은 클래스 내에서만 접근가능
    private int privateValue;

    //protected로 선언된 변수는 같은 클래스 및 파생 클래스에서 접근가능
    protected int protectedValue;

    //internal로 선언된 변수는 같은 프로젝트 내에서 접근가능
    internal int internalValue;

    // Start 함수는 유니티가 호출하는 메서드이므로 public으로 선언해야함
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
