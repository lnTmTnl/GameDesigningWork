using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovableObj : MonoBehaviour
{
    private Vector3 originPosition;  //原始位置
    private Vector3 originRotation;  //原始旋转角度
    private Vector3 originLocalRotation;  //原始局部旋转角度

    public bool isSelected;  //是否被选中
    public MoveType moveType;  //可动类型
    public Vector3 singleMove;  //每次运动规则
    public Vector3 maxRange;  //最大可平移或旋转范围
    public Vector3 minRange;  //最小可平移或旋转范围
    public Tween tween;  //动画过渡

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originRotation = transform.eulerAngles;
        originLocalRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.GetComponent<HighLightOutline>()) transform.GetComponent<HighLightOutline>().enabled = isSelected;
    }

    public void MoveObj(int direction)
    {
        Vector3 deltaPosition = transform.position - originPosition;
        Vector3 deltaRotation = transform.eulerAngles - originRotation;
        Vector3 deltaLocalRotation = transform.localEulerAngles - originLocalRotation;

        switch (moveType)
        {
            case MoveType.Position:
                Vector3 targetVec3 = transform.position + singleMove * direction;

                if (deltaPosition.x + singleMove.x * direction > maxRange.x || deltaPosition.y + singleMove.y * direction > maxRange.y || deltaPosition.z + singleMove.z * direction > maxRange.z) return;
                if (deltaPosition.x + singleMove.x * direction < minRange.x || deltaPosition.y + singleMove.y * direction < minRange.y || deltaPosition.z + singleMove.z * direction < minRange.z) return;
                
                transform.DOComplete();
                transform.DOMove(targetVec3, .6f).SetEase(Ease.OutBack);
                break;

            case MoveType.Rotation:
                if (deltaRotation.x + singleMove.x * direction > maxRange.x || deltaRotation.y + singleMove.y * direction > maxRange.y || deltaRotation.z + singleMove.z * direction > maxRange.z) return;
                if (deltaRotation.x + singleMove.x * direction < minRange.x || deltaRotation.y + singleMove.y * direction < minRange.y || deltaRotation.z + singleMove.z * direction < minRange.z) return;

                transform.DOComplete();
                transform.DORotate(singleMove * direction, .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
                break;

            case MoveType.LocalRotation:
                if (deltaLocalRotation.x + singleMove.x * direction > maxRange.x || deltaLocalRotation.y + singleMove.y * direction > maxRange.y || deltaLocalRotation.z + singleMove.z * direction > maxRange.z) return;
                if (deltaLocalRotation.x + singleMove.x * direction < minRange.x || deltaLocalRotation.y + singleMove.y * direction < minRange.y || deltaLocalRotation.z + singleMove.z * direction < minRange.z) return;

                transform.DOComplete();
                tween = transform.DORotate(singleMove * direction, .6f, RotateMode.LocalAxisAdd).SetEase(Ease.OutBack);
                break;
        }
    }
}
