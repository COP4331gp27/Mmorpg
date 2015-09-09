
// Use this for initialization
function Start () {
}

var target : Transform;
 
function Update () {
transform.LookAt (target);
	transform.position.x = target.position.x;
	transform.position.y = target.position.y;
	transform.rotation = target.rotation;
}

