
//Renderer

var renderer = new THREE.WebGLRenderer({ div: document.getElementById('ModelCanvas'), antalias: true });
var modelRenderer = document.getElementById('ModelCanvas');
renderer.setClearColor(0x00ff00);
renderer.setPixelRatio(window.devicePixelRatio);
renderer.setSize(modelRenderer.clientWidth, modelRenderer.clientHeight);
renderer.gammaOutput = true;
modelRenderer.appendChild(renderer.domElement);

//Camera
var camera = new THREE.PerspectiveCamera(45, modelRenderer.clientWidth / modelRenderer.clientHeight, 0.25, 20);
camera.position.set(0, 0.9, 8);

//scene
var scene = new THREE.Scene();

//Controls
var controls = new THREE.OrbitControls(camera, renderer.domElement);

//controls.update() must be called after any manual changes to the camera's transform

controls.update();

//object
new THREE.RGBELoader()
    .setDataType(THREE.UnsignedByteType)
    .setPath('wwwroot/Textures/')
    .load('pedestrian_overpass_2k.hdr', function (texture) {

        var options = {
            minFilter: texture.minFilter,
            magFilter: texture.magFilter
        };

        scene.background = new THREE.WebGLRenderTargetCube(1024, 1024, options).fromEquirectangularTexture(renderer, texture);

        var pmremGenerator = new THREE.PMREMGenerator(scene.background.texture);
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


requestAnimationFrame(render);

//light
var light = new THREE.AmbientLight(0xffffff, 0.8);

scene.add(light);




function render() {
    
    renderer.render(scene, camera);
    requestAnimationFrame(render);
}

window.addEventListener('resize', onWindowResize, false);
function onWindowResize() {

    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(window.innerWidth, window.innerHeight);

}