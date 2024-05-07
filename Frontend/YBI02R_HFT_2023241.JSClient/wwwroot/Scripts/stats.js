let items = [];
var output;

const ArtistNameElement = document.getElementById("ArtistName");

const displayItemsElement = document.getElementById("displayItems");

var artistName;

function getArtistName() {
	return ArtistNameElement.value;
}

//ARTIST

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


function ArtistHomeCity() {
    try {
        if (getArtistName() == "") {
            displayItemsElement.innerHTML = "Please enter an artist name";
        } else {
            fetchArtistHomeCity();
        }
    } catch (error) {
        alert(error);
    }
}

function fetchArtistHomeCity() {
    items = null;
    fetch(
        "http://localhost:53910/Stat/ArtistHomeCity?artistName=" +
        getArtistName()
    )
        .then((response) => response.text())
        .then((data) => {
            items = data;
            displayItemsElement.innerHTML = data;
            console.log(items);
        });
}

function AvgSongLengthForArtist() {
    try {
        if (getArtistName() == "") {
            displayItemsElement.innerHTML = "Please enter an artist name";
        } else {
            fetchAvgSongLengthForArtist();
        }
    } catch (error) {
        alert(error);
    }
}

function fetchAvgSongLengthForArtist() {
    items = null;
    fetch(
        "http://localhost:53910/Stat/AvgSongLengthForArtist?artistName=" +
        getArtistName()
    )
        .then((response) => response.text())
        .then((data) => {
            items = data;
            displayItemsElement.innerHTML =
                "The average song length for " + getArtistName() +
                " is " +
                parseFloat(items).toFixed(2) +
                " seconds.";
            console.log(items);
        });
}





//GENERAL

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
			items.songs.forEach((element) => {
				sum += element.plays;
			});
			displayItemsElement.innerHTML =
				items.name +
				" is the most popular artist with " +
				items.songs.length +
				" songs and " +
				sum +
				" plays.";
			console.log(items);
		});
}

function LongestSong() {
    try {
        fetchLongestSong();
    } catch (error) {
        alert(error);
    }
}

function fetchLongestSong() {
    items = null;
    fetch("http://localhost:53910/Stat/LongestSong/")
        .then((response) => response.json())
        .then((data) => {
            items = data;
            displayItemsElement.innerHTML =
                items.title +
                " is the longest song at " +
                items.length +
                " seconds.";
            console.log(items);
        });
}

function ArtistWithMostSongs() {
    try {
        fetchArtistWithMostSongs();
    } catch (error) {
        alert(error);
    }
}

function fetchArtistWithMostSongs() {
    items = null;
    fetch("http://localhost:53910/Stat/ArtistWithMostSongs/")
        .then((response) => response.json())
        .then((data) => {
            items = data;
            displayItemsElement.innerHTML =
                items.name +
                " is the artist with most songs with " +
                items.songs.length +
                " songs.";
            console.log(items);
        });
}