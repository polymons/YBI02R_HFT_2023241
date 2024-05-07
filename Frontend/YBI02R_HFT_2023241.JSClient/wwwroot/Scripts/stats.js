let items = [];
var output;

const ArtistNameElement = document.getElementById("ArtistName");

const displayItemsElement = document.getElementById("displayItems");

var artistName;

function getArtistName() {
	return ArtistNameElement.value;
}

function MostPopularSongOfArtist() {
	try {
		if (getArtistName() == "") {
			displayItemsElement.innerHTML = "Please enter an artist name";
		} else {
			fetchMostPopularSongOfArtist();
		}
	} catch (error) {
		alert(error);
	}
}
function fetchMostPopularSongOfArtist() {
	items = null;
	fetch(
		"http://localhost:53910/Stat/MostPopularSongOfArtist?artistName=" +
			getArtistName()
	)
		.then((response) => response.json())
		.then((data) => {
			items = data;
			displayItemsElement.innerHTML =
				items.title +
				" is the most popular song of " +
				items.artist.name +
				" with " +
				items.plays +
				" plays.";
			console.log(items);
		});
}

function MostPopularArtist() {
    try {
        fetchMostPopularArtist();
    } catch (error) {
        alert(error);
    }
}

function fetchMostPopularArtist() {
    items = null;
    fetch("http://localhost:53910/Stat/MostPopularArtist/")
        .then((response) => response.json())
        .then((data) => {
            items = data;
            let sum = 0;
            items.songs.forEach(element => { sum += element.plays });
            displayItemsElement.innerHTML = items.name + " is the most popular artist with " + items.songs.length + " songs and " + sum + " plays.";
            console.log(items);
        });
}


