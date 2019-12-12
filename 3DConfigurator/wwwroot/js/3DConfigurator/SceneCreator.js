
//Scene aanmaken
var scene = new THREE.Scene();
//backgroundcolor
scene.background = new THREE.Color(0xc9c9c9);


//Create renderer
var renderer = new THREE.WebGLRenderer();
renderer.gammaOutput = true;

//Renderer Size
var objectpreview = document.getElementById("materialpreview");
var width = objectpreview.clientWidth;
var height = objectpreview.clientHeight;
renderer.setSize(width, height);


//Add render to materialpreview

objectpreview.appendChild(renderer.domElement);



//resize window
window.addEventListener('resize', function () {

    var width = objectpreview.clientWidth;
    var height = objectpreview.clientHeight;
    renderer.setSize(width, height);
    camera.aspect = width / height;
    camera.updateProjectionMatrix();
});

////Controls
//controls = new THREE.OrbitControls(camera, renderer.domElement);

//Load object in Scene
var loader = new THREE.GLTFLoader();
loader.load(
    // resource URL
    'wwwroot/Objects/Current.glb',
    // called when resource is loaded
    function (gltf) {
        scene.add(gltf.scene);
        gltf.animations; // Array<THREE.AnimationClip>
        gltf.scene; // THREE.Scene
        gltf.scenes; // Array<THREE.Scene>
        gltf.cameras; // Array<THREE.Camera>
        gltf.asset; // Object
    });

//Add camera
var camera = new THREE.PerspectiveCamera(75, width / height, 0.1, 1000);

//Add light
var directionalLight = new THREE.DirectionalLight(0xffffff, 0.5);
scene.add(directionalLight);

//Add hdri
var hdrUrls = ['px.hdr', 'nx.hdr', 'py.hdr', 'ny.hdr', 'pz.hdr', 'nz.hdr'];
hdrCubeMap = new HDRCubeTextureLoader()
    .setPath('wwwroot/Textures/pisaHDR/')
    .setDataType(THREE.UnsignedByteType)
    .load(hdrUrls, function () {

        var pmremGenerator = new PMREMGenerator(hdrCubeMap);
        pmremGenerator.update(renderer);

        var pmremCubeUVPacker = new PMREMCubeUVPacker(pmremGenerator.cubeLods);
        pmremCubeUVPacker.update(renderer);

        hdrCubeRenderTarget = pmremCubeUVPacker.CubeUVRenderTarget;

        hdrCubeMap.magFilter = THREE.LinearFilter;
        hdrCubeMap.needsUpdate = true;

        pmremGenerator.dispose();
        pmremCubeUVPacker.dispose();

    });

//logic
var update = function () {
 
};

//Draw  scene
var render = function () {
    
    var renderTarget, cubeMap;
    renderTarget = hdrCubeRenderTarget;
    cubeMap = hdrCubeMap;
    renderer.render(scene, camera);
};

//run game loop(update, render , repeat)
var GameLoop = function () {
    requestAnimationFrame(GameLoop);
    update();
    render();
};

GameLoop();




