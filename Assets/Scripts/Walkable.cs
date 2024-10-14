using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{

    public List<WalkPath> possiblePaths = new List<WalkPath>();  //可能与之发生连接的路径块

    [Space]

    public Transform previousBlock;  //玩家行走到当前块之前的一个路径块

    [Space]

    [Header("Booleans")]
    public bool isStair = false;  //是否为台阶
    public bool movingGround = false;  //是否为正在移动的物体
    public bool isButton;  //是否为按钮
    public bool dontRotate;  //是否不可旋转

    [Space]

    [Header("Offsets")]
    public float walkPointOffset = .5f;  //路径点纵向偏移
    public float stairOffset = .4f;  //台阶路径点额外纵向偏移

    public Vector3 GetWalkPoint()  //获取路径点的位置
    {
        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up * stair;
    }

    private void OnDrawGizmos()  //绘制路径点即连接线
    {
        Gizmos.color = Color.gray;
        float stair = isStair ? .4f : 0;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);

        if (possiblePaths == null)
            return;

        foreach (WalkPath p in possiblePaths)
        {
            if (p.target == null)
                return;
            Gizmos.color = p.active ? Color.black : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.target.GetComponent<Walkable>().GetWalkPoint());
        }
    }
}

[System.Serializable]
public class WalkPath
{
    public Transform target;  //路径点连接目标
    public bool active = true;  //该目标当前是否可连接
}
