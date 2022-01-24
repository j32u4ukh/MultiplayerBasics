using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log($"There are now {numPlayers} players!");
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        Debug.Log("I connected to a server!");
    }
}

/* �оǤ��A�~�ӤF NetworkManager ��AInspector ���]�|������ NetworkManager �ɤ@�ˡA
 * �۰ʥ[�� Transport �}���A���ڹ�ڰ���S���A���ӬO�]���睊�᪺�t���C
 * �w�]�� Transport �]�q TelepathyTransport �ܦ� KcpTransport�A�ϥ��S���۰ʱ����A
 * �ڴN�����M�оǬۦP�� Transport�C
 * P.S. ���F Reset �s��A�~�|�ˬd�O�_�� Transport�A�S�����ܦA�۰ʥͦ� KcpTransport
 * 
 * �� Client �s�� Server�A
 * Client �|Ĳ�o 1. OnStartClient 2. OnClientConnect�F
 * Server �|Ĳ�o 1. OnServerConnect 2. OnServerReady (3. Create Player) 4. OnServerAddPlayer
 */