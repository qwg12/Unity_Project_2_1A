using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExParentClass : MonoBehaviour //��� : ����ƼƼ ������Ʈ���� �����ϰ�
{
    //protected�� ����� ������ ���� Ŭ���� �I �Ļ� Ŭ�������� ���� ����
    protected int protectedValue;
}

public class ExchildClass : ExParentClass //ExParentClass�� ���
{
    void Start()
    {
        //ExParentClass�� protected ������ ���� ����
        Debug.Log("Protected Value from Child Class : " + protectedValue);
    }
}
