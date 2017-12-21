using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape2_3 : Move {

	void Awake(){
		base.Awake ();
		base.init (new int[]{ 0, 2, 1 }, new int[]{0, 2, 1}, 3, 5);

	}

}
