﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape3_2 : Move {

	void Awake(){
		base.Awake ();
		base.init (new int[]{ 0, 2, 0 }, new int[]{0, 2, 0}, 2);
	}

}
