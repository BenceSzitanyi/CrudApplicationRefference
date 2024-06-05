let developers = [];
getdata();
async function getdata() {
    await fetch('http://localhost:57640/Stat/DevelopersWithMostGames/3')
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
            + "</td></tr>";
    });
}