using System;
using Godot;

partial class MultiplayerClient
{
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
    private void SendAction(ActionMode action)
    {
        if (Multiplayer.IsServer())
        {
            var sender = Multiplayer.GetRemoteSenderId();
            GD.Print($"From {sender}: {action}");
            return;
        }
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    private void MoveCards()
    {
        if (!Multiplayer.IsServer())
        {
            var manager = GetNode<CardManager>("Client/CardManager");

            foreach (var card in TotalList)
            {
                if (!manager.TrySelectingNewSlot(card, Player, Opponent))
                {
                    // throw new Exception();
                }
            }
        }
    }
}
