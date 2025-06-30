window.onload = function () {
    UserTypeList();
    fillSelect();
    
}



function Clear() {
    ClearValues("formUserType")
}

async function Edit(id) {
    await fillSelect();
    await setValues(`UserType/Get/?id=${id}`, "formUserType");

  
}





async function fillSelect() {
    const response = await fetch("UserType/List");
    const data = await response.json();
    createSelect(data, "selectUserType", "name", "id");
}

var table_parameters; // En esta variable se guardan los datos y parametros de las tablas
function UserTypeList() {
    table_parameters = {
        url: "UserType/List",
        id: "table-UserType",
        headers: ["id", "nombre", "descripción"], //Cabeceras de la tabla
        properties: ["id", "name", "description"], // propiedades de la tabla de la db
        propierty_id: "id"
    }

    CreateTable(table_parameters)
}



function SaveData() {
    var data = document.getElementById("formUserType");

    // Validación HTML5 manual
    if (!data.checkValidity()) {
        data.reportValidity(); // Muestra errores en pantalla
        return; // Detiene el envío
    }

    var form = new FormData(data);
    fetch("UserType/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
            console.log([...form.entries()]);
             UserTypeList();
        });
}



function Delete(id) {
    deleteRow(id, "UserType/DeleteData",  UserTypeList)

}


