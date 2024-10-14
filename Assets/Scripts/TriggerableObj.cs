using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableObj : MonoBehaviour
{
    public bool isTriggerred;  //是否已触发
    public bool releasable;  //是否可释放
    public List<PassivelyMovableObj> triggerTargets;  //触发目标
    public List<TriggerCondition> triggerConditions;  //触发条件

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!releasable && isTriggerred) return;
        if (GameManager.instance.player.walking) return;

        bool canBeTriggerred = false;

        foreach(TriggerCondition triggerCondition in triggerConditions)
        {
            if (triggerCondition.JudgeConditions())
            {
                canBeTriggerred = true;
                break;
            }
        }

        if (!isTriggerred && canBeTriggerred)
        {
            isTriggerred = true;
            foreach (PassivelyMovableObj triggerTarget in triggerTargets)
            {
                triggerTarget.MoveObj(1);
            }
        }
        
        if(releasable && !canBeTriggerred && isTriggerred)
        {
            foreach (PassivelyMovableObj triggerTarget in triggerTargets)
            {
                isTriggerred = false;
                triggerTarget.MoveObj(-1);
            }
        }
    }

    [System.Serializable]
    public class TriggerCondition:ObjCondition
    {
        
    }
}
