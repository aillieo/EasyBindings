## Easy Bindings

[中文版本](README.zh-cn.md)

EasyBindings makes it easy to manage the binding of events and callback functions. And the following features are included.

- Use `Binder` to record callback functions bound to `UnityEvent`, [EasyEvent](https://github.com/aillieo/EasyEvent.git), and C# event to remove them conveniently.

- Type `BindableProperty`, to which callbacks can be bound to listen to its value change.

- Type `BindableObject`, to which callbacks can be bound to listen for changes of their properties.

- Three generally used bindable collections, `BindableList`, `BindableDictionary`, and `BindableSet`, to which callbacks can be bound to listen to changes like `Add`, `Remove`, and `Update`, etc.

-  Create extension methods for `Binder`, and manage whatever bindings with custom clean up callback registerations.

## Quick start

### Binding events

As for `UnityEvent`, bind callbacks directly. Call `Dispose()` when to remove.

```c#
private Binder binder = new Binder();
private void OnEnable()
{
    // bind button click event
    Button button = this.GetComponent<Button>();
    binder.BindUnityEvent(button.onClick, () =>
    {
        UnityEngine.Debug.Log("clicked!");
    });

    // bind scroll event
    ScrollRect scrollRect = this.GetComponent<ScrollRect>();
    binder.BindUnityEvent(scrollRect.onValueChanged, v =>
    {
        UnityEngine.Debug.Log($"scroll pos {v}");
    });
    
    // bind toggle event
    Toggle toggle = this.GetComponent<Toggle>();
    binder.BindUnityEvent(toggle.onValueChanged, v =>
    {
        UnityEngine.Debug.Log($"toggle changed {v}");
    });
}

private void OnDisable()
{
    // unbind
    binder.Dispose();
}
```

As for [EasyEvent](https://github.com/aillieo/EasyEvent.git), bind callbacks directly. Call `Dispose()` when to remove.

```c#
private AillieoUtils.Event<int> someEvent = new Event<int>();

private Binder binder = new Binder();
private void OnEnable()
{
    binder.BindEvent(someEvent, v => UnityEngine.Debug.Log(v));
}

private void OnDisable() => binder.Dispose();
```

As for C# `event`, due to language limitations, the `RegisterCustomCleanupAction()` method is needed for the binding cleanup.

```c#
private Binder binder = new Binder();
private void OnEnable()
{
    // bind and register to Binder
    Application.focusChanged += this.OnFocusChanged;
    binder.RegisterCustomCleanupAction(() => Application.focusChanged -= this.OnFocusChanged);
}

private void OnDisable()
{
    // unbind
    binder.Dispose();
}

private void OnFocusChanged(bool v) { }
```

### `BindableProperty` and `BindableObject`

Get a `BindableProperty` instance, and then callbacks can be used to listen to its value or its change.

```c#
// BindableProperty instance
private BindableProperty<int> age = new BindableProperty<int>(20);

private Binder binder = new Binder();
private Text ageText;

private void OnEnable()
{
    ageText = GetComponent<Text>();

    // bind and sync
    binder.BindPropertyValue(age, v => ageText.text = v.ToString());

    // bind change event
    binder.BindPropertyChange(age, () => UnityEngine.Debug.Log($"age changed"));    
    binder.BindPropertyChange(age, v => UnityEngine.Debug.Log($"age changed to {v}"));
    binder.BindPropertyChange(age, (v0, v1) => UnityEngine.Debug.Log($"age changed from {v0} to {v1}"));
}

private void OnDisable() => binder.Dispose();
```

`BindableObject` is an abstract type. Define your object type derived from `BindableObject` before using it. And note that the property setter shall be written with the following pattern.

```c#
public class SomeFriend : BindableObject
{
    private int phone;
    public int Phone
    {
        get => phone;
        set => SetStructValue(ref phone, value);
    }

    private string address;
    public string Address
    {
        get => address;
        set => SetClassValue(ref address, value);
    }
}
```

Bind all properties or property with the specified name.

```c#
// BindableObject instance
private SomeFriend someone = new SomeFriend();

private Binder binder = new Binder();

private void OnEnable()
{
    // bind
    binder.BindObject(someone, propName => UnityEngine.Debug.Log($"{propName} changed"));
    binder.BindObject(someone, "Age", () => UnityEngine.Debug.Log($"Age changed"));
}

private void OnDisable() => binder.Dispose();
```

### Bindable collections

There are `BindableList`, `BindableDictionary`, and `BindableSet` in bindable collections, and they have similar methods with C# generic collection types.

Take the `BindableList` as an example.

```c#
private BindableList<int> list = new BindableList<int>();
private Binder binder = new Binder();

private void OnEnable()
{
    binder.BindList(list, e =>
    {
        UnityEngine.Debug.Log($"change type: {e.type}");
        UnityEngine.Debug.Log($"changed index: {e.index}");
    });
}

private void OnDisable() => binder.Dispose();
```

### Other bindings

Use `RegisterCustomCleanupAction()` to register custom cleanup actions.

```c#
private Binder binder = new Binder();
        
private void OnEnable()
{
    LoadSomeAssets();
    binder.RegisterCustomCleanupAction(UnloadSomeAssets);
    
    PauseSomeActions();
    binder.RegisterCustomCleanupAction(ResumeSomeActions);
    
    ChangeSomeStates();
    binder.RegisterCustomCleanupAction(ResetSomeStates);
}

private void OnDisable() => binder.Dispose();


private void LoadSomeAssets() { }
private void UnloadSomeAssets() { }

private void ChangeSomeStates() { }
private void ResetSomeStates() { }

private void PauseSomeActions() { }
private void ResumeSomeActions() { }
```

Wrap and create extension methods for `Binder` if you use it frequently. See `BinderExtensions.UnityEvents.cs`.

## Installation

Clone this repository and copy it to your project folder, or add https://github.com/aillieo/EasyBindings.git#upm as a dependency in the Package Manager window.

## Related repositories

[EasyEvent](https://github.com/aillieo/EasyEvent.git)
