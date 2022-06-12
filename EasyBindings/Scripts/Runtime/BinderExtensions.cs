using UnityEngine.Events;

namespace AillieoUtils.EasyBindings
{
    public static class BinderExtensions
    {
        public static void Bind(this Binder binder, UnityEvent evt, UnityAction eventHandler)
        {
            evt.AddListener(eventHandler);
            binder.RegisterCustomCleanupAction(() => evt.RemoveListener(eventHandler));
        }

        public static void Bind<T>(this Binder binder, UnityEvent<T> evt, UnityAction<T> eventHandler)
        {
            evt.AddListener(eventHandler);
            binder.RegisterCustomCleanupAction(() => evt.RemoveListener(eventHandler));
        }

        public static void Bind<T, R>(this Binder binder, UnityEvent<T, R> evt, UnityAction<T, R> eventHandler)
        {
            evt.AddListener(eventHandler);
            binder.RegisterCustomCleanupAction(() => evt.RemoveListener(eventHandler));
        }
    }
}
