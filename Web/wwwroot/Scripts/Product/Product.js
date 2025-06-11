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
            console.log([...form.entries()])
            ProductList();
        })
}

function Delete(id) {
    Swal.fire({
        title: "¿Estás seguro de eliminar este registro?",
        text: "Esta acción no se puede deshacer.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(`Product/DeleteProduct?id=${id}`, {
                method: "POST"
            })
                .then(res => res.text())
                .then(response => {
                    if (parseInt(response) > 0) {
                        Swal.fire(
                            "¡Eliminado!",
                            "El registro ha sido eliminado.",
                            "success"
                        );
                        ProductList(); // Refresca la tabla
                    } else {
                        Swal.fire(
                            "Error",
                            "No se pudo eliminar el registro.",
                            "error"
                        );
                    }
                })
                .catch(error => {
                    console.error("Error al eliminar:", error);
                    Swal.fire("Error", "Ocurrió un error al eliminar.", "error");
                });
        }
    });

}