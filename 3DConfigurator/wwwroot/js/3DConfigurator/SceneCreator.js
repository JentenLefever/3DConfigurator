
var scene = new THREE.Scene();
scene.background = new THREE.Color(0xc9c9c9);
var objectpreview = document.getElementById("materialpreview");

var renderer = new THREE.WebGLRenderer();
renderer.gammaOutput = true;
renderer.gammaFactor = 2.2;

var width = objectpreview.clientWidth;
var height = objectpreview.clientHeight;
renderer.setSize(width, height);
var camera = new THREE.PerspectiveCamera(75, width / height, 0.1, 1000);

objectpreview.appendChild(renderer.domElement);



//resize window
window.addEventListener('resize', function () {

    var width = objectpreview.clientWidth;
    var height = objectpreview.clientHeight;
    renderer.setSize(width, height);
    camera.aspect = width / height;
    camera.updateProjectionMatrix();
});

controls = new THREE.OrbitControls(camera, renderer.domElement);

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


var directionalLight = new THREE.DirectionalLight(0xffffff, 0.5);
scene.add(directionalLight);



//Create the shape
var geometry = new THREE.BoxGeometry(1, 1, 1);

// Create Material
var material = new THREE.MeshBasicMaterial({ color: 0xFF0000, wireframe: false });
var cube = new THREE.Mesh(geometry, material);


//scene.add(cube);
camera.position.z = 3;


//logic
var update = function () {

    //cube.rotation.x +=0.01
    //cube.rotation.y +=0.005
};

//Draw  scene
var render = function () {
    renderer.render(scene, camera);


};

//run game loop(update, render , repeat)
var GameLoop = function () {

    requestAnimationFrame(GameLoop);

    update();

    render();
};

GameLoop();




