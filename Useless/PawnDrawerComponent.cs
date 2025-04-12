using RimWorld;
using Verse;

namespace NotifyOnLoad
{
    public class PawnDrawerComponent : MapComponent
    {
        public PawnDrawerComponent(Map map) : base(map)
        {
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            HelloLoadGameComponent gameComponent = Current.Game.GetComponent<HelloLoadGameComponent>();
            if (gameComponent != null)
            {
                foreach (Pawn pawn in map.mapPawns.AllPawnsSpawned)
                {
                    gameComponent.CheckAndUpdatePawn(pawn);
                }
            }
        }
    }
}