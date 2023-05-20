## Easy Bindings

[English version](README.md)

EasyBindings可以方便地管理事件和回调函数的绑定。具体包括以下功能：

- 使用`Binder`记录绑定`UnityEvent`，[EasyEvent](https://github.com/aillieo/EasyEvent.git)及C#原生`event`的回调函数，并快速清理绑定；

- 包含类型`BindableProperty`，可用于绑定函数以监听其值变化；

- 包含类型`BindableObject`，可用于绑定函数以监听其属性变化；

- 包含了三个常用的可绑定集合，`BindableList`，`BindableDictionary`和`BindableSet`，可用于绑定函数以监听其增加、删除、更新等变化；

- 为`Binder`增加扩展方法，添加自定义的绑定，并注册清理绑定的函数；

## 快速上手

### 直接绑定事件

绑定`UnityEvent`，直接绑定。解除绑定时，调用`Dispose()`：

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

绑定[EasyEvent](https://github.com/aillieo/EasyEvent.git)，直接绑定。解除绑定时，调用`Dispose()`：

```c#
private Binder binder = new Binder();
private void OnEnable()
{
    EasyEvent<int> someEvent = ...;
    binder.BindEvent(someEvent, v => UnityEngine.Debug.Log(v));
}

private void OnDisable() => binder.Dispose();
```

绑定C#原生的`event`，由于受到语言的限制，需要使用`RegisterCustomCleanupAction()`方法来实现绑定的清理：

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

### `BindableProperty`和`BindableObject`

获取`BindableProperty`对象实例，绑定其值或绑定其变化：

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

`BindableObject`是抽象基类，在使用时先定义基于`BindableObject`的子类，注意属性`setter`的写法：

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

绑定其属性变化，可以绑定全部属性或者指定名称的属性：

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

### 可绑定集合

可绑定集合包含`BindableList`，`BindableDictionary`和`BindableSet`。其接口与C#原生集合基本一致。

以`BindableList`为例：

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

### 其它自定义的绑定

可以使用`RegisterCustomCleanupAction()`来注册自定义的回调：

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

如果需要在很多场合使用，可以为`Binder`封装扩展方法，可参考`BinderExtensions.UnityEvents.cs`。

## 安装

克隆此仓库并放入工程目录中即可，或者在Package Manager窗口中将`https://github.com/aillieo/EasyBindings.git#upm`添加为依赖项。

## 其它相关仓库

[EasyEvent](https://github.com/aillieo/EasyEvent.git)
