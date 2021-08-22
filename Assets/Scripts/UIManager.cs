using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StatBar energyPanel;
    public StatBar healthPanel;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public StatBar GetEnergyBar()
    {
        return energyPanel;
    }
    public StatBar GetHealthBar()
    {
        return healthPanel;
    }
}
