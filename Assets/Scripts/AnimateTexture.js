#pragma strict

var xLength = 1;
var yLength = 1; 
var fps = 10.0;
var shouldTile : boolean = true;
var ScaleToTiles : float = 1.0;


function Start () {

}


function Update () {
 
	// Calculate index
	var index : int = Time.time * fps;
	// repeat when exhausting all frames
	index = index % (xLength * yLength);
 
	// Size of every tile
	var size = Vector2 (1.0 / xLength, 1.0 / yLength);
 
	// split into horizontal and vertical index
	var uIndex = index % xLength;
	var vIndex = index / yLength;
 
	// build offset
	// v coordinate is the bottom of the image in opengl so we need to invert.
	var offset = Vector2 (uIndex * size.x, 1.0 - size.y - vIndex * size.y);
 
	renderer.material.SetTextureOffset ("_MainTex", offset);
	if(!shouldTile)
	renderer.material.SetTextureScale ("_MainTex", size);
	
	if(shouldTile)
	{
	renderer.material.mainTextureScale.x = transform.lossyScale.x*ScaleToTiles / xLength;
	renderer.material.mainTextureScale.y = transform.lossyScale.y*ScaleToTiles / yLength;
	}
	
}