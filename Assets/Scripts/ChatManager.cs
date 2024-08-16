using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager SingleTon;

    [SerializeField] ChatMessage chatMessagePrefab;
    [SerializeField] CanvasGroup chatContent;
    [SerializeField] TMP_InputField chatInput;

    public string playerName;
    void Awake()
    {
        ChatManager.SingleTon = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(chatInput.text, playerName);
            chatInput.text = " ";
        }
    }
    public void SendChatMessage(string _message, string _fromWho = null)
    {
        if (string.IsNullOrWhiteSpace(_message)) return;
        string S = _fromWho + ":" + _message;

    }
    void AddMessage(string msg)
    {
        ChatMessage CM = Instantiate(chatMessagePrefab, chatContent.transform);
        CM.SetText(msg);
    }
    [ServerRpc(RequireOwnership = false)]
    public void SendChatMessageServerRpc(string msg)
    {
        ReceiveChatMessageClientRpc(msg);
    }
    [ClientRpc]
    public void ReceiveChatMessageClientRpc(string msg)
    {
        ChatManager.SingleTon.AddMessage(msg);
    }
}
