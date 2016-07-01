/*Class:
 * Requires:
 * Provides: 
 * Definition:
*/
using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class Inventory : MonoBehaviour
{

	public  Sprite empty_Sprite;
	//empty image to put it in the inventory when its clear
	public  Image[] bag;
	//the array of items images in the inventory
	private int Ibag = 0;
	//the indexer that hold the number of item in the bag
	private int IMAXbag = 6;
	//the maximum size of the bag

	private static Inventory inv;

	public static Inventory IN {
		get {
			if (inv == null)
				inv = Inventory.FindObjectOfType<Inventory> ();
			return Inventory.inv;
		}
	}

	public void AddItem (Sprite item)
	{//to add item in the inventory type inventory.IN.AddItem(the item Sprite);
		if (Ibag <= IMAXbag && bag [Ibag].sprite == empty_Sprite) {
			bag [Ibag].sprite = item;
			Ibag++;
			SortItem ();
		} else {//if the bag was full from down check else element from above
			for (int i = 0; i < IMAXbag; i++) {
				if (bag [i].sprite == empty_Sprite) {
					bag [i].sprite = item;
					Ibag++;
					break;
				}
			}
			SortItem ();
		}

	}

	public bool CanUseItem (Sprite item)
	{//to check if the item is in the bag and use it after type inventory.IN.CanUseItem(the item Sprite);
		for (int i = 0; i < bag.Length; i++) {
			if (item == bag [i].sprite) {
				bag [i].sprite = empty_Sprite;
				Ibag--;
				SortItem ();
				return true;
			}
		}
		return false;
	}

	public void RemoveItemNum (int index)
	{//to use or delete item with spicific index type inventory.IN.RemoveItemNum(the index in bag);
		if (index <= IMAXbag) {
			bag [index].sprite = empty_Sprite;
			Ibag--;
			SortItem ();
		}
	}

	public void RemoveItem (Sprite icon)
	{//to remove item with its sprite type inventory.IN.RemoveItem(the item Sprite);
		for (int i = 0; i < IMAXbag; i++) {
			if (bag [i].sprite == icon) {
				bag [i].sprite = empty_Sprite;
				Ibag--;
				break;
			}
		}
		SortItem ();
	}

	public void RemoveLastItem ()
	{//to remove last item in the bag type inventory.IN.RemoveLastItem();
		if (bag [Ibag].sprite != empty_Sprite) {
			bag [Ibag].sprite = empty_Sprite;
			Ibag--;
			SortItem ();
		} else {
			for (int i = IMAXbag - 1; i >= 0; i--) {
				if (bag [i].sprite != empty_Sprite) {
					bag [i].sprite = empty_Sprite;
					Ibag--;
					break;
				}
			}
			SortItem ();
		}
	}

	public void RemoveAllItem ()
	{//to empty the bag type inventory.IN.RemoveAllItem();
		for (int i = 0; i < bag.Length; i++) {
			bag [i].sprite = empty_Sprite;
		}
		Ibag = 0;
	}

	public void SortItem ()
	{//to sort items after using one and fill the empty places
		if (Ibag > 0) {
			for (int i = 0; i < bag.Length - 1; i++) {
				if (bag [i].sprite == empty_Sprite) {
					for (int k = i + 1; k < bag.Length; k++) {
						if (bag [k].sprite != empty_Sprite && bag [i].sprite == empty_Sprite) {
							bag [i].sprite = bag [k].sprite;
							bag [k].sprite = empty_Sprite;
							Ibag = i;
						}
					}
				}
			}
		}
	}
}
