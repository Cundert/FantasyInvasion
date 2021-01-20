using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollider : MonoBehaviour
{
	public GameController controller;
	public GameObject[] units;


	private bool occupied;
	private GameObject unit;
    private GameObject unit_temp;

    [HideInInspector] public float timeDiff;

    public void removeUnit()
    {
        if (occupied)
        {
			controller.refund(unit);
			unit.GetComponent<Unit>().die();
            controller.selectedUnit = GameController.unit.None;
        }
    }

	public void summonUnit() {
		int x = Mathf.FloorToInt(transform.position.x);
		x+=5;
		int z = Mathf.FloorToInt(transform.position.z);
		z=Mathf.Abs(z-1);
		int price=-1;
		float y_offset = 0;
        float x_offset = 0;
		switch (controller.selectedUnit) {
			case GameController.unit.Chest:
                unit_temp = units[0];
				price=controller.unitPrices[0];
                y_offset = 0.36f;
				break;
			case GameController.unit.Dragon:
                unit_temp = units[1];
				price=controller.unitPrices[1];
				break;
			case GameController.unit.Knight:
                unit_temp = units[2];
				price=controller.unitPrices[2];
				break;
            case GameController.unit.Explosive:
                unit_temp = units[3];
                price = controller.unitPrices[3];
                x_offset = 0.2f;
                y_offset = 0.1f;
                break;
            case GameController.unit.IceDragon:
                unit_temp = units[4];
                price = controller.unitPrices[4];
                break;
			case GameController.unit.None:
				break;
			default:
				Debug.Log("Error, intento de invocar una unidad no existente");
				break;
		}
		if (controller.selectedUnit!=GameController.unit.None) {
            if (occupied)
            {
                Debug.Log("La casilla " + x + " " + z + " ya está ocupada");
            }
            else{
                if (controller.saldo >= price)
                {
                    bool bought = controller.buyUnit();
                    if (bought)
                    {
                        occupied = true;
                        unit = Instantiate(unit_temp, new Vector3(transform.position.x + x_offset, transform.position.y + y_offset, transform.position.z), unit_temp.transform.rotation);
                        unit.GetComponent<Unit>().setCellCollider(this);
                    }
                    else Debug.Log("Unidad en cooldown de compra");
                    controller.selectedUnit = GameController.unit.None;
                }
                else
                {
                    GameObject oroIn = GameController.getInstance().oroInsuficiente;
                    oroIn.SetActive(true);
                    oroIn.GetComponent<OroInsuficiente>().Start();
                    Vector3 pointer = Input.mousePosition;
                    pointer.z = 0.5f;
                    oroIn.transform.position = Camera.main.ScreenToWorldPoint(pointer);
                }
            }
		}
	}

	public int checkHp()
    {
        if (!occupied) return -1;
        return unit.GetComponent<Unit>().hp;
    }

	public void unitDies() {
		unit=null;
		occupied=false;
	}

	// Start is called before the first frame update
	void Start() {
		controller=GameController.getInstance();
		occupied=false;
		if (tag!="Grid") {
			CellCollider cell = transform.parent.gameObject.GetComponent<CellCollider>();
			units=cell.units;
		}
        timeDiff = 0.0f;
	}

    private void Update()
    {
        if (tag != "Grid") {
            if (timeDiff <= 0.0f) GetComponent<MeshRenderer>().enabled = false;
            else timeDiff -= Time.deltaTime;
        }
    }
}
