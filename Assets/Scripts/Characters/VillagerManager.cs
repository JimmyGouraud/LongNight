using System.Collections;
using UnityEngine;

public class VillagerManager : MonoBehaviour {
	
	private GameObject target = null;

	// Will be destroyed when unnecessary
	private GameObject cityHall = null;
	private GameObject resourcesHeap = null;
	// ===

	private Coroutine coMovement;

	private float speed = 3f;
	private bool reachTarget = false;

	private bool mining = false;
	private bool lumbering = false;

	void Awake() {
		this.cityHall = GameObject.Find("City Hall");
		this.resourcesHeap = FindCloserResource();
		DayManager.Instance.OnChangeDay += ChangeActivity;
	}

	void ChangeActivity() {
		if (coMovement != null) {
			StopCoroutine(coMovement);
		}

		this.target = DayManager.Instance.Day ? this.resourcesHeap : this.cityHall;
		coMovement = StartCoroutine(MoveTowardTarget());

	}

	void OnMouseDown() {
		InformationsUI.Instance.UpdatePanel(this.gameObject);
	}

	void OnCollisionEnter(Collision collision) {
		GameObject collisionGO = collision.gameObject;
		if (collisionGO == this.target) {
			this.transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z);
			reachTarget = true;

			if (collisionGO.tag == "Stone Mine") {
				StartCoroutine(Mining());
			}
			else if (collisionGO.tag == "Forestry") {
				StartCoroutine(Lumbering());
			}
		}
	}
	
	IEnumerator Mining() { // exploitation minière
		this.mining = true;

		int stones = 0;
		while (mining) {
			stones++;
			if (stones == 10) {
				ResourcesManager.Instance.AddStones(stones);
				stones = 0;
			}
			yield return new WaitForSeconds(0.2f);
		}
		ResourcesManager.Instance.AddStones(stones);
	}

	IEnumerator Lumbering() { // exploitation forestière
		this.lumbering = true;

		int woods = 0;
		while (lumbering) {
			woods++;
			if (woods == 10) {
				ResourcesManager.Instance.AddWoods(woods);
				woods = 0;
			}
			yield return new WaitForSeconds(0.2f);
		}
		ResourcesManager.Instance.AddWoods(woods);
	}

	IEnumerator MoveTowardTarget() {
		Vector3 directionToTarget = GetDirectionToTarget(this.target);

		this.mining = false;
		this.lumbering = false;
		this.reachTarget = false;
		while (!reachTarget) {
			this.transform.position += directionToTarget * Time.deltaTime * this.speed;
			yield return null;
		}
	}

	Vector3 GetDirectionToTarget(GameObject go) {
		Vector3 direction = (go.transform.position - this.transform.position).normalized;
		direction.y = 0;
		return direction;
	}

	GameObject FindCloserResource() {
		GameObject closerStoneMine = FindCloserResource(GameObject.Find("Stone Mines").transform);
		float distToCloserStoneMine = Vector3.Distance(closerStoneMine.transform.position, this.transform.position);

		GameObject closerWoods= FindCloserResource(GameObject.Find("Forest").transform);
		float distToCloserForest = Vector3.Distance(closerWoods.transform.position, this.transform.position);

		return (distToCloserForest < distToCloserStoneMine) ? closerWoods : closerStoneMine;
	}

	GameObject FindCloserResource(Transform resources) {
		GameObject closerResource = null;
		float closerDistance = float.MaxValue;

		for (int i = 0; i < resources.childCount; i++) {
			GameObject resource = resources.GetChild(i).gameObject;
			if (closerResource == null) {
				closerResource = resource;
				closerDistance = Vector3.Distance(closerResource.transform.position, this.transform.position);
				continue;
			}

			float newDistance = Vector3.Distance(resource.transform.position, this.transform.position);
			if (newDistance < closerDistance) {
				closerResource = resource;
				closerDistance = newDistance;
			}
		}

		return closerResource;
	}
}
