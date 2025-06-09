const getValue = (parameter) => {
    return document.getElementById(parameter).value;
}

const setValue = (id,value) => {
    document.getElementById(id).value = value;
}

const setValues = (url, idForm, exceptions = []) => {
    const elements = document.querySelectorAll(`#${idForm} [name]`);

    fetch(url)
        .then(res => res.json())
        .then(data => {
            elements.forEach(element => {
                const fieldName = element.name;

                if (exceptions.includes(fieldName)) return;

                const valueToSet = data[fieldName];

                // Si no hay valor, lo ignora
                if (valueToSet === undefined || valueToSet === null) return;

                if (element.type === "radio") {
                    if (element.value === valueToSet.toString()) {
                        element.checked = true;
                    }
                } else if (element.tagName === "SELECT") {
                    element.value = valueToSet.toString();
                } else {
                    element.value = valueToSet;
                }
            });
        });
};





const ClearValues = (id) => {
    document.getElementById(id).reset()
}


var globalParameters;
var globalSearchConfig;

function buildTable(data, parameters) {
    let content = "<table class='table'>";
    content += "<tr>";
    parameters.headers.forEach(header => {
        content += `<th>${header}</th>`;
    });
    content += "<th>Operaciones</th>"
    content += "</tr>";

    data.forEach(row => {
        content += "<tr>";
        parameters.properties.forEach(prop => {
            content += `<td>${row[prop]}</td>`;
        });

        //Botones
        
        //editar
        content += "<td>"
        content += `<i class=" btn btn-primary bi bi-pencil-square" onclick = "Edit(${row[parameters.propierty_id]})" ></i>`
        content += `<i class=" btn btn-danger bi bi-trash" onclick="Delete(${row[parameters.propierty_id]})" ></i>`
        content += "</td>"

        ////eliminar
        //content += "<td>"
        //
        //content += "</td>"
       

        content += "</tr>";
    });

    content += "</table>";

    // solo reemplaza el contenido de la tabla, no del buscador
    document.getElementById(parameters.id).innerHTML = content;
}

function CreateTable(parameters, searchConfig = null, formParameters = null) {
    globalParameters = parameters;
    globalSearchConfig = searchConfig;

    // Si se debe agregar buscador
    if (searchConfig?.search) {
        const searchHTML = `
        <div class="input-group mb-3">
            <input type="text" id="${searchConfig.input_txt}" class="form-control" placeholder="Buscar..." aria-label="Buscar">
            <button class="btn btn-outline-secondary" type="button" onclick="PerformSearch()">Buscar</button>
        </div>
        `;
        document.getElementById(searchConfig.container_id).innerHTML = searchHTML;

        const inputElement = document.getElementById(searchConfig.input_txt);
        inputElement.addEventListener("keyup", function (event) {
            if (event.key === "Enter") {
                PerformSearch();
            }
        });
    }

    // Construcción de tabla
    fetch(parameters.url)
        .then(res => res.json())
        .then(res => {
            buildTable(res, parameters);
        });

    // Construcción del formulario (fuera del bloque del buscador)
    if (formParameters) {
        const formHTML = buildForm(formParameters);
        document.getElementById("form-container").innerHTML = formHTML;
    }
}


// Se encarga de realizar la búsqueda
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


function buildForm(formParameters) {
    const type = formParameters.type || "fieldset";
    const rows = formParameters.form || [];
    const formId = formParameters.form_id || "generatedForm";
    const legend = formParameters.legend || "Formulario";
    const onSave = formParameters.onSave || "SaveData";
    const onClear = formParameters.onClear || "Clear";

    let content = `<${type} class="container mb-4" style="border: 1px solid black;">`;

    if (type === "fieldset") {
        content += `<legend style="font-size: 14px;">${legend}</legend>`;
    }

    content += `<form id="${formId}" method="post">`;

    // Campos estructurados por fila
    rows.forEach(row => {
        content += `<div class="row">`;

        row.forEach(field => {
            const inputType = field.type || "text";
            const label = field.label || "";
            const name = field.name || "";
            const value = field.value ?? "";
            const readonly = field.readonly ? "readonly" : "";
            const col = field.col || 12 / row.length;
            const nroRow = field.row; 



            const id = field.id || `form_${name}`;

            if (inputType === "textarea") {
                content += `
        <div class="mb-3 col-md-${col}">
            <label for="${id}" class="form-label">${label}</label>
            <textarea class="form-control" id="${id}" name="${name}" ${readonly} rows="${nroRow }">${value}</textarea>
        </div>`;
            } else {
                content += `
        <div class="mb-3 col-md-${col}">
            <label for="${id}" class="form-label">${label}</label>
            <input type="${inputType}" class="form-control" id="${id}" name="${name}" value="${value}" ${readonly}>
        </div>`;
            }
        });

        content += `</div>`;
    });

    // Botones
    content += `
        <div class="mb-3"> 
            <button type="button" class="btn btn-primary" onclick="${onSave}()">Aceptar</button>
            <button type="button" class="btn btn-danger" onclick="${onClear}()">Limpiar</button>
        </div>
    `;

    content += `</form></${type}>`;

    return content;
}



function createSelect(data, id, propertie, propertie_id) {
    let content = `<option value=''>--Seleccione--</option>`;
    for (let position = 0; position < data.length; position++) {
        const element = data[position];
        content += `<option value="${element[propertie_id]}">${element[propertie]}</option>`;
    }
    document.getElementById(id).innerHTML = content;
}

