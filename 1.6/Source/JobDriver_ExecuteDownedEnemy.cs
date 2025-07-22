using RimWorld;
using Verse;
using Verse.AI;

namespace DeathSentence
{
	public class JobDriver_ExecuteDownedEnemy : JobDriver
	{
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(this.job.GetTarget(TargetIndex.A), this.job, 1, -1, null, errorOnFailed);
		}

		public override System.Collections.Generic.IEnumerable<Toil> MakeNewToils()
		{
			this.FailOnDespawnedOrNull(TargetIndex.A);
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
			yield return Toils_General.Wait(100, TargetIndex.A).WithProgressBarToilDelay(TargetIndex.A);
			yield return new Toil
			{
				initAction = delegate
				{
					Pawn target = (Pawn)this.job.GetTarget(TargetIndex.A);
					if (target != null && !target.Dead)
					{
						target.Kill(new DamageInfo(DamageDefOf.ExecutionCut, 99999, 999f, -1f, this.pawn));
					}
				},
				defaultCompleteMode = ToilCompleteMode.Instant
			};
		}
	}
}