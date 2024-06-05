let publishers = [];
let connection = null;

let publisherIdToUpdate = -1;

getdata()

async function getdata() {
    await fetch('http://localhost:57640/publisher')
        .then(x => x.json())
        .then(y => {
            publishers = y;
            console.log(publishers);
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    publishers.forEach(t => {

        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.publisherId + "</td><td>"
            + t.publisherName + "</td><td>"
            + `<button type="button" onclick="remove(${t.publisherId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.publisherId})">Update</button>`
            + "</td></tr>";

    });
}
function create() {
    let cName = document.getElementById('name').value;
    fetch('http://localhost:57640/publisher', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { publisherName: cName })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function remove(id) {
    fetch('http://localhost:57640/publisher/' + id, {
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
    document.getElementById('nametoupdate').value = publishers.find(t => t['publisherId'] == id)['publisherName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    publisherIdToUpdate = id;
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let uName = document.getElementById('nametoupdate').value;
    fetch('http://localhost:57640/publisher', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { publisherName: uName, publisherId: publisherIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}