using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Character
{
	public CellCollider cell;
	[HideInInspector] public int maxHp;

	override public void die() {
		cell.unitDies();
		Destroy(gameObject);
	}

	public void setCellCollider(CellCollider cc) {
		cell=cc;
	}

	virtual protected void Start() {
		maxHp=hp;
	}
}
