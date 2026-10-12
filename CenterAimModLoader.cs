using Engine;
using HarmonyLib;

namespace Game {
    public class CenterAimModLoader : ModLoader {
        public const string PackageName = "user.CenterAimMod";

        public override void __ModInitialize() {
            Harmony harmony = new Harmony(PackageName);
            harmony.PatchAll();
        }
    }
}