using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ConfigManager : MonoBehaviour
{
    #region Networked Player Local
    [SerializeField]private NetworkedPlayer myPlayer;
    public void RegisterMyPlayer(NetworkedPlayer myPla) => myPlayer=myPla;
    public void UnregisterMyPlayer(NetworkedPlayer myPla) => myPlayer=null;

    public bool TryGetMyLocalPlayer(ref NetworkedPlayer myPla)
    {
        if (myPlayer == null)
            return false;

        myPla = myPlayer;
        return true;
    }
    #endregion
    
}
