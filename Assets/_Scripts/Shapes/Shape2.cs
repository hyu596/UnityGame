using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape2 : Move {

	void Awake(){
		base.Awake ();
		base.init (new int[]{ 1, 2, 0 }, 3);

	}

}
