using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class Equipment : NetworkBehaviour
{
    public MeleeWeapon melee;

    public ITool GetTool(ToolType type)
    {
        switch (type)
        {
            case ToolType.Melee:
                return melee;
            default:
                return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
