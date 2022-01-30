using NLog;
using Sandbox.Game.SessionComponents;
using Sandbox.Game.World;
using System;
using System.Reflection;
using Torch.Managers.PatchManager;

namespace ALE_DontShareProgression {

    [PatchShim]
    public static class ALE_MySessionComponentResearchPatch {

        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        internal static readonly MethodInfo unlock =
            typeof(MySessionComponentResearch).GetMethod("ShareResearch", BindingFlags.Static | BindingFlags.NonPublic) ??
            throw new Exception("Failed to find method to patch");

        internal static readonly MethodInfo unlockPatch =
            typeof(ALE_MySessionComponentResearchPatch).GetMethod(nameof(ShareResearchPatch), BindingFlags.Static | BindingFlags.Public) ??
            throw new Exception("Failed to find patch method");

        public static void Patch(PatchContext ctx) {

            ctx.GetPattern(unlock).Prefixes.Add(unlockPatch);

            Log.Debug("Patching Successful MySessionComponentResearchPatch!");
        }

        public static bool ShareResearchPatch(long toIdentity, long fromIdentityId) {

            ALE_DontShareProgressionConfig config = ALE_DontShareProgressionPlugin.Instance.Config;

            if (config.Enabled) {

                MyIdentity fromPlayer = MySession.Static.Players.TryGetIdentity(fromIdentityId);
                MyIdentity toPlayer = MySession.Static.Players.TryGetIdentity(toIdentity);

                Log.Info("Blocked " + fromPlayer?.DisplayName + ", from sharing Research with " + toPlayer?.DisplayName);

                return false;
            }

            return true;
        }
    }
}
