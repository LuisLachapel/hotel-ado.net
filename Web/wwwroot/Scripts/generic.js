function CreateTable(parameters) {
    fetch(parameters.url)
        .then(res => res.json())
        .then(res => {

            var content = "<table class='table'>"
            content += "<tr>"
            for (var header = 0; header < parameters.headers.length; header++) {
                content += "<th>" + parameters.headers[header] + "</th>"
            }
            content += "</tr>"
            var row;
            var currentProperty;
            for (var element = 0; element < res.length; element++) {
                row = res[element];
                content += "<tr>";
                for (var property = 0; property < parameters.properties.length; property++) {
                    currentProperty = parameters.properties[property];
                    content += "<td>" + row[currentProperty] + "</td>"
                }
                content += "</tr>";
            }

            content += "</table>"
            document.getElementById(parameters.id).innerHTML = content;

            /*alert(res)*/
        })
}