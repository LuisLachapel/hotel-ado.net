window.onload = function () {
    PersonList();
    fillSelect();
}



async function fillSelect() {
    const response = await fetch("UserType/List");
    const data = await response.json();
    createSelect(data, "selectUserType", "name", "id");
}

var table_parameters; // En esta variable se guardan los datos y parametros de las tablas
function PersonList() {
    table_parameters   = {
        url: "Person/List",
        id: "table-person",
        headers: ["id", "nombre", "sexo", "Tipo"], //Cabeceras de la tabla
        properties: ["id", "name", "sex", "userType"] // propiedades de la tabla de la db
    }

    CreateTable(table_parameters)
}

function searchByUserType() {
    var id = getValue("selectUserType");
    table_parameters.url = `Person/Filter/?id=${id}`;
    CreateTable(table_parameters)
}

function Clear() {
    ClearValues("formPerson")
}

function Edit(id) {
    setValues(`Person/Get/?id=${id}`, "formPerson")
}

