using Engine;
using HarmonyLib;

namespace Game {
    // Mira central no celular, igual ao PC.
    // 1) Nao trava a camera durante a mira (m_doAimBlockLook = false).
    // 2) O raio de mira passa a ser o do centro da tela.
    // No PC nada muda (IsControlledByTouch fica falso com o mouse).
    // Uso: segure o dedo ~0,2 s para mirar, arraste para olhar ao redor, solte para atirar.
    [HarmonyPatch(typeof(ComponentInput), nameof(ComponentInput.UpdateInputFromWidgets))]
    public static class CenterAimPatch {
        [HarmonyPostfix]
        public static void Postfix(ComponentInput __instance) {
            if (!__instance.IsControlledByTouch) {
                return;
            }
            ComponentPlayer player = __instance.m_componentPlayer;
            if (player == null) {
                return;
            }

            player.m_doAimBlockLook = false;

            PlayerInput input = __instance.m_playerInput;
            if (input.Aim.HasValue) {
                Camera camera = player.GameWidget.ActiveCamera;
                input.Aim = new Ray3(camera.ViewPosition, camera.ViewDirection);
                __instance.m_playerInput = input;
            }
        }
    }
}