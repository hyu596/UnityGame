using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape3 : Move {

	void Awake(){
		base.Awake ();
		base.init (new int[]{ 1, 1, 0 }, 2);
	}

}
