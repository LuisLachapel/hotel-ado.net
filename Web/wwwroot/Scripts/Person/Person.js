window.onload = function () {
    PersonList();
    fillSelect();
}

function Clear() {
    ClearValues("formPerson")
}

async function Edit(id) {
    await fillSelect(); // Esperamos que se llene el select
    setValues(`Person/Get/?id=${id}`, "formPerson");
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
        properties: ["id", "name", "sex", "userType"], // propiedades de la tabla de la db
        propierty_id: "id"
    }

    CreateTable(table_parameters)
}

function searchByUserType() {
    var id = getValue("selectUserType");
    table_parameters.url = `Person/Filter/?id=${id}`;
    CreateTable(table_parameters)
}


function SaveData() {
    var data = document.getElementById("formPerson");
    var form = new FormData(data);
    fetch("Person/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
            console.log([...form.entries()])
            PersonList();
        })
}



