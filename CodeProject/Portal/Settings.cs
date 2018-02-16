﻿using System.Diagnostics;
namespace Portal.Properties
{

    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings
    {
        public Settings()
        {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            //this.SettingChanging += this.SettingChangingEventHandler;
            //
            //this.SettingsSaving += this.SettingsSavingEventHandler;
            //
            SetWebApiApplicationSettings();
            SetDebugApplicationSettings();
            SetReleaseApplicationSettings();
        }


        [Conditional("WEBAPI")]
        private void SetWebApiApplicationSettings()
        {
            // Set the two Settings values to use the resource files designated
            // for the DEBUG version of the app:
            this["RunMode"] = "WEBAPI";
            //this.RunMode = "WEBAPI";
        }
        [Conditional("DEBUG")]
        private void SetDebugApplicationSettings()
        {
            // Set the two Settings values to use the resource files designated
            // for the DEBUG version of the app:
            this["RunMode"] = "DEBUG";
            //this.RunMode = "DEBUG";
        }

        [Conditional("RELEASE")]
        private void SetReleaseApplicationSettings()
        {
            // Set the two Settings values to use the resource files designated
            // for the DEBUG version of the app:
            this["RunMode"] = "RELEASE";
            //this.RunMode = "RELEASE";
        }
    }
}