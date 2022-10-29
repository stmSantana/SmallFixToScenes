using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;
using MelonLoader;
using UnityEngine.SceneManagement;

namespace SmallFixToScenes
{
    public class Patches:MonoBehaviour                               //public class Patches:MonoBehaviour
    {
        /*
        public struct fixdata
        {
            public bool Delete;
            public string Scene;
            public string Name;
            public int EulerY;
        }
        private List<fixdata> fL_ = new List<fixdata>();
        */

        public static string[,] itemDataArray =
        {
            {"0_Delete","1_Scene",              "2_Name",                               "3_EulerY", "4_Position" },
            {"false",   "CrashMountainRegion",  "STR_StoneCabinA_Prefab/TRIGGER_IndoorSpace",              "0",        "0"},
            {"false",   "CrashMountainRegion",  "STR_StoneCabinATrapDoor",              "0",        "0"},
            {"false",   "CrashMountainRegion",  "INTERACTIVE_BedMattressB",             "0",        "890.3362,160.6400,341.8036"},       //item = INTERACTIVE_BedMattressB p = 890.3362,160.6400,341.8036 r = 0.0000,90.3298,0.0000 c = 100
          //{"false",   "CrashMountainRegion",  "INTERACTIVE_BedMattressB",             "0",        "890.7637,160.6400,343.6804"},       //item = INTERACTIVE_BedMattressB p = 890.7637,160.6400,343.6804 r = 0.0000,172.8061,0.0000 c = 100

            {"false",   "CrashMountainRegion",  "INTERACTIVE_WorkBench",                "0",        "0"},       //item = INTERACTIVE_WorkBench p=886.7240,161.1370,344.1470 r=0.0000,85.3432,0.0000 c=100
            {"false",   "CrashMountainRegion",  "StoneCabinStuff/CONTAINER_FirstAidKitB","0",       "891.0700,160.7100,342.5940"},        //item = CONTAINER_FirstAidKitB p=891.0700,160.7100,342.5940 r=270.3867,127.7562,309.6633 c=100

            {"false",   "CrashMountainRegion",  "STR_StoneCabinAShutterRight_Prefab",   "175" ,     "0"},
            {"false",   "CrashMountainRegion",  "STR_StoneCabinAShutterLeft_Prefab",    "175" ,     "0"},
            {"false",   "CoastalRegion",        "INTERACTIVE_BedMattressB",             "98",       "357.2500,203.2560,1155.4300"},     // p=357.2500,203.2560,1155.4300 r=0.0000,97.6586,0.0000 c=100
            {"false",   "CoastalRegion",        "CONTAINER_Cupboard",                   "43",       "354.138 203.691 1156.432"}         //
        };

