#pragma strict


    var ScaleToTiles : float = 1.0;
     
    function Start () {
    GetComponent.<Renderer>().material.mainTextureScale.x = transform.lossyScale.x*ScaleToTiles;
    GetComponent.<Renderer>().material.mainTextureScale.y = transform.lossyScale.y*ScaleToTiles;
    }

function Update () {

}