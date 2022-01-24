using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        player.setDisplayName($"Player {numPlayers}");
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        player.setDisplayColor(color);
    }

    /// <summary>
    /// 用 NetworkClient.connection 取代原本傳入參數的 NetworkConnection conn
    /// </summary>
    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }
}

/* 教學中，繼承了 NetworkManager 後，Inspector 中也會像掛載 NetworkManager 時一樣，
 * 自動加掛 Transport 腳本，但我實際執行沒有，應該是因為改版後的差異。
 * 預設的 Transport 也從 TelepathyTransport 變成 KcpTransport，反正沒有自動掛載，
 * 我就掛載和教學相同的 Transport。
 * P.S. 按了 Reset 鈕後，才會檢查是否有 Transport，沒有的話再自動生成 KcpTransport
 * 
 * 當 Client 連到 Server，
 * Client 會觸發 1. OnStartClient 2. OnClientConnect；
 * Server 會觸發 1. OnServerConnect 2. OnServerReady (3. Create Player) 4. OnServerAddPlayer
 */