using UnityEngine;
using System.Collections;

public class SupportCharacter
{
	//Public:
	//In Game State
	public int in_NumberOfUses = 3;
	public bool SuppState = true;
	//Static Support State
	public float fl_SuppLevel = 1;

	public bool CanUse ()
	{
		if (in_NumberOfUses == 0)
			SuppState = false;
		else
			SuppState = true;
		return SuppState;
	}
}
