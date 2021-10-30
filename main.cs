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
        bool hazards;

        Keys sirenToggleKey, beamToggleKey, interiorLightToggleKey, leftIndicatorKey, rightIndicatorKey, hazardsKey;
        GTA.Control sirenToggleButton, beamToggleButton, leftIndicatorButton, rightIndicatorButton, hazardsButton;

        public Main()
        {
            this.Tick += OnTick;
            this.KeyDown += OnKeyDown;

            ScriptSettings config = ScriptSettings.Load("scripts\\EVLC_Settings.ini");

            #region Keys
            sirenToggleKey = config.GetValue<Keys>("Emergency Vehicles", "Siren_Toggle_Key", Keys.Tab);
            beamToggleKey = config.GetValue<Keys>("Headlights", "Beam_Toggle_Key", Keys.CapsLock);
            interiorLightToggleKey = config.GetValue<Keys>("Interior", "Interior_Light_Toggle_Key", Keys.I);
            leftIndicatorKey = config.GetValue<Keys>("Indicators", "Left_Indicator_key", Keys.Left);
            rightIndicatorKey = config.GetValue<Keys>("Indicators", "Right_Indicator_Key", Keys.Right);
            hazardsKey = config.GetValue<Keys>("Indicators", "Hazard_Lights_Key", Keys.Down);
            #endregion

            #region Buttons
            sirenToggleButton = config.GetValue<GTA.Control>("Emergency Vehicles", "Siren_Toggle_Button", GTA.Control.ScriptPadDown);
            beamToggleButton = config.GetValue<GTA.Control>("Headlights", "Beam_Toggle_Button", GTA.Control.ScriptRLeft);
            leftIndicatorButton = config.GetValue<GTA.Control>("Indicators", "Left_Indicator_Button", GTA.Control.ScriptPadLeft);
            rightIndicatorButton = config.GetValue<GTA.Control>("Indicators", "Right_Indicator_Button", GTA.Control.ScriptPadRight);
            hazardsButton = config.GetValue<GTA.Control>("Indicators", "Hazard_Lights_Button", GTA.Control.ScriptRB);
            #endregion
        }

        private void OnTick(object sender, EventArgs e)
        {
            string modName = "Enhanced Vehicle Lighting Controls";
            string version = "PreRelease v0.3.0";
            string developer = "MccDev260";

            if (firstTime)
            {
                Notification.Show(NotificationIcon.Blocked, modName, developer, version + " " + "loaded!!", false, true);
                firstTime = false;
            }

            if (Game.LastInputMethod == InputMethod.GamePad && playerCharacter.CurrentVehicle != null)
                GamePad();
        }

        #region Input
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
                    ToggleRightIndicator();

                if (e.KeyCode == leftIndicatorKey)
                    ToggleLeftIndicator();

                if (e.KeyCode == hazardsKey)
                    ToggleHazards();
            }
        }

        private void GamePad()
        {
            if (Game.IsControlJustReleased(sirenToggleButton))
                ToggleSiren();

            if (Game.IsControlJustReleased(beamToggleButton))
                ToggleFullBeams();

            if (Game.IsControlJustPressed(leftIndicatorButton))
                ToggleLeftIndicator();

            if (Game.IsControlJustPressed(rightIndicatorButton))
                ToggleRightIndicator();

            if (Game.IsControlJustPressed(hazardsButton))
                ToggleHazards();
        }
        #endregion

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

        #region Indicators
        private void ToggleHazards()
        {
            hazards = !hazards;
            SetIndicators(hazards, hazards);
        }

        private void ToggleRightIndicator()
        {
            if (leftIndicator)
                ToggleLeftIndicator();
            
            rightIndicator = !rightIndicator;
            SetIndicators(false, rightIndicator);
        }

        private void ToggleLeftIndicator()
        {
            if (rightIndicator)
                ToggleRightIndicator();

            leftIndicator = !leftIndicator;
            SetIndicators(leftIndicator);
        }

        private void SetIndicators(bool leftIndicator = false, bool rightIndicator = false)
        {
            playerCharacter.CurrentVehicle.IsLeftIndicatorLightOn = leftIndicator;
            playerCharacter.CurrentVehicle.IsRightIndicatorLightOn = rightIndicator;
        }
        #endregion
    }
}
