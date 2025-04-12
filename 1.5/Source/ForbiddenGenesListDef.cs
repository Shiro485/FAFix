using System.Collections.Generic;
using Verse;
using RimWorld;

namespace FAFix
{
    public class ForbiddenGenesListDef : Def
    {
        public List<GeneDef> genes;
    }

    [DefOf]
    public static class ForbiddenGenesListDefOf
    {
        public static ForbiddenGenesListDef ForbiddenGenesList;
    }
}