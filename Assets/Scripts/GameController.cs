using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	static public GameController instance;

	public MenuController menuController;

	public enum unit { None, Chest, Dragon, Knight, Explosive, IceDragon, Remove};

	public int saldo;
	public float cooldown;
	public unit selectedUnit = unit.None;
	public int[] unitPrices;
	public float[] unitBuyingCooldowns;
	public GameObject textGold, oroInsuficiente, textVictoria, textFail, textPausa;
    public float victoryDelay, failDelay;
    private float victoryTimeDiff, failTimeDiff;

    public int enemiesRemaining;
	private float[] unitTimeDiffCooldowns;
	private BuyPanel buyPanel;

    public bool pausa = false;
	public bool pausable;

	static public GameController getInstance() {
		return instance;
	}

	public void setBuyPanel(BuyPanel bp) {
		buyPanel=bp;
	}

	private void Awake() {
		if (!instance) instance=this;
		else Destroy(gameObject);
	}

	// Start is called before the first frame update
	void Start() {
		System.Array.Resize(ref unitTimeDiffCooldowns, unitBuyingCooldowns.Length);
		for (int i = 0; i<unitTimeDiffCooldowns.Length; ++i) unitTimeDiffCooldowns[i]=0;
        victoryTimeDiff = -1.0f;
        failTimeDiff = -1.0f;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape) && pausable) {
			pausa=!pausa;
			menuController.backFromSettingsInPause();
		}
        if (pausa)
        {
            textPausa.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!pausa)
        {
            Time.timeScale = 1;
            textPausa.SetActive(false);
            float time = Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = LayerMask.GetMask("Diamond");
                LayerMask mask2 = LayerMask.GetMask("Lever");
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 20.0f, mask))
                {
                    SoundController.getInstance().play(SoundController.SoundId.diamond);
                    saldo += hit.transform.gameObject.GetComponent<Diamond>().die();
                    
                }
                else if (Physics.Raycast(ray, out hit, 20.0f, mask2))
                {
                    hit.transform.gameObject.GetComponent<Lever>().activate();
                }
                else
                {
                    mask = LayerMask.GetMask("CellCollider");
                    if (Physics.Raycast(ray, out hit, 20.0f, mask))
                    {
                        if (selectedUnit == unit.Remove) hit.transform.gameObject.GetComponent<CellCollider>().removeUnit();
                        else hit.transform.gameObject.GetComponent<CellCollider>().summonUnit();
                    }
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = LayerMask.GetMask("CellCollider");
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 20.0f, mask))
                {
                    if (selectedUnit != unit.None)
                    {
                        hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        hit.transform.gameObject.GetComponent<CellCollider>().timeDiff = 0.001f;
                    }
                }

                textGold.GetComponent<TextMeshProUGUI>().text = ""+saldo;

                updateTimeCooldowns(time);
                if (victoryTimeDiff > -1.0f && failTimeDiff <= -1.0f)
                {
                    victoryTimeDiff += time;
                    if (victoryTimeDiff >= victoryDelay)
                    {
                        victoryTimeDiff = -1;
						if (SceneManager.GetActiveScene().name=="Level1_2") {
							Camera.main.GetComponent<CameraFunctions>().deactivateUI();
							Camera.main.GetComponent<CameraFunctions>().deleteChest();
							Camera.main.GetComponent<Animator>().SetTrigger("levelTransition");
						} else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
                    }
                }
                if (failTimeDiff > -1.0f)
                {
                    failTimeDiff += time;
                    if (failTimeDiff >= failDelay)
                    {
                        failTimeDiff = -1;
                        SceneManager.LoadScene("MainMenu3D", LoadSceneMode.Single);
                    }
                }
            }
        }
	}

    public void win()
    {
        if (failTimeDiff <= -1.0f)
        {
            textVictoria.SetActive(true);
            victoryTimeDiff = 0.0f;
        }
    }

    public void lose()
    {
        if (failTimeDiff <= -1.0f)
        {
            textFail.SetActive(true);
            failTimeDiff = 0.0f;
        }
    }

    public bool buyUnit() {
		int id = (int)selectedUnit-1;
		if (unitTimeDiffCooldowns[id]==0) {
			unitTimeDiffCooldowns[id]=unitBuyingCooldowns[id];
			pay(unitPrices[id]);
			return true;
		}
		return false;
	}

	public void pay(int price) {
		saldo-=price;
	}

	public void earn(int price) {
		saldo+=price;
	}

	public void refund(GameObject unit) {
		Unit un = unit.GetComponent<Unit>();
		int value = 0;
		switch (unit.name) {
			case ("Treasure(Clone)"):
				value=unitPrices[0];
				break;
			case ("Dragon(Clone)"):
				value=unitPrices[1];
				break;
			case ("Knight(Clone)"):
				value=unitPrices[2];
				break;
			case ("Explosive(Clone)"):
				value=unitPrices[3];
				break;
			case ("IceDragon(Clone)"):
				value=unitPrices[4];
				break;
		}
		int amount = Mathf.RoundToInt(Mathf.Floor(((float)un.hp/(float)un.maxHp)*value/2.0f));
		earn(amount);
	}

	private void updateTimeCooldowns(float time) {
		for (int i = 0; i<unitTimeDiffCooldowns.Length; ++i) {
			if (unitTimeDiffCooldowns[i]>0) {
				unitTimeDiffCooldowns[i]-=time;
				if (unitTimeDiffCooldowns[i]<=0) {
					unitTimeDiffCooldowns[i]=0;
				}
				buyPanel.updateCooldown(i,Mathf.CeilToInt(unitTimeDiffCooldowns[i]));
			}
		}
	}
}
