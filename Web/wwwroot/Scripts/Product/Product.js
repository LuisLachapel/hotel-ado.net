window.onload = function () {
    ProductList();
    fillSelect();
    fillSelectByCategory();
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
        properties: ["id", "name", "brandName", "salePrice", "stock", "denomination"]
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
