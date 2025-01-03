using RimWorld;
using Verse;

namespace DeathSentence
{
	public static class ExecutionUtility
	{
		public static bool CanBeExecutedByColony(Thing t)
		{
			if (t is not Pawn pawn)
			{
				return false;
			}
			if (pawn.Faction == null || pawn.Faction.IsPlayer)
			{
				return false;
			}
			if (!pawn.Downed)
			{
				return false;
			}
			
			return pawn.HostileTo(Faction.OfPlayer);
		}
	}
}