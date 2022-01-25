using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text display_name_text = null;
    [SerializeField] private Renderer display_color_renderer = null;

    [SyncVar(hook = nameof(handleDisplayNameUpdate))] [SerializeField] private string display_name = "Missing Name";
    [SyncVar(hook = nameof(handleDisplayColorUpdate))] [SerializeField] private Color display_color = Color.white;

    #region Server
    /// <summary>
    /// Call by server
    /// </summary>
    /// <param name="display_name"></param>
    [Server]
    public void setDisplayName(string display_name)
    {
        this.display_name = display_name;
    }

    /// <summary>
    /// Call by server
    /// </summary>
    /// <param name="color"></param>
    [Server]
    public void setDisplayColor(Color color)
    {
        display_color = color;
    }

    /// <summary>
    /// Call by client
    /// [Command]: For clients calling a method on the server
    /// </summary>
    /// <param name="display_name"></param>
    [Command]
    private void cmdSetDisplayName(string display_name)
    {
        if (isNameValid(display_name))
        {
            rpcLogNewName(display_name);
            setDisplayName(display_name);
        }
        else
        {
            Debug.LogWarning($"The new name '{display_name}' is not valid.");
        }
    }

    [Server]
    private bool isNameValid(string new_name)
    {
        return (2 < new_name.Length) && (new_name.Length < 20);
    }
    #endregion

    #region Client
    private void handleDisplayNameUpdate(string origin_name, string new_name)
    {
        display_name_text.text = new_name;
    }

    /// <summary>
    /// 名稱不一定要叫做 origin_color/new_color，但需要新舊兩個參數
    /// </summary>
    /// <param name="origin_color">即便原本沒有使用，也需給予此參數</param>
    /// <param name="new_color"></param>
    private void handleDisplayColorUpdate(Color origin_color, Color new_color)
    {
        display_color_renderer.material.SetColor("_BaseColor", new_color);
    } 

    [ContextMenu("Set My Name")]
    private void setMyName()
    {
        cmdSetDisplayName("M");
    }

    /// <summary>
    /// [ClientRpc]: For the server calling a method on all clients
    /// </summary>
    /// <param name="display_name"></param>
    [ClientRpc]
    private void rpcLogNewName(string display_name)
    {
        Debug.Log(display_name);
    }

    // [TargetRpc]: For the server calling a method on a specific client
    #endregion
}
