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


function Search() {
    var roomTypeName = getValue("txtRoomType");
    CreateTable({
        url: `RoomType/FilterRoomType/?parameter=${roomTypeName}`,
        id: "table-roomtype",
        headers: ["id", "nombre", "descripción"],
        properties: ["id", "name", "description"]
    })
   
}
