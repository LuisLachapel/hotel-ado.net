window.onload = function () {
    BedList();
}

function Clear() {
    ClearValues("formBed")
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
