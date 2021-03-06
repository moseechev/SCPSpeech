﻿using HarmonyLib;
using UnityEngine;

namespace SCP_Speech
{
	[HarmonyPatch(typeof(Intercom), "RequestTransmission")]
	[HarmonyPatch(new[] { typeof(GameObject) })]
	public class RequestTransmissionOverride
	{
		public static bool Prefix(Intercom __instance, GameObject spk)
		{
			if (spk != null)
				return true;

			if (Intercom.host.speaker == null)
				return true;			

			CharacterClassManager ccm = Intercom.host.speaker.GetComponent<CharacterClassManager>();
			if (!ccm.CurClass.Is939())
				return true;
			Scp939PlayerScript script = Intercom.host.speaker.GetComponent<Scp939PlayerScript>();

			if (!script.NetworkusingHumanChat)
				__instance.Networkspeaker = null;
			return false;
		}
	}
}
