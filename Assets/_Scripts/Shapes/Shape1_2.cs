using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape1_2 : Move {

	void Awake(){
		base.Awake ();
		base.init (new int[]{ 0, 3, 0 }, new int[]{0, 3, 0}, 3, 0);
	}
		
}
