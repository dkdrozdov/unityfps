using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolUser : MonoBehaviour
{
    public Equipment equipment;
    public delegate void Selection(ToolType toolType);
    public static event Selection OnSelect;
    public Camera userCamera;
    public ITool ActiveTool;
    public Vector3 GetForward()
    {
        return userCamera.transform.forward;
    }
    public Vector3 getCameraCenterPoint()
    {
        return userCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
    }
    // Start is called before the first frame update
    void Start()
    {
        ActiveTool = equipment.GetTool(ToolType.Melee);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            ActiveTool.Use();
        }
        if (Input.GetButtonDown("1") || Input.GetButtonDown("2") || Input.GetButtonDown("3") || Input.GetButtonDown("4"))
        {
            ToolType SelectedToolType = default;
            if (Input.GetButtonDown("1"))
            {
                SelectedToolType = ToolType.Melee;
            }
            if (Input.GetButtonDown("2"))
            {
                SelectedToolType = ToolType.Ranged;
            }
            if (Input.GetButtonDown("3"))
            {
                SelectedToolType = ToolType.Utility;
            }
            if (Input.GetButtonDown("4"))
            {
                SelectedToolType = ToolType.Magic;
            }
            OnSelect?.Invoke(SelectedToolType);
            ActiveTool = equipment.GetTool(SelectedToolType);
        }
    }
}
