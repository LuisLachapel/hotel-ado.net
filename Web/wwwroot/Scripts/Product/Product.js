window.onload = function () {
    ProductList();
    fillSelect();
}


async function fillSelect() {
    const response = await fetch("Brand/List");
    const data = await response.json();
    createSelect(data, "selectBrand", "name", "id");
}

function ProductList() {
    CreateTable({
        url: "Product/List",
        id: "table-product",
        headers: ["id", "nombre", "marca", "precio", "stock", "denominacion"],
        properties: ["id", "name", "brandName", "salePrice","stock", "denomination"]
    }, {
        search: true,
        input_txt: "txtProduct",
        container_id: "search-product",
        custom_search_url: (value) => `Product/FilterProduct/?parameter=${value}`
        
    })
}

function searchByBrand() {
    var id = getValue("selectBrand");
    CreateTable({
        url: `Product/FilterProductByBrand/?id=${id}`,
        id: "table-product",
        headers: ["id", "nombre", "marca", "precio", "stock", "denominacion"],
        properties: ["id", "name", "brandName", "salePrice", "stock", "denomination"]
    })
    

}
