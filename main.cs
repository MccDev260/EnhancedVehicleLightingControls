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
            string version = "PreRelease v0.2.0";
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
                if (playerCharacter.CurrentVehicle.HasSiren)
                {
                    if (e.KeyCode == sirenToggleKey)
                    {
                        isSirenSilent = !isSirenSilent;
                        playerCharacter.CurrentVehicle.IsSirenSilent = isSirenSilent;
                    }
                }

                if (playerCharacter.CurrentVehicle.AreLightsOn)
                {
                    if (e.KeyCode == beamToggleKey)
                        playerCharacter.CurrentVehicle.AreHighBeamsOn = !playerCharacter.CurrentVehicle.AreHighBeamsOn;
                }

                if (e.KeyCode == interiorLightToggleKey)
                    playerCharacter.CurrentVehicle.IsInteriorLightOn = !playerCharacter.CurrentVehicle.IsInteriorLightOn;

                if (e.KeyCode == rightIndicatorKey)
                {
                    rightIndicator = !rightIndicator;
                    playerCharacter.CurrentVehicle.IsRightIndicatorLightOn = rightIndicator;

                    if (leftIndicator)
                    {
                        leftIndicator = !leftIndicator;
                        playerCharacter.CurrentVehicle.IsLeftIndicatorLightOn = leftIndicator;
                    }
                }

                if (e.KeyCode == leftIndicatorKey)
                {
                    leftIndicator = !leftIndicator;
                    playerCharacter.CurrentVehicle.IsLeftIndicatorLightOn = leftIndicator;

                    if (rightIndicator)
                    {
                        rightIndicator = !rightIndicator;
                        playerCharacter.CurrentVehicle.IsRightIndicatorLightOn = rightIndicator;
                    }
                }
            }
        }
    }
}
