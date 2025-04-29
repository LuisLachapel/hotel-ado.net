window.onload = function () {
    BedList()
}


function BedList() {
    CreateTable({
        url: "Bed/List",
        id: "table-bed",
        headers: ["id", "nombre", "descripción"],
        properties: ["id", "name", "description"]
       
    })
}
