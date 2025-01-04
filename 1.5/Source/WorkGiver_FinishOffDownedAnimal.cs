using RimWorld;
using Verse;
using Verse.AI;

namespace DeathSentence
{
	public class WorkGiver_FinishOffDownedAnimal : WorkGiver_Scanner
	{
		public override ThingRequest PotentialWorkThingRequest
		{
			get
			{
				return ThingRequest.ForGroup(ThingRequestGroup.Pawn);
			}
		}

		public override PathEndMode PathEndMode
		{
			get
			{
				return PathEndMode.Touch;
			}
		}

		public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return HasJobOn(pawn, t, forced);
		}

		public static bool HasJobOn(Pawn pawn, Thing t, bool forced)
		{
			Pawn target = t as Pawn;
			if (target == null || target == pawn)
			{
				return false;
			}
			if (!target.Downed)
			{
				return false;
			}
			if (target.RaceProps.Humanlike)
			{
				return false;
			}
			if (pawn != null)
			{
				if (!pawn.CanReserve(target, 1, -1, null, forced))
				{
					return false;
				}
			}
			return true;
		}

		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			Job job = JobMaker.MakeJob(DefsOf.FinishOffDownedAnimal, t);
			return job;
		}
	}
}