using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.input
{
    internal class InputUtils
    {
        public const float tapTimeWindow_sec = 0.1f;

        private static readonly Dictionary<InputValues, float> values = new Dictionary<InputValues, float>();


        public static InputResult readInput()
        {
            var result = new InputResult();


            //result.set(InputValues.COCK,  Input.GetKeyDown("space"));

            getInputPulse("w", InputValues.STANDUP, result);
            getInputPulse("s", InputValues.CROUCH, result);
            getInputPulse("d", InputValues.SHOOT, result);
            getInputPulse("a", InputValues.COCK, result);


            return result;
        }

        private static void getInputPulse(string keycode, InputValues value, InputResult result)
        {
           
            if (Input.GetKey(keycode))
            {
                float currentTime = Time.realtimeSinceStartup;
                float lastTimePressed;
                values.TryGetValue(value, out lastTimePressed);
                if (lastTimePressed != 0)
                {
                    values[value] = currentTime;
                    if (currentTime - lastTimePressed > tapTimeWindow_sec)
                    {
                        result.set(value, Input.GetKey(keycode));
                    }
                }
                else
                {
                    result.set(value, Input.GetKey(keycode));
                    values[value]= currentTime;
                }
            }
            else
            {
                values.Remove(value);
            }
        }
    }
}