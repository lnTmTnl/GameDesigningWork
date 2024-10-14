using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObj : MonoBehaviour
{
    public bool selectable;  //�Ƿ��ѡ��
    public List<MovableObj> selectTargets;  //ѡ��Ŀ��
    public SelectedCondition selectedCondition;  //ѡ������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedCondition == null) return;
        selectable = selectedCondition.JudgeConditions();
    }
    
}

[System.Serializable]
public class SelectedCondition : ObjCondition
{

}