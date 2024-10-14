using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObj : MonoBehaviour
{
    public bool selectable;  //是否可选中
    public List<MovableObj> selectTargets;  //选中目标
    public SelectedCondition selectedCondition;  //选中条件

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