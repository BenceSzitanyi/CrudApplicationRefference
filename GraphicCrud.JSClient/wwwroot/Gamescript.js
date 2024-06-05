let games = [];
let connection = null;

let gameIdToUpdate = -1;

getdata();
setupSignalR();

async function getdata() {
    await fetch("http://localhost:57640/game")
        .then(x => x.json())
        .then(y => {
            games = y
            console.log(games);
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    games.forEach(t => {

        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.videoGameId + "</td><td>"
            + t.title + "</td><td>"
            + `<button type="button" onclick="remove(${t.videoGameId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.videoGameId})">Update</button>`
            + "</td></tr>";

    });
}
function create() {
    let cTitle = document.getElementById('title').value;
    fetch('http://localhost:57640/game', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: cTitle })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function remove(id) {
    fetch('http://localhost:57640/game/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function showupdate(id) {
    document.getElementById('nametoupdate').value = games.find(t => t['videoGameId'] == id)['title'];
    document.getElementById('updateformdiv').style.display = 'flex';
    gameIdToUpdate = id;
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let uName = document.getElementById('nametoupdate').value;
    fetch('http://localhost:57640/game', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { title: uName, videoGameId: gameIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function setupSignalR()
{
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:57640/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("VideoGameCreated", (user, message) => {
        getdata();
    });

    connection.on("VideoGameDeleted", (user, message) => {
        getdata();
    });

    connection.on("VideoGameUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}