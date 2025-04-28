window.onload = function () {
    TypeRoomList();
}

function TypeRoomList() {
    CreateTable({
        url: "TypeRoom/List",
        id: "table-data",
        headers: ["id", "nombre", "descripcion"],
        properties: ["id", "name", "description"]
    })
} 

