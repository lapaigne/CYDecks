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
}
