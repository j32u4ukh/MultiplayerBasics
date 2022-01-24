using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] private string display_name = "Missing Name";
    [SyncVar] [SerializeField] private Color display_color = Color.white;

    [Server]
    public void setDisplayName(string display_name)
    {
        this.display_name = display_name;
    }

    [Server]
    public void setDisplayColor(Color color)
    {
        display_color = color;
    }
}
