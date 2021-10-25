using System;
using System.Windows.Forms;

using GTA;
using GTA.UI;

namespace EnhancedVehicleLightingControls
{
    public class Main : Script
    {
        bool firstTime = true;

        Ped playerCharacter = Game.Player.Character;
        bool isSirenSilent;
        bool leftIndicator, rightIndicator;

        Keys sirenToggleKey, beamToggleKey, interiorLightToggleKey, leftIndicatorKey, rightIndicatorKey;

        public Main()
        {
            this.Tick += OnTick;
            this.KeyDown += OnKeyDown;

            ScriptSettings config = ScriptSettings.Load("scripts\\EVLC_Settings.ini");
            
            sirenToggleKey = config.GetValue<Keys>("Emergency Vehicles", "Siren_Toggle_Key", Keys.Tab);
            beamToggleKey = config.GetValue<Keys>("All", "Beam_Toggle_Key", Keys.CapsLock);
            interiorLightToggleKey = config.GetValue<Keys>("All", "Interior_Light_Toggle_Key", Keys.I);
            leftIndicatorKey = config.GetValue<Keys>("All", "Left_Indicator_key", Keys.Left);
            rightIndicatorKey = config.GetValue<Keys>("All", "Right_Indicator_key", Keys.Right);

        }

        private void OnTick(object sender, EventArgs e)
        {
            string modName = "Enhanced Vehicle Lighting Controls";
            string version = "PreRelease v0.2.1";
            string developer = "MccDev260";

            if (firstTime)
            {
                Notification.Show(NotificationIcon.Blocked, modName, developer, version + " " + "loaded!!", false, true);
                firstTime = false;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {   
            if (playerCharacter.CurrentVehicle != null)
            {
                if (e.KeyCode == sirenToggleKey)
                    ToggleSiren();

                if (e.KeyCode == beamToggleKey)
                    ToggleFullBeams();

                if (e.KeyCode == interiorLightToggleKey)
                    ToggleInteriorLights();

                if (e.KeyCode == rightIndicatorKey)
                {
                    ToggleRightIndicator();

                    if (leftIndicator)
                        ToggleLeftIndicator();
                }

                if (e.KeyCode == leftIndicatorKey)
                {
                    ToggleLeftIndicator();

                    if (rightIndicator)
                        ToggleRightIndicator();
                }
            }
        }

        private void ToggleSiren()
        {
            if (playerCharacter.CurrentVehicle.HasSiren)
            {
                isSirenSilent = !isSirenSilent;
                playerCharacter.CurrentVehicle.IsSirenSilent = isSirenSilent;
            }
        }

        private void ToggleFullBeams()
        {
            if (playerCharacter.CurrentVehicle.AreLightsOn)
            {
                playerCharacter.CurrentVehicle.AreHighBeamsOn = !playerCharacter.CurrentVehicle.AreHighBeamsOn;
            }
        }

        private void ToggleInteriorLights()
        {
            playerCharacter.CurrentVehicle.IsInteriorLightOn = !playerCharacter.CurrentVehicle.IsInteriorLightOn;
        }

        private void ToggleRightIndicator()
        {
            rightIndicator = !rightIndicator;
            playerCharacter.CurrentVehicle.IsRightIndicatorLightOn = rightIndicator;
        }

        private void ToggleLeftIndicator()
        {
            leftIndicator = !leftIndicator;
            playerCharacter.CurrentVehicle.IsLeftIndicatorLightOn = leftIndicator;
        }
    }
}
