﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconForUI : MonoBehaviour {

    [SerializeField] Image[] powersIcons;
    [SerializeField] Image[] fearsIcons;
    [SerializeField] int maxIcon;

    [SerializeField] Color emptyColor;
    [SerializeField] Color fullColor;

    public void HandleIcons(List<SuperPower> pow, List<Fear> fears)
    {
        ResetIcons();

        int i = 0;
        foreach (var p in pow)
        {
            SetPowerUI(p.Icon, i);
            i++;
        }

        i = 0;
        foreach (var f in fears)
        {
            SetFearUI(f.Icon, i);
            i++;
        }
    }

    private void SetPowerUI(Sprite icon, int index)
    {
        if (icon != null) {
            if (index < maxIcon)
            {
                powersIcons[index].sprite = icon;
                powersIcons[index].color = fullColor;
            }
            else
            {
                Debug.LogError("too many power icons : " + index + "/" + maxIcon);
            }
        }
    }

    private void SetFearUI(Sprite icon, int index)
    {
        if (icon != null)
        {
            if (index < maxIcon)
            {
                fearsIcons[index].sprite = icon;
                fearsIcons[index].color = fullColor;
            }
            else
            {
                Debug.LogError("too many fear icons : " + index + "/" + maxIcon);
            }
        }
    }

    private void ResetIcons()
    {
        for(int i=0; i<maxIcon; i++)
        {
            powersIcons[i].sprite = null;
            powersIcons[i].color = emptyColor;

            fearsIcons[i].sprite = null;
            fearsIcons[i].color = emptyColor;
        }
    }
}
