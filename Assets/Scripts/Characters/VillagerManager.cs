using System.Collections;
using UnityEngine;

public class VillagerManager : MonoBehaviour {
	enum State { idle, mining, lumbering, building, walking };

	private GameObject target = null;
	private State state = State.idle;

	// Will be destroyed when unnecessary
	private GameObject cityHall = null;
	private GameObject resourcesHeap = null;
	// ===

	private Coroutine coMovement;

	private float speed = 3f;

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
	
	GameObject FindAncestorWithRigidbody(GameObject go)
	{
		while (go.transform.parent != null && (go.GetComponent<Rigidbody>() == null)) {
			go = go.transform.parent.gameObject;
		}

		return go.GetComponent<Rigidbody>() == null ? null : go;
	}

	void OnTriggerEnter(Collider collider)
	{
		// TODO: Find another way to collide with forestry or stone mines.
		GameObject colliderGO = FindAncestorWithRigidbody(collider.gameObject);

		if (colliderGO == this.target) {
			this.transform.position = new Vector3(colliderGO.transform.position.x, transform.position.y, colliderGO.transform.position.z);

			if (state != State.mining && colliderGO.tag == "Stone Mine") {
				StartCoroutine(Mining());
			}
			else if (state != State.lumbering && colliderGO.tag == "Forestry") {
				StartCoroutine(Lumbering());
			}
			else if (state != State.idle && colliderGO.tag == "City Hall") {
				state = State.idle;
			}
		}
	}
	
	IEnumerator Mining() { // exploitation minière
		state = State.mining;

		int stones = 0;
		while (state == State.mining) {
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
		state = State.lumbering;

		int woods = 0;
		while (state == State.lumbering) {
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
		state = State.walking;

		Vector3 directionToTarget = GetDirectionToTarget(this.target);
		while (state == State.walking) {
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
