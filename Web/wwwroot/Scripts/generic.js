const getValue = (parameter) => {
    return document.getElementById(parameter).value;
}

var globalParameters;
var globalSearchConfig;

function buildTable(data, parameters) {
    let content = "<table class='table'>";
    content += "<tr>";
    parameters.headers.forEach(header => {
        content += `<th>${header}</th>`;
    });
    content += "</tr>";

    data.forEach(row => {
        content += "<tr>";
        parameters.properties.forEach(prop => {
            content += `<td>${row[prop]}</td>`;
        });
        content += "</tr>";
    });

    content += "</table>";

    // Ahora solo reemplaza el contenido de la tabla, no del buscador
    document.getElementById(parameters.id).innerHTML = content;
}

function CreateTable(parameters, searchConfig = null) {
    globalParameters = parameters;
    globalSearchConfig = searchConfig;

    // Si se debe agregar buscador, lo hace en un contenedor independiente
    if (searchConfig?.search) {
        const searchHTML = `
        <div class="input-group mb-3">
            <input type="text" id="${searchConfig.input_txt}" class="form-control" placeholder="Buscar..." aria-label="Buscar">
            <button class="btn btn-outline-secondary" type="button" onclick="PerformSearch()">Buscar</button>
        </div>
    `;
        document.getElementById(searchConfig.container_id).innerHTML = searchHTML;
    }


    fetch(parameters.url)
        .then(res => res.json())
        .then(res => {
            buildTable(res, parameters);
        });
}


//Se encarga de realizar la busqueda
function PerformSearch() {
    const searchValue = getValue(globalSearchConfig.input_txt);

    const searchUrl = globalSearchConfig.custom_search_url
        ? globalSearchConfig.custom_search_url(searchValue)
        : `${globalParameters.url}?parameter=${searchValue}`;

    fetch(searchUrl)
        .then(res => res.json())
        .then(res => {
            buildTable(res, globalParameters);
        });
}
