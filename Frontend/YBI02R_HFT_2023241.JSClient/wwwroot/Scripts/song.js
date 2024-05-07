let display = [];
let connection = null;
const itemDisplayElement = document.getElementById('displayItems');

const IdElement = document.getElementById('itemId');
const NameElement = document.getElementById('itemName');
const BirthElement = document.getElementById('itemBirth');
const WeightElement = document.getElementById('itemWeight');
const ColorElement = document.getElementById('itemColor');

const updateIdElement = document.getElementById('updateId');
const updateNameElement = document.getElementById('updateName');
const updateBirthElement = document.getElementById('updateBirth');
const updateWeightElement = document.getElementById('updateWeight');
const updateColorElement = document.getElementById('updateColor');

getData();
setupSignalR();

async function getData() {
    await fetch('http://localhost:53910/Song')
        .then(x => x.json())
        .then(y => {
            display = y;
            console.log(y);
            displayItems();
        });
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build()

    connection.on("SongCreated", (user, message) => {
        getData();
    });

    connection.on("SongDeleted", (user, message) => {
        getData();
    });

    connection.on("SongUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}
function displayItems() {
    itemDisplayElement.innerHTML = null;
    display.forEach(t => {
        itemDisplayElement.innerHTML += "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>" + t.birthYear + "</td><td>" + t.weight + "</td><td>" + t.color + "</td><td><button class='btn btn-success' type='button' onclick='deleteItem(" + t.id + ")'>Delete</button><button class='btn btn-success' type='button' onclick='showUpdate(" + t.id + ")'>Update</button></td></tr>"
    });
}
function deleteItem(id) {
    fetch('http://localhost:53910/Song/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null,
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });
}

function create() {
    let id = Number(IdElement.value);
    let name = NameElement.value;
    let birth = Number(BirthElement.value);
    let weigth = Number(WeightElement.value);
    let color = Number(ColorElement.value);

    fetch('http://localhost:53910/Song', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: id, name: name, birthYear: birth, weight: weigth, color: color })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });

    IdElement.value = null;
    NameElement.value = null;
    BirthElement.value = null;
    WeightElement.value = null;
    ColorElement.value = null;
}

function showUpdate(id) {
    var toBeUpdated = display.find(d => d['id'] == id);
    document.getElementById("updateDiv").style.display = null;
    document.getElementById("formDiv").style.display = "none";

    updateIdElement.value = toBeUpdated.id;
    updateNameElement.value = toBeUpdated.name;
    updateBirthElement.value = toBeUpdated.birthYear;
    updateWeightElement.value = toBeUpdated.weight;
    updateColorElement.value = toBeUpdated.color;
}

function update() {
    document.getElementById("updateDiv").style.display = "none";
    document.getElementById("formDiv").style.display = null;
    let id = Number(updateIdElement.value);
    let name = updateNameElement.value;
    let birth = Number(updateBirthElement.value);
    let weigth = Number(updateWeightElement.value);
    let color = Number(updateColorElement.value);

    fetch('http://localhost:53910/Song', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: id, name: name, birthYear: birth, weight: weigth, color: color })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });

    updateIdElement.value = null;
    updateNameElement.value = null;
    updateBirthElement.value = null;
    updateWeightElement.value = null;
    updateColorElement.value = null;
}