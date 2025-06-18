window.onload = function () {
    HotelList();
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
    ClearValues("formHotel")
    const previewImage = document.getElementById("previewImage");
    previewImage.removeAttribute("src");
    previewImage.style.display = "none";
}

async function Edit(id) {
    await fillSelect();
    await setValues(`Hotel/Get/?id=${id}`, "formHotel");

    const imgPreview = document.getElementById("previewImage");

    // Asignar el manejador de error primero
    imgPreview.onerror = function () {
        imgPreview.style.display = "none";
        imgPreview.removeAttribute("src"); // <-- evita imagen rota o alt visible
    };

    imgPreview.onload = function () {
        imgPreview.style.display = "block";
    };

    imgPreview.src = `/Hotel/GetPhoto?id=${id}`;
}



var table_parameters; // En esta variable se guardan los datos y parametros de las tablas
function HotelList() {
    table_parameters = {
        url: "Hotel/List",
        id: "table-hotel",
        headers: ["id", "nombre", "descripción", "dirección"], //Cabeceras de la tabla
        properties: ["id", "name", "description", "address"], // propiedades de la tabla de la db
        propierty_id: "id"
    }

    CreateTable(table_parameters)
}

function SaveData() {
    var data = document.getElementById("formHotel");

    // Validación HTML5 manual
    if (!data.checkValidity()) {
        data.reportValidity(); // Muestra errores en pantalla
        return; // Detiene el envío
    }

    var form = new FormData(data);
    fetch("Hotel/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
            console.log([...form.entries()]);
            HotelList();
        });
}



function Delete(id) {
    deleteRow(id, "Hotel/DeleteData", HotelList)

}
