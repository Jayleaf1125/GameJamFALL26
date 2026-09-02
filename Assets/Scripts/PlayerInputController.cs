using System;
using Consystently.Essentials;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : Manager<PlayerInputController>
{
  public TenJam Controls { get; private set; }
  public TenJam.MenuActions Menu { get; private set; }
  public TenJam.GameplayActions Gameplay { get; private set; }

  public event Action<string> OnLetterKeyPressed, OnSpaceBarPressed;

  protected override void Awake()
  {
    base.Awake();

    Controls = new TenJam();
    Menu = Controls.Menu;
    Gameplay = Controls.Gameplay;
  }

  public void OnEnable()
  {
    Controls.Enable();

    Menu.Space.started += x => OnSpaceBarPressed?.Invoke(x.control.name);

    Gameplay.LetterKeys.started += x => OnLetterKeyPressed?.Invoke(x.control.name);
  }

  public void OnDisable()
  {
    Controls.Disable();

    Menu.Space.started -= x => OnSpaceBarPressed?.Invoke(x.control.name);

    Gameplay.LetterKeys.started -= x => OnLetterKeyPressed?.Invoke(x.control.name);
  }
}
