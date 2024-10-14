using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksCubeController : MonoBehaviour
{
    public Transform mapCenter;
    public List<CubeMap> EdgeCubes;
    public List<CubeMap> connectedCubes;
    public int distance;
    public bool isBottom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            connectedCubes.Clear();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            /*if (selectedObjs != null)
            {
                for (int i = 0; i < selectedObjs.Count; i++)
                {
                    selectedObjs[i].isSelected = false;
                }
            }
            selectedObjs = null;*/

            if (Physics.Raycast(mouseRay, out mouseHit) && mouseHit.transform.GetComponent<RubiksCubeController>())
            {
                mouseHit.transform.GetComponent<RubiksCubeController>().GetConnectedCubes();
            }
        }
    }

    public void GetConnectedCubes()
    {
        connectedCubes.Clear();
        Vector3 pos = transform.position + (isBottom ? -3 * transform.up : Vector3.zero);
        
        for (int i = 0; i < EdgeCubes.Count; i++)
        {
            EdgeCubes[i].controllerCube.SetParent(transform.parent);
            EdgeCubes[i].mapCube.SetParent(mapCenter.parent);

            //Vector3 cubPos = EdgeCubes[i].controllerCube.position;
            //EdgeCubes[i].controllerCube.position = new Vector3((int)cubPos.x, (int)cubPos.y, (int)cubPos.z);

            Vector3 conCubePos = EdgeCubes[i].controllerCube.position;
            Vector3 leftCenter = pos - transform.right * distance;
            Vector3 rightCenter = pos + transform.right * distance;
            Vector3 backCenter = pos - transform.forward * distance;
            Vector3 forwardCenter = pos + transform.forward * distance;
            Vector3 leftBackCenter = pos - transform.right * distance - transform.forward * distance;
            Vector3 leftForwardCenter = pos - transform.right * distance + transform.forward * distance;
            Vector3 rightBackCenter = pos + transform.right * distance - transform.forward * distance;
            Vector3 rightForwardCenter = pos + transform.right * distance + transform.forward * distance;

            Vector3 leftOffset = conCubePos - leftCenter;
            Vector3 rightOffset = conCubePos - rightCenter;
            Vector3 backOffset = conCubePos - backCenter;
            Vector3 forwardOffset = conCubePos - forwardCenter;
            Vector3 leftBackOffset = conCubePos - leftBackCenter;
            Vector3 leftForwardOffset = conCubePos - leftForwardCenter;
            Vector3 rightBackOffset = conCubePos - rightBackCenter;
            Vector3 rightForwardOffset = conCubePos - rightForwardCenter;

            float offset = 0.5f;

            if (((Mathf.Abs(leftOffset.x) <= offset) && (Mathf.Abs(leftOffset.y) <= offset) && (Mathf.Abs(leftOffset.z) <= offset)) ||
                ((Mathf.Abs(rightOffset.x) <= offset) && (Mathf.Abs(rightOffset.y) <= offset) && (Mathf.Abs(rightOffset.z) <= offset)) ||
                ((Mathf.Abs(backOffset.x) <= offset) && (Mathf.Abs(backOffset.y) <= offset) && (Mathf.Abs(backOffset.z) <= offset)) ||
                ((Mathf.Abs(forwardOffset.x) <= offset) && (Mathf.Abs(forwardOffset.y) <= offset) && (Mathf.Abs(forwardOffset.z) <= offset)) ||
                ((Mathf.Abs(leftBackOffset.x) <= offset) && (Mathf.Abs(leftBackOffset.y) <= offset) && (Mathf.Abs(leftBackOffset.z) <= offset)) ||
                ((Mathf.Abs(leftForwardOffset.x) <= offset) && (Mathf.Abs(leftForwardOffset.y) <= offset) && (Mathf.Abs(leftForwardOffset.z) <= offset)) ||
                ((Mathf.Abs(rightBackOffset.x) <= offset) && (Mathf.Abs(rightBackOffset.y) <= offset) && (Mathf.Abs(rightBackOffset.z) <= offset)) ||
                ((Mathf.Abs(rightForwardOffset.x) <= offset) && (Mathf.Abs(rightForwardOffset.y) <= offset) && (Mathf.Abs(rightForwardOffset.z) <= offset)))
            {
                connectedCubes.Add(EdgeCubes[i]);
            }
        }

        for (int i = 0; i < connectedCubes.Count; i++)
        {
            connectedCubes[i].controllerCube.SetParent(transform);
            connectedCubes[i].mapCube.SetParent(mapCenter);
        }
    }

    [System.Serializable]
    public class CubeMap
    {
        public Transform controllerCube;
        public Transform mapCube;
    }
}
