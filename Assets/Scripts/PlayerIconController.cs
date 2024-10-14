using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconController : MonoBehaviour
{
    public Image border;
    public PlayerController player;
    public Color selectedColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        border.color = player.isBeingControlled ? selectedColor : new Color(1, 1, 1, .8f);
    }
}
