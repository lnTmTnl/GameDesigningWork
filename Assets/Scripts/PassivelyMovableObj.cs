using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivelyMovableObj : MonoBehaviour
{
    public MoveType moveType;  //可动类型
    public Vector3 singleMove;  //单次移动规则

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveObj(int direction)
    {
        switch (moveType)
        {
            case MoveType.Position:
                Vector3 targetVec3 = transform.position + singleMove * direction;

                transform.DOComplete();
                transform.DOMove(targetVec3, .6f).SetEase(Ease.OutBack);
                break;

            case MoveType.Rotation:
                transform.DOComplete();
                transform.DORotate(singleMove * direction, .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
                break;

            case MoveType.LocalRotation:
                transform.DOComplete();
                transform.DORotate(singleMove * direction, .6f, RotateMode.LocalAxisAdd).SetEase(Ease.OutBack);
                break;
        }
    }
}
