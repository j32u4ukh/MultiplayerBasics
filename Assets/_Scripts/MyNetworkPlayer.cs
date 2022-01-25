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
}
