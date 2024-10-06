using Mirror;
using UnityEngine;

public partial class NetworkedPlayer
{
    [SyncVar]// [SyncVar(hook = nameof(PlayerNumberChanged))]
    public double playerRTT = 0d;
    // [ServerCallback]
    // internal static void ResetPlayerNumbers()
    // {
    //     // always 0 when at ServerSide
    //     playerRTT = NetworkTime.rtt;
    // }
    
    #region Server
    public override void OnStartServer()
    {
        // Debug.LogWarning("OnStartServer");
    }
    
    public override void OnStopServer()
    {
    }
    #endregion
}
