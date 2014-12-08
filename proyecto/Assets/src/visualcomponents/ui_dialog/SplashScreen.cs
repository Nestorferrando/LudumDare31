using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.src.visualcomponents.ui_dialog
{
    class SplashScreen : MonoBehaviour
    {

        private Image img;
  
        private void Start()
        {
            img = GetComponent<Image>();
            img.enabled = true;
            Text childText = GameObject.Find("continue").GetComponent<Text>();
            childText.enabled = false;

        }

        public void remove()
        {
            img.CrossFadeAlpha(0f, 0.5f, false);
            GetComponentsInChildren<IntermitentText>()[0].Enabled = false;

        }

    }

}
