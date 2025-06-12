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
    deleteRow(id, "RoomType/Delete", RoomTypeList)

}