        public static void ChangeObjcets()
        {   

            GameObject findTargetGO = new GameObject();

            // EY === Euler_Y
            float MHShutterRight_EY = 187.4753f;
            float MHShutterLeft_EY  = 129.4225f;
            float MHShutterRight_EY_Close = 175f;
            float MHShutterLeft_EY_Close  = 175f;
            float MHShutterRight_EY_Open  = 305f;
            float MHShutterLeft_EY_Open   = 45f;

            //float MHShutterRightlocal_EY = 102.0222f;       
            //float MHShutterLeftlocal_EY = 43.9695f;         

            Vector3 MHFAkitPos = new Vector3(891.0700f, 160.7100f, 342.5940f);
            Quaternion MHFAkitRot = Quaternion.Euler(270.3867f, 127.7562f, 309.6633f);

            /*
             * struct
             * 
            fL_.Add(new fixdata { Delete = false, Scene = "CrashMountainRegion", Name = "STR_StoneCabinAShutterRight_Prefab", EulerY = 175  });
            fL_.Add(new fixdata { Delete = false, Scene = "CrashMountainRegion", Name = "STR_StoneCabinAShutterLeft_Prefab", EulerY = 175 });
            //item = INTERACTIVE_BedMattressB p = 890.3362,160.6400,341.8036 r = 0.0000,90.3298,0.0000 c = 100
            fL_.Add(new fixdata { Delete = false, Scene = "CrashMountainRegion", Name = "INTERACTIVE_BedMattressB", EulerY = 0 });
            //item = INTERACTIVE_BedMattressB p = 890.7637,160.6400,343.6804 r = 0.0000,172.8061,0.0000 c = 100
            fL_.Add(new fixdata { Delete = true,  Scene = "CrashMountainRegion", Name = "INTERACTIVE_BedMattressB", EulerY = 0 });
            */

            for (int i = 1; i < itemDataArray.GetLength(0); i++)
            {
                // ----- Find Name -----------------------------------------------------------------
                if (GameObject.Find(itemDataArray[i, 2]) == null) 
                { 
                    //MelonLogger.Msg("ChangeObjcet is null.");
                    continue;
                }
                else
                {
                    findTargetGO = GameObject.Find(itemDataArray[i,2]); 
                    // GameObject.Find cannot find for already inactive game objects. So this needs to be reloaded after confermation.
                }
                // -------------------------------------------------------------------------------------


                if (findTargetGO != null)
                {
                    // ----- Scene check -----------------------------------------------------------------
                    if (findTargetGO.scene.name != itemDataArray[i,1]) // Scene 
                    {
                        //MelonLogger.Msg("Scene name does not match.");
                        continue; 
                    }
                    // -------------------------------------------------------------------------------------

                    /*
                    string nameposi = findTargetGO.transform.name +"_"+ findTargetGO.transform.position.x +"_"+ findTargetGO.transform.position.y + "_" + findTargetGO.transform.position.z;
                    MelonLogger.Msg(findTargetGO.name + " : for , i = " + i + " - " + nameposi);

                    Transform fTGOTrf = findTargetGO.transform.parent;
                    if (fTGOTrf == null)
                    {
                        //MelonLogger.Msg(" ---------- gearsTrf null");
                        continue;
                    }
                    */

                    // ==============================================================================================================
                    // Timberwolf Mountain, CrashMountainRegion 
                    // ==============================================================================================================

                    if (itemDataArray[i, 1]== "CrashMountainRegion") { 

                        // Windows --------------------------------------------------------------------------------
                        
                        if (itemDataArray[i,2] == "STR_StoneCabinAShutterRight_Prefab" ) // Window Right  
                        {
                            switch (Settings.options.TMMHWindows) 
                            {
                                case 0: //default
                                    findTargetGO.transform.rotation = Quaternion.Euler(0, MHShutterRight_EY, 0);
                                    break;
                                case 1: //open
                                    findTargetGO.transform.rotation = Quaternion.Euler(0, MHShutterRight_EY_Open, 0); 
                                    break;
                                case 2: //close
                                    findTargetGO.transform.rotation = Quaternion.Euler(0, MHShutterRight_EY_Close, 0); 
                                    break;
                            }
                        }
                        if (itemDataArray[i, 2] == "STR_StoneCabinAShutterLeft_Prefab") // Window Left  
                        {
                            switch (Settings.options.TMMHWindows)
                            {
                                case 0: //default
                                    findTargetGO.transform.rotation = Quaternion.Euler(0, MHShutterLeft_EY, 0);
                                    break;
                                case 1: //open
                                    findTargetGO.transform.rotation = Quaternion.Euler(0, MHShutterLeft_EY_Open, 0);
                                    break;
                                case 2: //close
                                    findTargetGO.transform.rotation = Quaternion.Euler(0, MHShutterLeft_EY_Close, 0);
                                    break;
                            }
                        }
                        // ------------------------------------------------------------------------------------------


                        // FirstAidKitB ------------------------------------------------------------------------------------------
                        if (itemDataArray[i, 2] == "StoneCabinStuff/CONTAINER_FirstAidKitB") 
                        {
                            if (Settings.options.TMMHRemoveFAKit)
                            { findTargetGO.SetActive(false); }
                            else
                            { findTargetGO.SetActive(true); }

                            if (Settings.options.TMMHMoveFAKit)
                            {
                                findTargetGO.transform.position = new Vector3(
                                    Settings.options.TMMHFAKPosX, 
                                    Settings.options.TMMHFAKPosY, 
                                    Settings.options.TMMHFAKPosZ 
                                );
                                findTargetGO.transform.rotation = Quaternion.Euler( 
                                    Settings.options.TMMHFAKRotX,
                                    Settings.options.TMMHFAKRotY, 
                                    Settings.options.TMMHFAKRotZ 
                                );
                            }
                            else
                            {
                                findTargetGO.transform.position = MHFAkitPos; //MHFAkitPos.x = 891.0700f; MHFAkitPos.y = 160.7100f; MHFAkitPos.z = 342.5940f;
                                findTargetGO.transform.rotation = MHFAkitRot; //Quaternion.Euler(270.3867f, 127.7562f, 309.6633f);
                            }
                        }
                        // ------------------------------------------------------------------------------------------


                        // Bed MH ------------------------------------------------------------------------------------------
                        if (itemDataArray[i, 2] == "INTERACTIVE_BedMattressB")
                        {
                            if (Settings.options.TMMHRemoveBeds && findTargetGO.scene.name == itemDataArray[i,1])
                            { findTargetGO.SetActive(false); }
                            else
                            { findTargetGO.SetActive(true); }
                        }
                        // ------------------------------------------------------------------------------------------
                    

                        // WorkBench ------------------------------------------------------------------------------------------
                        if (itemDataArray[i, 2] == "INTERACTIVE_WorkBench")
                        {
                            if (Settings.options.TMMHRemoveWorkbench) 
                            { findTargetGO.SetActive(false); }
                            else 
                            { findTargetGO.SetActive(true); }
                        }
                        // ------------------------------------------------------------------------------------------


                        // Roof ------------------------------------------------------------------------------------------
                        if (itemDataArray[i, 2] == "STR_StoneCabinATrapDoor")
                        {
                            if (Settings.options.TMMHCloseRoof) { 
                                if (GameObject.Find("STR_StoneCabinATrapDoor(Clone)")) 
                                {
                                    continue; 
                                }
                                else 
                                {
                                    GameObject cloneObject = Instantiate(  // #Instantiate: clone to gameobject (MonoBehaviour)
                                        findTargetGO, 
                                        new Vector3(887f, 164.7f, 343.2f), 
                                        Quaternion.Euler(0f, 175f, 325f)
                                        );
                                    cloneObject.transform.localScale = new Vector3(3f, 0.5f, 2f);
                                }
                            }
                            else
                            {
                                if (GameObject.Find("STR_StoneCabinATrapDoor(Clone)"))
                                {
                                    Destroy(GameObject.Find("STR_StoneCabinATrapDoor(Clone)"),0.5f);
                                }
                            }
                        }
                        // ------------------------------------------------------------------------------------------

                        // Indoor Temp ------------------------------------------------------------------------------------------
                        if (itemDataArray[i, 2] == "STR_StoneCabinA_Prefab/TRIGGER_IndoorSpace")
                        {
                            findTargetGO.GetComponent<IndoorSpaceTrigger>().m_TemperatureDeltaCelsius = Settings.options.TMMHTemp;
                        }
                        // ------------------------------------------------------------------------------------------

                    }

                    // ==============================================================================================================
                    // Coastal Highway 
                    // ==============================================================================================================
                    if (itemDataArray[i, 1]== "CoastalRegion") { 

                        // Bed CH_AL ------------------------------------------------------------------------------------------

                        if (itemDataArray[i, 2] == "INTERACTIVE_BedMattressB")
                        {
                        
                            if (Settings.options.CHALRemoveBeds && findTargetGO.scene.name == itemDataArray[i, 1])
                            { findTargetGO.SetActive(false); }
                            else
                            { findTargetGO.SetActive(true); }
                        }
                        // ------------------------------------------------------------------------------------------

                        // Cupboard ------------------------------------------------------------------------------------------
                        if (itemDataArray[i, 2] == "CONTAINER_Cupboard")
                        {
                            if (Settings.options.CHALRemoveCupBoard)
                            { findTargetGO.SetActive(false); }
                            else
                            { findTargetGO.SetActive(true); }
                        }
                        // ------------------------------------------------------------------------------------------
                    }
                }

            }


            /*
            foreach (var d in fL_)
            {
                findTargetGO = GameObject.Find(d.Name);
                //Transform trf = findTargetGO.transform;

                if (findTargetGO != null)
                {
                    d.Delete = false;

                    if(findTargetGO.scene.name == d.Scene) { }

                    if (d.Delete == false ) { 
                        findTargetGO.SetActive(false);
                    }
                    else { 
                        findTargetGO.transform.rotation = Quaternion.Euler(0, d.EulerY, 0);
                    }
                }
            }



            */

        }

    }

}