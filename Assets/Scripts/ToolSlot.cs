using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ToolType
{
    Melee, Ranged, Utility, Magic
}
public class ToolSlot : MonoBehaviour
{
    Color unselectedColor = new Color(0f, 0f, 0f, 0.3f);
    Color selectedColor = new Color(0, 0, 0, 1f);
    public bool isSelected;
    public ToolType type;
    public GameObject tool;
    public Image icon;
    public Image panelImage;
    private void OnEnable()
    {
        ToolUser.OnSelect += SelectResponse;
    }
    private void OnDisable()
    {
        ToolUser.OnSelect -= SelectResponse;
    }
    public void SetTool(GameObject tool)
    {

    }
    public void Select()
    {
        isSelected = true;
        panelImage.color = selectedColor;
    }
    public void Deselect()
    {
        isSelected = false;
        panelImage.color = unselectedColor;
    }
    public void SelectResponse(ToolType toolType)
    {
        if (toolType == type)
        {
            Debug.Log(type + "  selected!");
            Select();
        }
        else
        {
            Deselect();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isSelected)
        {
            Select();
        }
        else
        {
            Deselect();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
