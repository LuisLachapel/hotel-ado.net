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
    deleteRow(id, "Bed/Delete", BedList)
}

function BedList() {
    CreateTable({
        url: "Bed/List",
        id: "table-bed",
        headers: ["id", "nombre", "descripción"],
        properties: ["id", "name", "description"],
        propierty_id: "id"
    });
}
