using System;
using System.Runtime.Serialization;
using Godot;

public partial class StartMenu : Control
{
    CheckButton checkButton;
    Button button;
    PackedScene multiplayerClient;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        multiplayerClient = GD.Load<PackedScene>(
            "res://scenes/MultiplayerClient/MultiplayerClient.tscn"
        );

        checkButton = GetNode<CheckButton>("IsServerButton");
        checkButton.Toggled += OnToggle;

        button = GetNode<Button>("StartButton");
        button.Pressed += OnStart;

        button.Text = $"Запустить {(checkButton.ButtonPressed ? "сервер" : "клиент")}";
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void OnStart()
    {
        MultiplayerClient scene = multiplayerClient.Instantiate<MultiplayerClient>();

        GetTree().Root.AddChild(scene);

        if (checkButton.ButtonPressed)
        {
            scene.StartServer();
            scene.GetNode<Node>("Client").QueueFree();
        }
        else
        {
            scene.StartClient();
            scene.GetNode<Node>("Server").QueueFree();
        }
        Hide();
        ProcessMode = ProcessModeEnum.Disabled;
    }

    public void OnToggle(bool value)
    {
        button.Text = $"Запустить {(value ? "сервер" : "клиент")}";
    }
}
