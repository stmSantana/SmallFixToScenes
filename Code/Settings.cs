using ModSettings;
using UnityEngine;
using MelonLoader;

using HarmonyLib;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace SmallFixToScenes
{
    internal class SmallFixSettings : JsonModSettings
    {
        [Section("Timberwolf Mountain Mountaineer's hut")]

        [Name("Change the indoor temperature")]
        [Description("The value of temperature added to the outdoor air temperature (Celsius.Default is 10).")]
        [Slider(0, 50, 51)]
        public float TMMHTemp = 10f;

        [Name("Close the hole in the roof")]
        [Description("True(Close) / False(Open).")]
        public bool TMMHCloseRoof = false;

        [Name("Windows")]
        [Description("Default / Open / Close")]
        [Choice("Default", "Open", "Close")]
        public int TMMHWindows = 0;

        [Name("Remove beds")]
        [Description("Remove two beds. It is necessary to reload the file after the change.")]
        public bool TMMHRemoveBeds = false;

        [Name("Remove workbench")]
        [Description("Remove a workbench. It is necessary to reload the file after the change.")]
        public bool TMMHRemoveWorkbench = false;

        [Name("Remove FirstAidKit")]
        [Description("Remove a first aid kit. It is necessary to reload the file after the change.")]
        public bool TMMHRemoveFAKit = false;

        [Name("Move FirstAidKit")]
        [Description("Change the position of a first aid kit.")]
        public bool TMMHMoveFAKit = false;

        [Name("FirstAidKit position X")]
            [Description("position of a first aid kit. Default = 891.0700f")]
            [Slider(880,900)]
            public float TMMHFAKPosX = 891.0700f;
        [Name("FirstAidKit position Y")]
            [Description("position of a first aid kit. Default = 160.7100f")]
            [Slider(160, 170)]
            public float TMMHFAKPosY = 161.7100f;
        [Name("FirstAidKit position Z")]
            [Description("position of a first aid kit. Default = 342.5940f")]
            [Slider(330, 350)]
            public float TMMHFAKPosZ = 342.5940f;

        [Name("FirstAidKit rotation X")] 
            [Description("position of a first aid kit. Default = 270.3867f")]
            [Slider(0, 360)]
            public float TMMHFAKRotX = 270.3867f;
        [Name("FirstAidKit rotation Y")]
            [Description("position of a first aid kit. Default = 127.7562f")]
            [Slider(0, 360)]
            public float TMMHFAKRotY = 127.7562f;
        [Name("FirstAidKit rotation Z")]
            [Description("position of a first aid kit. Default = 309.6633f")]
            [Slider(0, 360)]
            public float TMMHFAKRotZ = 309.6633f;

        [Section("Coastal Highway - Abandoned Lookout")]

        [Name("Remove beds")]
        [Description("Remove a bed. It is necessary to reload the file after the change.")]
        public bool CHALRemoveBeds = false;

        [Name("Remove Cupboard")]
        [Description("Remove a cupboard. It is necessary to reload the file after the change.")]
        public bool CHALRemoveCupBoard = false;


        protected override void OnConfirm()
        {
            base.OnConfirm();

            Patches.ChangeObjcets();
        }

    }

    internal static class Settings
    {
        public static SmallFixSettings options;

        public static void OnLoad()
        {
            options = new SmallFixSettings();
            options.AddToModSettings("Small Fix To Scenes Settings", MenuType.Both);
        }
    }

}
