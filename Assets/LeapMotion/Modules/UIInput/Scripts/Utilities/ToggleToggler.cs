/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Leap.Unity.InputModule
{
    public class ToggleToggler : MonoBehaviour
    {
        public Text text;
        public UnityEngine.UI.Image image;
        public Color OnColor;
        public Color OffColor;
        public GameObject pallet;
        public GameObject BoxSec;
        

        public void SetToggle(Toggle toggle)
        {
            if (toggle.isOn)
            {
                //text.text = "On";
                text.color = Color.white;
                image.color = OnColor;
            }
            else
            {
                //text.text = "Off";
                text.color = new Color(0.3f, 0.3f, 0.3f);
                image.color = OffColor;
            }
        }


        public void SetPallet(Toggle toggle)
        {
            if (toggle.isOn)
            {
                string[] xz = name.Split('X');
                ManagerScript.Instance.palet = new Palet(float.Parse(xz[0]),1000,float.Parse(xz[1]));
                ManagerScript.Instance.palet.x = float.Parse(xz[0]);
                ManagerScript.Instance.palet.z = float.Parse(xz[1]);

                Palet palet = ManagerScript.Instance.palet;
                Vector3 scale;

                scale = new Vector3(palet.x*0.001f,(palet.y*0.001f),palet.z*0.001f);
                scale = Vector3.Scale(scale, new Vector3(1,1,1));
                pallet.transform.localScale = scale;
                
                
                BoxSec.SetActive(true);
            }
        }
    }
}