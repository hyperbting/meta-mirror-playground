using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ControllerScript:NetworkBehaviour
{
    [SyncVar(hook = nameof(OnChangeOwner))]
    public GameObject Owner;

    void OnChangeOwner(GameObject oldOwner, GameObject newOwner)
    {
        RefTransform = newOwner.transform;
    }
    
    private void Update()
    {
        if (isServer)
            Base_Update();
        
        if (!isLocalPlayer) 
            return;

        TryBecomeOwner();


        if (!isOwned)
            return;
        
#if META
        Update_Meta();
#endif    
    }

    private void TryBecomeOwner()
    {
#if META
        // if (TryBecomeOwner_Meta())
        // {
        //     CmdChangeOwner()
        // }
#endif 
        
    }


    [Command]
    void CmdChangeOwner(GameObject owner)
    {
        //Release
        if (owner == null)
        {
            Owner = owner;
            return;
        }

        //Become
        if(Owner== null)
            Owner = owner;
    }
}
