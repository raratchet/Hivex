using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.Build.Content;
using UnityEngine;

public abstract class Player: MonoBehaviour 
{
    public Sprite icon;
    public Play playType;
    public virtual void Load()
    {

    }
}
