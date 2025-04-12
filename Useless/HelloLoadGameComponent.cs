using RimWorld;
using Verse;
using System.Collections.Generic;

namespace NotifyOnLoad
{
    public class HelloLoadGameComponent : GameComponent
    {
        public Dictionary<int, bool> pawnDictionary = new Dictionary<int, bool>();
        public List<GeneDef> targetGenes = new List<GeneDef>();

        public HelloLoadGameComponent(Game game)
        {
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            Log.Message("Hello World - Game Loaded");

            // Initialize your list of target genes here
            targetGenes.Add(DefDatabase<GeneDef>.GetNamed("Hemogenic")); // Replace with actual genes
        }
        public void CheckAndUpdatePawn(Pawn pawn)
        {
            if (!pawnDictionary.ContainsKey(pawn.thingIDNumber))
            {
                bool hasTargetGene = false;
                foreach (var gene in targetGenes)
                {
                    if (pawn.genes?.HasActiveGene(gene) == true)
                    {
                        Log.Message("Yuck a Sanguophage");
                        hasTargetGene = true;
                        break;
                    }
                }
                pawnDictionary[pawn.thingIDNumber] = !hasTargetGene;
            }
        }
    }
}
