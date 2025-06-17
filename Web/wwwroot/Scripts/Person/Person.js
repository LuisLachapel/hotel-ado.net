window.onload = function () {
    PersonList();
    fillSelect();
    previewImage();
}


function previewImage() {
    var uploadPhoto = document.getElementById("photoInput");
    var imgPhoto = document.getElementById("previewImage");
    uploadPhoto.onchange = function () {
        var file = uploadPhoto.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onloadend = function () {
                imgPhoto.src = reader.result;
                imgPhoto.style.display = "block";
            };
            reader.readAsDataURL(file);
        }
    }
}

function Clear() {
    ClearValues("formPerson")
    const previewImage = document.getElementById("previewImage");
    previewImage.removeAttribute("src");
    previewImage.style.display = "none";
}

async function Edit(id) {
    await fillSelect();
    await setValues(`Person/Get/?id=${id}`, "formPerson");

    const imgPreview = document.getElementById("previewImage");

    // Asignar el manejador de error primero
    imgPreview.onerror = function () {
        imgPreview.style.display = "none";
        imgPreview.removeAttribute("src"); // <-- evita imagen rota o alt visible
    };

    imgPreview.onload = function () {
        imgPreview.style.display = "block";
    };

    imgPreview.src = `/Person/GetPhoto?id=${id}`;
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

    // Validación HTML5 manual
    if (!data.checkValidity()) {
        data.reportValidity(); // Muestra errores en pantalla
        return; // Detiene el envío
    }

    var form = new FormData(data);
    fetch("Person/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
            console.log([...form.entries()]);
            PersonList();
        });
}



function Delete(id) {
    deleteRow(id,"Person/DeleteData",PersonList)

}


