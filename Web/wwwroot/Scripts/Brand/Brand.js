window.onload = function() {
    BrandList();
}

function BrandList(){
    CreateTable({
        url: "Brand/List",
        id: "table-brand",
        headers: ["Id", "Nombre", "Descripción"],
        properties: ["id", "name","description"]
    },
        {
            search: true,
            input_txt: "txtBrand",
            container_id: "search-brand",
            custom_search_url: (value) => `Brand/FilterBrand/?parameter=${value}`
        })
}