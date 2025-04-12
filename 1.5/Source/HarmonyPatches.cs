using HarmonyLib;
using System;
using System.Reflection;
using RimWorld;
using Verse;
using System.Collections.Generic;

namespace FAFix
{
    [StaticConstructorOnStartup]
    public static class MyPatch
    {
        static MyPatch()
        {
            Harmony.DEBUG = true;
            Log.Message("MyPatch static constructor called.");
            var harmony = new Harmony("whitelight.notifyonload");

            try{
                // Get the internal class type using AccessTools.TypeByName
                var internalClassType = AccessTools.TypeByName("FacialAnimation.DrawFaceGraphicsComp");
                if (internalClassType == null)
                {
                    Log.Message("Internal class not found.");
                    return;
                }

                // Get the internal method using reflection
                var internalMethod = AccessTools.Method(internalClassType, "CheckEnableDrawing");
                if (internalMethod == null)
                {
                    Log.Message("Internal method not found.");
                    return;
                }

                // Patch the internal method
                harmony.Patch(internalMethod, new HarmonyMethod(typeof(MyPatch).GetMethod(nameof(Prefix), BindingFlags.NonPublic | BindingFlags.Static)));
                Log.Message("Patch applied successfully.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error applying patch: {ex.Message}");
            }
        }

        private static bool Prefix(Pawn pawn, ref bool __result)
        {
            if (pawn == null)
            {
                __result = false;
                return false;
            }
            if (pawn.gender == Gender.Female && !FacialAnimation.FacialAnimationMod.Settings.EnableFemaleDrawing)
            {
                __result = false;
                return false;
            }
            if (pawn.gender == Gender.Male && !FacialAnimation.FacialAnimationMod.Settings.EnableMaleDrawing)
            {
                __result = false;
                return false;
            }
            if (pawn.gender == Gender.None && !FacialAnimation.FacialAnimationMod.Settings.EnableNeitherDrawing)
            {
                __result = false;
                return false;
            }
            if(HasForbiddenGenes(pawn)){
                __result = false;
                return false;
            }
            return true;
        }

        private static bool HasForbiddenGenes(Pawn pawn)
        {
            ForbiddenGenesListDef ForbiddenGenesList = ForbiddenGenesListDefOf.ForbiddenGenesList;
            List<GeneDef> targetGenes = ForbiddenGenesList.genes;
            foreach (var gene in targetGenes)
            {
                if (pawn.genes?.HasActiveGene(gene) == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}