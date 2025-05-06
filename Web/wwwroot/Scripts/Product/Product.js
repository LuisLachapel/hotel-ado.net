window.onload = function () {
    ProductList();
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
