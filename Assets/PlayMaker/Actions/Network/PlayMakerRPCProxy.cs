using UnityEngine;
using System.Collections;

public class PlayMakerRPCProxy : MonoBehaviour
{
    public PlayMakerFSM[] fsms;

    public void Reset()
    {
        fsms = GetComponents<PlayMakerFSM>();
    }

#if !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL)
    [RPC]
#endif
    public void ForwardEvent(string eventName)
    {
        foreach (var fsm in fsms)
        {
            fsm.SendEvent(eventName);
        }
    }
}
