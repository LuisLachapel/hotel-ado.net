window.onload = function () {
    BedList();
}

function BedList() {
    CreateTable({
        url: "Bed/List",
        id: "table-bed",
        headers: ["id", "nombre", "descripción"],
        properties: ["id", "name", "description"]
    }, {
        search: true,
        input_txt: "txtBed",
        container_id: "search-bed",
        custom_search_url: (value) => `Bed/FilterBeds/?parameter=${value}`
    });
}
