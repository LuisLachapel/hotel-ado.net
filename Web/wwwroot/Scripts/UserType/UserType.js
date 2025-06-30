window.onload = function () {
    UserTypeList();
    loadPageParameters();
    fillSelect();
    
}

async function loadPageParameters() {
    try {
        const response = await fetch("Page/List");
        const data = await response.json();

        const tableBody = document.querySelector("#tablePageData tbody");
        tableBody.innerHTML = ""; // Limpia la tabla

        data.forEach(item => {
            const row = document.createElement("tr");

            row.innerHTML = `
                <td><input type="checkbox" class="param-checkbox" value="${item.id}" data-message="${item.message}"></td>
                <td>${item.id}</td>
                <td>${item.message}</td>
            `;

            tableBody.appendChild(row);
        });

    } catch (error) {
        console.error("Error al cargar parámetros:", error);
    }
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
        return;
    }

    var form = new FormData(data);

    // ✅ Agregar checkbox seleccionados
    const selectedParams = document.querySelectorAll(".param-checkbox:checked");
    selectedParams.forEach(checkbox => {
        form.append("pages", checkbox.value);
    });

    
    fetch("UserType/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
            console.log("Respuesta del backend:", res);
            UserTypeList();
        });
}





function Delete(id) {
    deleteRow(id, "UserType/DeleteData",  UserTypeList)

}


