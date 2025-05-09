const getValue = (parameter) => {
    return document.getElementById(parameter).value;
}

const setValue = (id,value) => {
    document.getElementById(id).value = value;
}

const setValues = (url,idForm, exceptions = []) => {
    const elements = document.querySelectorAll(`#${idForm} [name]`);

    fetch(url)
        .then(res => res.json())
        .then(data => {
            elements.forEach(element => {
                const fieldName = element.name;
                if (!exceptions.includes(fieldName) && data.hasOwnProperty(fieldName)) {
                    element.value = data[fieldName]; 
                }
            });
        })
}

const ClearValues = (id) => {
    document.getElementById(id).reset()
}


const DeleteAlert = () => {
    Swal.fire({
        title: "¿Estas seguro de eliminar este registro?",
        text: "El registro no se podra recuperar",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, Eliminalo!",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "Registro eliminado!",
                text: "Este registro ha sido eliminado.",
                icon: "success"
            });
        }
    });
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

        // Agrega evento para Enter
        const inputElement = document.getElementById(searchConfig.input_txt);
        inputElement.addEventListener("keyup", function (event) {
            if (event.key === "Enter") {
                PerformSearch();
            }
        });
    }

    fetch(parameters.url)
        .then(res => res.json())
        .then(res => {
            buildTable(res, parameters);
        });
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
