window.onload = function () {
    RoomTypeList();
}

function RoomTypeList() {
    CreateTable({
        url: "RoomType/List",
        id: "table-roomtype",
        headers: ["id", "nombre", "descripción"],
        properties: ["id", "name", "description"],
        propierty_id: "id"
    }, {
        search: true,
        input_txt: "txtRoomType",
        container_id: "search-roomtype", // NUEVO: ID donde irá el buscador
        custom_search_url: (value) => `RoomType/FilterRoomType/?parameter=${value}`,
        propierty_id: "id"
    });
}


function SaveData() {
    var data = document.getElementById("formRoomtype");
    var form = new FormData(data);
    fetch("RoomType/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {

            RoomTypeList();
        })
}

function Clear() {
    ClearValues("formRoomtype")
}

function Edit(id) {
    setValues(`RoomType/GetById/?id=${id}`,"formRoomtype")
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
            fetch(`RoomType/Delete?id=${id}`, {
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
                        RoomTypeList(); // Refresca la tabla
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