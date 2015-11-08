using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom
{
    internal class SetOnce<T>
    {
        private T m_value = default(T);

        public T Value
        {
            get
            {
                if (!IsSet)
                {
                    throw new InvalidOperationException($"{nameof(IsSet)} must be ${true} to get ${nameof(Value)}");
                }

                return m_value;
            }
            private set
            {
                m_value = value;
                IsSet = true;
            }
        }

        public bool IsSet { get; private set; } = false;

        public SetOnce()
        {
        }

        public SetOnce(T value)
        {
            Value = value;
        }

        public T Extract()
        {
            if (!IsSet)
            {
                return default(T);
            }
            else
            {
                return Value;
            }
        }

        public static SetOnce<T> Create(T nullable)
        {
            object obj = nullable;

            if (obj == null)
            {
                return new SetOnce<T>();
            }
            else
            {
                return new SetOnce<T>(nullable);
            }
        }
    }
}
