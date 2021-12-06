using System;
using MelonLoader;
using UnityEngine;
using ComfyUtils;
using VRC;

[assembly: MelonInfo(typeof(SimplyTPose.SimplyTPose), "SimplyTPose", "0.1", "Boppr")]
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
                    Animator animator = PlayerManager.field_Private_Static_PlayerManager_0.field_Private_Player_0.transform.Find("ForwardDirection").Find("Avatar").GetComponent<Animator>();
                    animator.enabled = !animator.enabled;
                }
                catch { }
            }
        }
    }
}
