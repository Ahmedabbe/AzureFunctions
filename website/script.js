let tabledata = document.getElementById('tabledata')

setInterval(() => {
    tabledata.innerHTML = "";

    fetch("https://iot20tablestorage.azurewebsites.net/api/GetDataFromTableStorage?code=aiypKZzkYMHD9CZEB/G15Hac3qxaTkYxRvvaWv1lPj73f6XCP3C/zA==&limit=20&orderby=desc")
    .then(res => res.json())
    .then(data => {
        for(let row of data) {
        tabledata.innerHTML += `<tr><td>${row.rowKey}</td><td>${row.deviceId}</td><td>${row.timestamp}</td><td>${row.temperature}</td><td>${row.humidity}</td>`
        }
    })
}, 20000);


