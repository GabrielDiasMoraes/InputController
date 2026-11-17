# InputController

A small multi-touch input controller for Unity. It polls the legacy Input Manager once per frame and reports which pointers (touches, plus the left mouse button) just appeared, are still held, or just left, so game code does not have to track finger IDs itself.

## Requirements

- Unity 2021.3.5f1 (the version the project was made with; the code only uses `UnityEngine.Input`, so other versions with the legacy Input Manager should work).
- Active Input Handling set to "Input Manager (Old)" or "Both".

## Install

Copy `InputController/Assets/DailySolutions/InputController` into your project's `Assets` folder.

## Usage

`InputController` is a plain C# class, not a `MonoBehaviour`: create it and call `UpdateInputs()` every frame.

```csharp
using DailySolutions.InputController;
using UnityEngine;

public class Example : MonoBehaviour
{
    private IInputController _input;

    private void Awake()
    {
        _input = new InputController();
        _input.OnNewPointer += pointers => { foreach (var p in pointers) Debug.Log($"down {p.PointerID} at {p.PointerPosition}"); };
        _input.OnUpdatePointer += pointers => { /* every active pointer, each frame */ };
        _input.OnExitPointer += pointers => { foreach (var p in pointers) Debug.Log($"up {p.PointerID}"); };
    }

    private void Update() => _input.UpdateInputs();
}
```

## API (namespace `DailySolutions.InputController`)

| Member | Description |
|---|---|
| `IInputController.UpdateInputs()` | Reads the current touches/mouse and fires the events below. Call once per frame. |
| `OnNewPointer` | `UnityAction<PointerData[]>`: pointers that appeared this frame. |
| `OnUpdatePointer` | `UnityAction<PointerData[]>`: all pointers currently active (including new ones). |
| `OnExitPointer` | `UnityAction<PointerData[]>`: pointers that were active last frame and are gone now (last known data). |
| `PointerData` | Struct with `int PointerID` and `Vector2 PointerPosition` (screen pixels). |

Notes: the left mouse button is reported as pointer ID `999`. The events fire every frame, with empty arrays when nothing changed.

## Status

Small experiment from 2023. Works for basic multi-touch tracking; no tests, no Input System package support, and no gesture recognition.

## License

MIT, see [LICENSE](LICENSE).
