using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape2_4 : Move {

	void Awake(){
		base.Awake ();
		base.init (new int[]{ 0, 2, 2 }, new int[]{0, 2, 1}, 3);

	}

}
