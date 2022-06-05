using System.Runtime.CompilerServices;

namespace AillieoUtils.EasyBindings
{
    public abstract class BindableObject
    {
        public readonly Event<string> onValueChanged = new Event<string>();
        
        protected void RaiseNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            onValueChanged.Invoke(propertyName);
        }
    }
}
