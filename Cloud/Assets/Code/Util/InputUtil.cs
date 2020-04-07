using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace daichHasegawaUtil
{
    public class InputUtil
    {
        /// <summary>
        /// 押しているキーを返します。
        /// </summary>
        /// <returns>押しているキー</returns>
        public static KeyCode GetInputtingKeyCode()
        {
            KeyCode keyCode = new KeyCode();
            if (Input.anyKey)
            {
                foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
                    if (Input.GetKey(code))
                    {
                        keyCode = code;
                        break;
                    }
            }

            return keyCode;
        }
    }
}