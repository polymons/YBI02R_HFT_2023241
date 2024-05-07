let display = [];
let connection = null;
const itemDisplayElement = document.getElementById("displayItems");

const IdElement = document.getElementById("songID");
const TitleElement = document.getElementById("Title");
const GenreElement = document.getElementById("Genre");
const ArtistElement = document.getElementById("Artist");
const PlaysElement = document.getElementById("Plays");

const updateIdElement = document.getElementById("updateId");
const updateTitleElement = document.getElementById("updateTitle");
const updateGenreElement = document.getElementById("updateGenre");
const updateArtistElement = document.getElementById("updateArtist");
const updatePlaysElement = document.getElementById("updatePlays");

getData();
setupSignalR();

async function getData() {
  await fetch("http://localhost:53910/Song")
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
  display.forEach((t) => {
    itemDisplayElement.innerHTML +=
      "<tr><td>" +
      t.songID +
      "</td><td>" +
      t.title +
      "</td><td>" +
      t.genre +
      "</td><td>" +
      t.plays +
      "</td><td><button class='btn btn-success' type='button' onclick='deleteItem(" +
      t.songID +
      ")'>Delete</button><button class='btn btn-success' type='button' onclick='showUpdate(" +
      t.songID +
      ")'>Update</button></td></tr>";
  });
}
function deleteItem(songID) {
  fetch("http://localhost:53910/Song/" + songID, {
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
  let songID = Number(IdElement.value);
  let title = TitleElement.value;
  let genre = GenreElement.value;
  let plays = Number(PlaysElement.value);

  fetch("http://localhost:53910/Song/", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      songID: songID,
      title: title,
      genre: genre,
      plays: plays,
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
  TitleElement.value = null;
  GenreElement.value = null;
  PlaysElement.value = null;
}

function showUpdate(songID) {
  var toBeUpdated = display.find((d) => d["songID"] == songID);
  document.getElementById("updateDiv").style.display = null;
  document.getElementById("formDiv").style.display = "none";

  updateIdElement.value = toBeUpdated.songID;
  updateTitleElement.value = toBeUpdated.title;
  updateGenreElement.value = toBeUpdated.genre;
  updatePlaysElement.value = toBeUpdated.plays;
}

function update() {
  document.getElementById("updateDiv").style.display = "none";
  document.getElementById("formDiv").style.display = null;
  let songID = Number(updateIdElement.value);
  let title = updateTitleElement.value;
  let genre = updateGenreElement.value;
  let plays = Number(updatePlaysElement.value);

  fetch("http://localhost:53910/Song/", {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      songID: songID,
      title: title,
      genre: genre,
      plays: plays,
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
  updateTitleElement.value = null;
  updateGenreElement.value = null;
  updatePlaysElement.value = null;
}
