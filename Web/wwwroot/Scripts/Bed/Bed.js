window.onload = function () {
    BedList();
}

function Clear() {
    ClearValues("formBed")
}

function SaveData() {
    var data = document.getElementById("formBed");
    var form = new FormData(data);
    fetch("Bed/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {

            BedList()
        })
}

function Edit(id) {
    setValues(`Bed/GetById/?id=${id}`, "formBed")
}

function Delete(id) {
    Swal.fire({
        title: "¿Estás seguro de eliminar este registro?",
        text: "Esta acción no se puede deshacer.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(`Bed/Delete?id=${id}`, {
                method: "POST"
            })
                .then(res => res.text())
                .then(response => {
                    if (parseInt(response) > 0) {
                        Swal.fire(
                            "¡Eliminado!",
                            "El registro ha sido eliminado.",
                            "success"
                        );
                        BedList(); // Refresca la tabla
                    } else {
                        Swal.fire(
                            "Error",
                            "No se pudo eliminar el registro.",
                            "error"
                        );
                    }
                })
                .catch(error => {
                    console.error("Error al eliminar:", error);
                    Swal.fire("Error", "Ocurrió un error al eliminar.", "error");
                });
        }
    });

}

function BedList() {
    CreateTable({
        url: "Bed/List",
        id: "table-bed",
        headers: ["id", "nombre", "descripción"],
        properties: ["id", "name", "description"],
        propierty_id: "id"
    }, {
        search: true,
        input_txt: "txtBed",
        container_id: "search-bed",
        custom_search_url: (value) => `Bed/FilterBeds/?parameter=${value}`
    }, {
        type: "fieldset",
        legend: "Datos de la cama",
        form_id: "formBed",
        onSave: "SaveData",
        onClear: "Clear",
        form: [
            [
                { type: "number", label: "Id", name: "id", value: "0", readonly: true },
                { type: "text", label: "Nombre", name: "name", value: "", readonly: false }

            ],
            [
                { type: "textarea", label: "descripción", name: "description", value: "", row: "5", col: "20", readonly: false }
            ]
        ]
    });
}
