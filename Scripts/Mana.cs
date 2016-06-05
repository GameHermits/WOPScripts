/*Class: Mana
 * Requires: None
 * Provides: Mana points of the player that can be changed in any context of any class due to it's static property.
 * Definition: this class isn't attached to any game object. it just serve to indecate the maximum mana of the player in each level and to provide the current mana points of the player at all times. these two
 * variables can be modified in any context of any class.
*/
using UnityEngine;
using System.Collections;

public static class Mana
{
	public static float maxMana = 1000;
	public static float mana = 1000;
}
