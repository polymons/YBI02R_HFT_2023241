let display = [];
let connection = null;
const itemDisplayElement = document.getElementById("displayItems");

const IdElement = document.getElementById("itemId");
const NameElement = document.getElementById("itemName");
const CityElement = document.getElementById("itemCity");

const updateIdElement = document.getElementById("updateId");
const updateNameElement = document.getElementById("updateName");
const updateCityElement = document.getElementById("updateCity");

getData();
setupSignalR();
async function getData() {
  await fetch("http://localhost:53910/publisher")
    .then((x) => x.json())
    .then((y) => {
      display = y;
      console.log(y);
      displayItems();
    });
}
function setupSignalR() {
  connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:53910/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("PublisherCreated", (user, message) => {
    getData();
  });

  connection.on("PublisherDeleted", (user, message) => {
    getData();
  });

  connection.on("PublisherUpdated", (user, message) => {
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
  display.forEach((t) => {
    itemDisplayElement.innerHTML +=
      "<tr><td>" +
      t.studioID +
      "</td><td>" +
      t.studioName +
      "</td><td>" +
      t.city +
      "</td><td><button class='btn btn-success' type='button' onclick='deleteItem(" +
      t.studioID +
      ")'>Delete</button><button class='btn btn-success' type='button' onclick='showUpdate(" +
      t.studioID +
      ")'>Update</button></td></tr>";
  });
}
function deleteItem(studioID) {
  fetch("http://localhost:53910/Publisher/" + studioID, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: null,
  })
    .then((response) => response)
    .then((data) => {
      console.log("Success: ", data);
      getData();
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}

function create() {
  let studioID = Number(IdElement.value);
  let studioName = NameElement.value;
  let city = CityElement.value;

  fetch("http://localhost:53910/Publisher/", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      studioID: studioID,
      studioName: studioName,
      city: city,
    }),
  })
    .then((response) => response)
    .then((data) => {
      console.log("Success: ", data);
      getData();
    })
    .catch((error) => {
      console.error("Error:", error);
    });

  IdElement.value = null;
  NameElement.value = null;
  CityElement.value = null;
}

function showUpdate(studioID) {
  var toBeUpdated = display.find((d) => d["studioID"] == studioID);
  document.getElementById("updateDiv").style.display = null;
  document.getElementById("formDiv").style.display = "none";

  updateIdElement.value = toBeUpdated.studioID;
  updateNameElement.value = toBeUpdated.studioName;
  updateCityElement.value = toBeUpdated.city;
}

function update() {
  document.getElementById("updateDiv").style.display = "none";
  document.getElementById("formDiv").style.display = null;
  let studioID = Number(updateIdElement.value);
  let studioname = updateNameElement.value;
  let city = updateCityElement.value;

  fetch("http://localhost:53910/Publisher", {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      studioID: studioID,
      studioname: studioname,
      city: city,
    }),
  })
    .then((response) => response)
    .then((data) => {
      console.log("Success: ", data);
      getData();
    })
    .catch((error) => {
      console.error("Error:", error);
    });

  updateIdElement.value = null;
  updateNameElement.value = null;
  updateCityElement.value = null;
}
