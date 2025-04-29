window.onload = function () {
    RoomTypeList();
}

function RoomTypeList() {
    CreateTable({
        url: "RoomType/List",
        id: "table-roomtype",
        headers: ["id", "nombre", "descripción"],
        properties: ["id", "name", "description"]
    })
} 

