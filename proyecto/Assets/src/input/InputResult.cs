using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Assets.scripts
{
    class InputResult
    {
        private Dictionary<InputValues, Boolean> values=new Dictionary<InputValues, bool>();

        public InputResult()
        {
           
        }

        public InputResult set(InputValues input, bool value)
        {
           this.values.Add(input,value);
            return this;
        }

        public bool get(InputValues input)
        {
            bool isPressed;

            this.values.TryGetValue(input, out isPressed);
            return isPressed;
        }


    }
}
