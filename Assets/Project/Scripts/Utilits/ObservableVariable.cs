using System;

namespace Utilit
{
    public class ObservableVariable<T>
    {
        public event Action<T> OnValueChangedEvent;
        private T _value;
        public T Value { 
            get => _value; 
            set
            {
                if (!_value.Equals(value))
                    OnValueChangedEvent?.Invoke(value);
                _value = value;
            }
        }
    }
}
