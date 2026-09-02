using System;
using UnityEngine;
using Consystently.Essentials;

public class MainMenuGameState : GameState
{
  public MainMenuGameState(GameManager gameManager) : base(gameManager) { }

  public override void Enter()
  {
    PlayerInputController.Instance.Menu.Enable();
  }

  public override void Update()
  {
    
  }

  public override void Exit()
  {
    PlayerInputController.Instance.Menu.Disable();
  }
    
}