using HarmonyLib;
using Verse;

namespace DeathSentence
{
	public class DeathSentenceMod : Mod
	{
		public DeathSentenceMod(ModContentPack pack) : base(pack)
		{
			new Harmony("DeathSentenceMod").PatchAll();
		}
	}
}