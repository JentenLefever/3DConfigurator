
//Renderer

var renderer = new THREE.WebGLRenderer({ div: document.getElementById('ModelCanvas'), antalias: true });
var modelRenderer = document.getElementById('ModelCanvas');
renderer.setClearColor(0xDCDCDC);
renderer.setPixelRatio(window.devicePixelRatio);
renderer.setSize(modelRenderer.clientWidth, modelRenderer.clientHeight);
renderer.gammaOutput = true;
modelRenderer.appendChild(renderer.domElement);

//Camera
var camera = new THREE.PerspectiveCamera(45, modelRenderer.clientWidth / modelRenderer.clientHeight, 0.25, 1000);
camera.position.set(0, 0.9, 8);

//scene
var scene = new THREE.Scene();


//object
var objects = [];
new THREE.RGBELoader()
    .setDataType(THREE.UnsignedByteType)
    .setPath('wwwroot/Textures/')
    .load('pedestrian_overpass_2k.hdr', function (texture) {

        var options = {
            minFilter: texture.minFilter,
            magFilter: texture.magFilter
        };

        
        var hrdBackground = new THREE.WebGLRenderTargetCube(5000, 5000, options).fromEquirectangularTexture(renderer, texture);
        scene.background = hrdBackground;
        var pmremGenerator = new THREE.PMREMGenerator(hrdBackground.texture);
        pmremGenerator.update(renderer);

        var pmremCubeUVPacker = new THREE.PMREMCubeUVPacker(pmremGenerator.cubeLods);
        pmremCubeUVPacker.update(renderer);

        var envMap = pmremCubeUVPacker.CubeUVRenderTarget.texture;

        // model
                
        var loader = new THREE.GLTFLoader().setPath('wwwroot/Objects/');
        loader.load('Current.glb', function (gltf) {

            gltf.scene.traverse(function (child) {

                if (child.isMesh) {

                    child.material.envMap = envMap;

                }

            });
            
            scene.add(gltf.scene);
        });

        pmremGenerator.dispose();
        pmremCubeUVPacker.dispose();

    });

var removeObject = document.getElementById('RemoveObject');
removeObject.onclick = function () {
    while (scene.children.length > 0) {
        scene.remove(scene.children[0]);
    }
};

var Addhdributton = document.getElementById('Addhdri');
var background = scene.background;
Addhdributton.onclick = function () {

    
    if (scene.background !== null) {
        console.log("changedhdri");
        scene.background = null;
        render();
        
    }
    else {
        new THREE.RGBELoader()
            .setDataType(THREE.UnsignedByteType)
            .setPath('wwwroot/Textures/')
            .load('pedestrian_overpass_2k.hdr', function (texture) {
                var options = {
                    minFilter: texture.minFilter,
                    magFilter: texture.magFilter
                };
                texture.offset = .5;
                scene.background = new THREE.WebGLRenderTargetCube(5000, 5000, options).fromEquirectangularTexture(renderer, texture);
            });
       
        render();
    }
    
};

var slider = document.getElementById("Rotaterange");
slider.oninput = function () {
    scene.children[0].rotation.y = this.value;
    
};


requestAnimationFrame(render);

//light
//var light = new THREE.AmbientLight(0xffffff, 0.8);

//scene.add(light);


//Controls
var controls;


        //controls = new THREE.DragControls(objects, camera, renderer.domElement);
   
       controls = new THREE.OrbitControls(camera, renderer.domElement);
    


    //controls.update() must be called after any manual changes to the camera's transform

    controls.update();

function render() {
    
    renderer.render(scene, camera);
    requestAnimationFrame(render);
}

window.addEventListener('resize', onWindowResize, false);
function onWindowResize() {

    camera.aspect = modelRenderer.clientWidth / modelRenderer.clientHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(modelRenderer.clientWidth, modelRenderer.clientHeight);

}