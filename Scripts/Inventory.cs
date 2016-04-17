using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;
public class Inventory : MonoBehaviour{

	public  Sprite empty_Sprite;
	public  Image[] bag;
	private int Ibag = 0;
	private int IMAXbag = 6;

	private static Inventory inv ;
	public static Inventory IN {
		get {
			if(inv == null)
				inv = Inventory.FindObjectOfType<Inventory>();
			return Inventory.inv;
		}
	}

	public void AddItem (Sprite item){
		if (Ibag <= IMAXbag && bag [Ibag].sprite == empty_Sprite) {
			bag [Ibag].sprite = item;
			Ibag++;

		} else {
			for (int i = 0; i < IMAXbag; i++) {
				if (bag[i].sprite == empty_Sprite) {
					bag [i].sprite = item;
					Ibag++;
					break;
				}
			}
		}

	}

	public bool CanUseItem (Sprite item){
		for (int i = 0; i < bag.Length; i++) {
			if (item == bag[i].sprite) {
				bag [i].sprite = empty_Sprite;
				Ibag--;
				return true;
			}
		}
		return false;
	}

	public void RemoveItemNum (int index){
		if (index <= IMAXbag) {
			bag [index].sprite = empty_Sprite;
			Ibag--;
		}
	}

	public void RemoveItem (Sprite icon){
		for (int i = 0; i < IMAXbag; i++) {
			if (bag[i].sprite == icon) {
				bag [i].sprite = empty_Sprite;
				Ibag--;
				break;
			}
		}
	}
		
	public void RemoveLastItem(){
		if (bag[Ibag].sprite != empty_Sprite) {
			bag [Ibag].sprite = empty_Sprite;
			Ibag--;
		} else {
			for (int i = IMAXbag-1; i >= 0; i--) {
				if (bag[i].sprite != empty_Sprite) {
					bag [i].sprite = empty_Sprite;
					Ibag--;
					break;
				}
			}
		}
	}

	public void RemoveAllItem (){
		for (int i = 0; i < bag.Length; i++) {
			bag [i].sprite = empty_Sprite;
		}
		Ibag = 0;
	}
}
