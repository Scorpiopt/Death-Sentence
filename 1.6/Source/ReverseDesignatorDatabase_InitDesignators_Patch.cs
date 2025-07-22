using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace DeathSentence
{
    [HarmonyPatch(typeof(ReverseDesignatorDatabase), "InitDesignators")]
    public static class ReverseDesignatorDatabase_InitDesignators_Patch
    {
        public static void Postfix(ref List<Designator> ___desList)
        {
            ___desList.Add(new Designator_ExecuteDownedEnemy());
        }
    }
}