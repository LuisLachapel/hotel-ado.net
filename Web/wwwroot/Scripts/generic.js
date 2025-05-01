const getValue = (parameter) => {
    return document.getElementById(parameter).value;
}

function CreateTable(parameters) {
    fetch(parameters.url)
        .then(res => res.json())
        .then(res => {

            var content = "<table class='table'>"
            content += "<tr>"
            parameters.headers.forEach(header => {
                content += `<th>${header}</th>`
            })
            content += "</tr>"
            
            res.forEach(row => {
                content += "<tr>"
                parameters.properties.forEach(element => {
                    content += `<td>${row[element]}</td>`
                })
                content += "<tr>"
            })

            content += "</table>"
            document.getElementById(parameters.id).innerHTML = content;

            /*alert(res)*/
        })
}