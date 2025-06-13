window.onload = function () {
    CategoryList();
}


var table_parameters;


function CategoryList() {
    table_parameters = {
        url: "Category/List",
        id: "table-category",
        headers: ["id", "nombre", "descripcion"],
        properties: ["id", "name", "description"],
        propierty_id: "id"
    }

    CreateTable(table_parameters)
}


 function Edit(id) {
    
    setValues(`Category/GetById/?id=${id}`, "formCategory");
}


function Clear() {
    ClearValues("formCategory")
}

function SaveData() {
    var data = document.getElementById("formCategory");
    var form = new FormData(data);
    fetch("Category/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
            // console.log([...form.entries()]) se utiliza para mostrar en consola todos los campos y valores de un objeto FormData
            CategoryList();
        })
}

function Delete(id) {
    deleteRow(id, "Category/DeleteData", CategoryList)
}
