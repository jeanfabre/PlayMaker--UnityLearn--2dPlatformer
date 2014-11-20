// (c) copyright Hutong Games, LLC 2010-2012. All rights reserved.
using System.Collections;
using UnityEngine;
using HutongGames.PlayMaker;

/// <summary>
/// This static class defines some useful extension methods for several PlayMaker specific classes (e.g. VariableType).
/// </summary>
public static class PlayMakerUtilsDotNetExtensions
{

    public static bool Contains(this VariableType[] target, VariableType vType)
    {
        if (target == null)
        {
            return false;
        }

        for (int index = 0; index < target.Length; index++)
        {
            if ( target[index] == vType)
            {
                return true;
            }
        }

        return false;
    }
}
