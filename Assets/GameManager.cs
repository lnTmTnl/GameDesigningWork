using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player;
    public List<MovableObj> selectedObjs;
    public List<PathCondition> pathConditions = new List<PathCondition>();
    //public List<Transform> pivots;  //动态物体
    public List<Transform> objsToHide;
    public List<ObjToHideCondition> objToHideConditions;
    public PassCondition passCondition;
    public List<PlayerController> players;
    public GameObject passUI;

    private int currentPlayerIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (ObjToHideCondition objToHideCondition in objToHideConditions)
        {
            foreach (Transform objTohide in objsToHide)
            {
                objTohide.gameObject.SetActive(true);
            }
        }

        if(players.Count > 0) players[0].isBeingControlled = true;
    }

    void Update()
    {
        if (passCondition.JudgeConditions()) passUI.SetActive(true);

        //检查需要满足一定条件的连接关系是否满足条件
        foreach (PathCondition pc in pathConditions)
        {
            foreach(SinglePath sp in pc.paths)
                sp.block.possiblePaths[sp.index].active = pc.JudgeConditions();  //当所有条件都符合时将路径块相互设为active
        }

        if (player != null && player.walking)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].isBeingControlled = false;
            }

            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            players[currentPlayerIndex].isBeingControlled = true;
            player = players[currentPlayerIndex];
        }

        //手动控制的可动物体
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedObjs == null) return;
            int multiplier = Input.GetKey(KeyCode.RightArrow) ? 1 : -1;
            for (int i = 0; i < selectedObjs.Count; i++)
            {
                selectedObjs[i].MoveObj(multiplier);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedObjs == null) return;
            int multiplier = Input.GetKey(KeyCode.UpArrow) ? 1 : -1;
            for (int i = 0; i < selectedObjs.Count; i++)
            {
                selectedObjs[i].MoveObj(multiplier);
            }
        }

        foreach (ObjToHideCondition objToHideCondition in objToHideConditions)
        {
            bool useHideEffection = objToHideCondition.JudgeConditions();
            foreach(Transform objTohide in objToHideCondition.ObjsToHide)
            {
                objTohide.gameObject.SetActive(useHideEffection);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObjs != null)
            {
                for (int i = 0; i < selectedObjs.Count; i++)
                {
                    selectedObjs[i].isSelected = false;
                }
            }
            selectedObjs = null;
        }

        //选中可动物体
        if (Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (selectedObjs != null)
            {
                for(int i = 0; i < selectedObjs.Count; i++)
                {
                    selectedObjs[i].isSelected = false;
                }
            }
            selectedObjs = null;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<SelectableObj>() != null && mouseHit.transform.GetComponent<SelectableObj>().selectable)
                {
                    selectedObjs = mouseHit.transform.GetComponent<SelectableObj>().selectTargets;
                    for (int i = 0; i < selectedObjs.Count; i++)
                    {
                        selectedObjs[i].isSelected = true;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

    }

    /*public void RotateRightPivot()
    {
        pivots[1].DOComplete();
        pivots[1].DORotate(new Vector3(0, 0, 90), .6f).SetEase(Ease.OutBack);
    }*/
}

public abstract class ObjCondition
{
    public List<Condition> conditions;

    public bool JudgeConditions()
    {
        int count = 0;
        foreach (Condition condition in conditions)
        {
            Transform conditionObject = condition.conditionObject;
            switch (condition.conditionType)
            {
                case ConditionType.Position:
                    if(condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.position == condition.conditionVec3 ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.position == condition.conditionVec3 ? 0 : 1);
                    }
                    break;
                case ConditionType.PosX:
                    if (condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.position.x == condition.conditionVec3.x ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.position.x == condition.conditionVec3.x ? 0 : 1);
                    }
                    break;
                case ConditionType.PosY:
                    if (condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.position.y == condition.conditionVec3.y ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.position.y == condition.conditionVec3.y ? 0 : 1);
                    }
                    break;
                case ConditionType.PosZ:
                    if (condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.position.z == condition.conditionVec3.z ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.position.z == condition.conditionVec3.z ? 0 : 1);
                    }
                    break;
                case ConditionType.Rotation:
                    if (condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.eulerAngles == condition.conditionVec3 ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.eulerAngles == condition.conditionVec3 ? 0 : 1);
                    }
                    break;
                case ConditionType.LocalPosition:
                    if (condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.localPosition == condition.conditionVec3 ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.localPosition == condition.conditionVec3 ? 0 : 1);
                    }
                    break;
                case ConditionType.LocalRotation:
                    if (condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.localEulerAngles == condition.conditionVec3 ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.localEulerAngles == condition.conditionVec3 ? 0 : 1);
                    }
                    break;
                case ConditionType.Parent:
                    if (condition.conditonJugement == ConditonJudgement.Equals)
                    {
                        count += (conditionObject.parent == condition.parent ? 1 : 0);
                    }
                    else
                    {
                        count += (conditionObject.parent == condition.parent ? 0 : 1);
                    }
                    break;
            }
        }
        return (count == conditions.Count);
    }
}

[System.Serializable]
public class PathCondition:ObjCondition  //需要一定条件才能相连的路径块及其连接条件
{
    public string pathConditionName;  //连接名称
    public List<SinglePath> paths;  //相连接的路径块
}

[System.Serializable]
public class ObjToHideCondition : ObjCondition  //隐藏物体出现的条件
{
    public List<Transform> ObjsToHide;
}

[System.Serializable]
public class PassCondition : ObjCondition  //过关条件
{

}

[System.Serializable]
public class Condition  //条件
{
    public Transform conditionObject;  //条件对象
    public ConditionType conditionType;  //条件类型
    public ConditonJudgement conditonJugement;  //条件判断方式
    public Vector3 conditionVec3;  //条件向量
    public Transform parent;  //父物体

}

[System.Serializable]
public class SinglePath  //单个路径块
{
    public Walkable block;  //对应的Walkable脚本
    public int index;  //连接目标在自身Walkable.possiblePaths中的下标
}

public enum MoveType
{
    Position,
    Rotation,
    LocalRotation
}

public enum ConditionType
{
    Position,
    PosX,
    PosY,
    PosZ,
    Rotation,
    LocalPosition,
    LocalRotation,
    Parent
}

public enum ConditonJudgement
{
    Equals,
    NotEquals
}