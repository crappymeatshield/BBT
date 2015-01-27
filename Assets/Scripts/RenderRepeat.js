#pragma strict


    var ScaleToTiles : float = 1.0;
     
    function Start () {
    renderer.material.mainTextureScale.x = transform.lossyScale.x*ScaleToTiles;
    renderer.material.mainTextureScale.y = transform.lossyScale.y*ScaleToTiles;
    }

function Update () {

}