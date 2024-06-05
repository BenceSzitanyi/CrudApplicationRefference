let developers = [];
let connection = null;

let developerIdToUpdate = -1;

getdata()
setupSignalR();

async function getdata() {
    await fetch('http://localhost:57640/developer')
        .then(x => x.json())
        .then(y => {
            developers = y;
            console.log(developers);
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    developers.forEach(t => {

        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.developerId + "</td><td>"
            + t.developerName + "</td><td>"
            + `<button type="button" onclick="remove(${t.developerId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.developerId})">Update</button>`
            + "</td></tr>";

    });
}

function create() {
    let cName = document.getElementById('name').value;
    fetch('http://localhost:57640/developer', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { developerName: cName })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function remove(id) {
    fetch('http://localhost:57640/developer/' + id, {
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
    document.getElementById('nametoupdate').value = developers.find(t => t['developerId'] == id)['developerName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    developerIdToUpdate = id;
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let uName = document.getElementById('nametoupdate').value;
    fetch('http://localhost:57640/developer', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { developerName: uName, developerId: developerIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:57640/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("DeveloperCreated", (user, message) => {
        getdata();
    });

    connection.on("DeveloperDeleted", (user, message) => {
        getdata();
    });

    connection.on("DeveloperUpdated", (user, message) => {
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