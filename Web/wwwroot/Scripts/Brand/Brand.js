window.onload = function() {
    BrandList();
}

function Clear() {
    ClearValues("formBrand")
}

function SaveData() {
    var data = document.getElementById("formBrand");
    var form = new FormData(data);
    fetch("Brand/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {

            BrandList()
        })
}

function Edit(id) {
    setValues(`Brand/GetById/?id=${id}`, "formBrand")
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
            fetch(`Brand/Delete?id=${id}`, {
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
                        BrandList(); // Refresca la tabla
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

function BrandList(){
    CreateTable({
        url: "Brand/List",
        id: "table-brand",
        headers: ["Id", "Nombre", "Descripción"],
        properties: ["id", "name", "description"],
        propierty_id: "id"
    },
        {
            search: true,
            input_txt: "txtBrand",
            container_id: "search-brand",
            custom_search_url: (value) => `Brand/FilterBrand/?parameter=${value}`
        },
        {
            type: "fieldset",
            legend: "Datos de la marca",
            form_id: "formBrand",
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
        }    )
}