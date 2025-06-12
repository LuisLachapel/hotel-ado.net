window.onload = function () {
    ProductList();
    fillSelect();
    fillSelectByCategory();
    validateNumberInputs();
}


var table_parameters;

async function fillSelect() {
    const response = await fetch("Brand/List");
    const data = await response.json();
    createSelect(data, "selectBrand", "name", "id");
}


async function fillSelectByCategory() {
    const response = await fetch("Product/CategoryList");
    const data = await response.json();
    createSelect(data, "selectCategory", "name", "id");
}




function ProductList() {
    table_parameters = {
        url: "Product/List",
        id: "table-product",
        headers: ["id", "nombre", "marca", "precio", "stock", "denominacion"],
        properties: ["id", "name", "brandName", "salePrice", "stock", "denomination"],
        propierty_id: "id"
    }

    CreateTable(table_parameters, {
        search: true,
        input_txt: "txtProduct",
        container_id: "search-product",
        custom_search_url: (value) => `Product/FilterProduct/?parameter=${value}`
        
    })
}

function searchByBrand() {
    var id = getValue("selectBrand");
    table_parameters.url = `Product/FilterProductByBrand/?id=${id}`
    CreateTable(table_parameters)
    

}

function searchByCategory() {
    var id = getValue("selectCategory");
    table_parameters.url = `Product/FilterProductByCategory/?id=${id}`
    CreateTable(table_parameters)


}

async function Edit(id) {
    await fillSelect(); // Esperamos que se llene el select
    setValues(`Product/Get/?id=${id}`, "formProduct");
}


function Clear() {
    ClearValues("formProduct")
}

function SaveData() {
    var data = document.getElementById("formProduct");
    var form = new FormData(data);
    fetch("Product/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
           // console.log([...form.entries()]) se utiliza para mostrar en consola todos los campos y valores de un objeto FormData
            ProductList();
        })
}

function Delete(id) {
    deleteRow(id, "Product/DeleteProduct", ProductList)
}