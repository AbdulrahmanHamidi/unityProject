using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using Ookii.Dialogs;
using UnityEngine;
using UnityEngine.UI;


namespace Crosstales.FB
{
    public class UploadImage : MonoBehaviour
    {
        public GameObject BoxInfo; // this game object will hold box info that will come from main form 
        // because we need box's info to create new image name

        public GameObject TextPrefab;

        public GameObject ScrollView;

        public Button OpenFilesBtn;
        public Button OpenFoldersBtn;

        public Text Error;

        private string NewPath;
        private string OldPath;


        public void Start()
        {
            //Util.Config.DEBUG = true;

            if (OpenFilesBtn != null)
                OpenFilesBtn.interactable = FileBrowser.canOpenMultipleFiles;

            if (OpenFoldersBtn != null)
                OpenFoldersBtn.interactable = FileBrowser.canOpenMultipleFolders;
        }


        public void OpenSingleFile() // this function will run when user press on upload btn
        {
            string path = FileBrowser.OpenSingleFile("jpg"); // this will search for our image but for testing we wrote txt

            if (path != "")
            {
                OldPath = path;
            }
        }


      

        public void Upload()
        {
            //DO NOT DELETE 
            //getting box info to write them in our new uploaded image
            //new name format will be like this 
            // box.name_box.x_box.y_box.z_randomNumber and extintion will be jpg

            Box box = new Box(BoxInfo);
            string newPath = Application.dataPath + "/"+ box.name+"_"+box.x+"_"+box.y+"_"+box.z+"_"+Time.deltaTime%100+".jpg";
            if (!File.Exists(newPath))
            {
                File.Copy(OldPath, newPath);
            }
            else
            {
                // show msg that this file exists before and no need to upload it again
            }
        }
    }
}