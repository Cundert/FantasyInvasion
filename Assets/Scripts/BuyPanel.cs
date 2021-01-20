using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPanel : MonoBehaviour
{
	private GameController controller;
	public GameController.unit unit;

	public GameObject[] panels;
	public Sprite[] sprites;
	public Sprite[] selectedSprites;
	public Sprite[] cooldownSprites;

	public TextMeshProUGUI[] cooldownTexts;

	private bool oneSelected;
	private bool[] inCooldown;
    private bool pausa = false;

	public void clickButton(int id) {
        if (!pausa)
        {
            unselectAll();
            switch (id)
            {
                case 0:
                    if (unit == GameController.unit.Chest) unit = GameController.unit.None;
                    else unit = GameController.unit.Chest;
                    break;
                case 1:
                    if (unit == GameController.unit.Dragon) unit = GameController.unit.None;
                    else unit = GameController.unit.Dragon;
                    break;
                case 2:
                    if (unit == GameController.unit.Knight) unit = GameController.unit.None;
                    else unit = GameController.unit.Knight;
                    break;
                case 3:
                    if (unit == GameController.unit.Explosive) unit = GameController.unit.None;
                    else unit = GameController.unit.Explosive;
                    break;
                case 4:
                    if (unit == GameController.unit.IceDragon) unit = GameController.unit.None;
                    else unit = GameController.unit.IceDragon;
                    break;
                case 5:
                    if (unit == GameController.unit.Remove) unit = GameController.unit.None;
                    else unit = GameController.unit.Remove;
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
            if (unit != GameController.unit.None && (unit == GameController.unit.Remove || !inCooldown[(int)unit - 1]))
            {
                oneSelected = true;
                panels[id].GetComponent<Image>().sprite = selectedSprites[id];
            }
            controller.selectedUnit = unit;
        }
	}

	private void unselectAll() {
		for (int i = 0; i<panels.Length; ++i)
			if (i==(panels.Length-1) || !inCooldown[i]) panels[i].GetComponent<Image>().sprite=sprites[i]; // En la ultima posicion tenemos el panel de eliminar, que no tiene cooldown
	}

	public void updateCooldown(int id, int cooldown) {
		if (cooldown==0) {
			inCooldown[id]=false;
			panels[id].GetComponent<Image>().sprite=sprites[id];
			cooldownTexts[id].text="";
		} else {
			inCooldown[id]=true;
			panels[id].GetComponent<Image>().sprite=cooldownSprites[id];
			cooldownTexts[id].text=" "+cooldown;
			if (cooldown>=10) cooldownTexts[id].text=""+cooldown;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		controller=GameController.getInstance();
		controller.setBuyPanel(this);
		oneSelected=false;
		System.Array.Resize(ref inCooldown, panels.Length-1); // Restamos 1 porque el panel de eliminar unidad no tiene cooldown
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pausa = !pausa;
        if (oneSelected && controller.selectedUnit==GameController.unit.None) {
			unit=GameController.unit.None;
			oneSelected=false;
			unselectAll();
		}
    }
}
