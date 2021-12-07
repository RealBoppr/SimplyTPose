using System;
using MelonLoader;
using UnityEngine;
using ComfyUtils;
using VRC;

[assembly: MelonInfo(typeof(SimplyTPose.SimplyTPose), "SimplyTPose", "0.2", "Boppr")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace SimplyTPose
{
    public class Config
    {
        public string PressKeyBind { get; set; } = "T";
        public string HoldKeyBind { get; set; } = "LeftAlt";
    }
    public class SimplyTPose : MelonMod
    {
        private static Config Config => Helper.Config;
        private static ConfigHelper<Config> Helper;
        private static KeyCode PressKeyBind;
        private static KeyCode HoldKeyBind;
        public override void OnApplicationStart()
        {
            Helper = new ConfigHelper<Config>($"{Environment.CurrentDirectory}\\UserData\\SimplyTPoseConfig.json");
            Helper.OnConfigUpdated += new Action(delegate () { SetKeyBind(); MelonLogger.Msg("[Config Updated]"); });
            SetKeyBind();
        }
        private void SetKeyBind()
        {
            PressKeyBind = Enum.TryParse(Config.PressKeyBind, out PressKeyBind) ? PressKeyBind : KeyCode.T;
            HoldKeyBind = Enum.TryParse(Config.HoldKeyBind, out HoldKeyBind) ? HoldKeyBind : KeyCode.LeftAlt;
        }
        public override void OnUpdate()
        {
            if (Input.GetKey(HoldKeyBind) && Input.GetKeyDown(PressKeyBind))
            {
                try
                {
                    Animator animator = Player.prop_Player_0._vrcplayer.field_Internal_Animator_0;
                    animator.enabled = !animator.enabled;
                }
                catch { }
            }
        }
    }
}
