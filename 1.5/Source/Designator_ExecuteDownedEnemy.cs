using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using RimWorld;

namespace DeathSentence
{
    public class Designator_ExecuteDownedEnemy : Designator
    {
        public override int DraggableDimensions => 2;
        public override DesignationDef Designation => DesignationDefOf.ExecutionOrder;
        public Designator_ExecuteDownedEnemy()
        {
            defaultLabel = "DesignatorExecuteDownedEnemy".Translate();
            defaultDesc = "DesignatorExecuteDownedEnemyDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/ExecutionOrder");
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
            soundSucceeded = SoundDefOf.Designate_Claim;
            hotKey = KeyBindingDefOf.Misc12;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!c.InBounds(base.Map))
            {
                return false;
            }
            if (!DownedEnemiesInCell(c).Any())
            {
                return "MessageMustDesignateDownedEnemy".Translate();
            }
            return true;
        }

        public override void DesignateSingleCell(IntVec3 c)
        {
            foreach (Thing item in DownedEnemiesInCell(c))
            {
                DesignateThing(item);
            }
        }

        public override AcceptanceReport CanDesignateThing(Thing t)
        {
            if (base.Map.designationManager.DesignationOn(t, Designation) != null)
            {
                return false;
            }
            return ExecutionUtility.CanBeExecutedByColony(t);
        }

        public override void DesignateThing(Thing t)
        {
            base.Map.designationManager.AddDesignation(new Designation(t, Designation));
        }

        private IEnumerable<Thing> DownedEnemiesInCell(IntVec3 c)
        {
            if (c.Fogged(base.Map))
            {
                yield break;
            }
            List<Thing> thingList = c.GetThingList(base.Map);
            for (int i = 0; i < thingList.Count; i++)
            {
                if (CanDesignateThing(thingList[i]).Accepted && thingList[i] is Pawn pawn && pawn.Faction != null && !pawn.Faction.IsPlayer && pawn.Downed)
                {
                    yield return thingList[i];
                }
            }
        }
    }
}