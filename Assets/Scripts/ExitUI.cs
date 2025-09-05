using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    [SerializeField] private WindowTransparent windowTransparent;
    [SerializeField] private Toggle switchTop;

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (windowTransparent.SetTop)
        {
            switchTop.isOn = true;
        }
        else
        {
            switchTop.isOn = false;
        }
    }

    public void ClickSwitchTop()
    {
        windowTransparent.SetTop = switchTop.isOn;
        windowTransparent.SwichTop();
    }
}
