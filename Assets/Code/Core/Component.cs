using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svitwo
{
    public class BaseComponent
    {
        protected static uint s_idCounter = 0;
        public const int MAX_COMPONENT_COUNT = 256;
    }

    public class Component<T> : BaseComponent
    {
        private static uint s_id;

        static Component()
        {
            s_id = ++s_idCounter;
        }

        public static uint TypeId { get { return s_id; } }
    }
}
