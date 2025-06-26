window.onload = function () {
    RoomList();
    fillSelectByBed();
    fillSelectByRoomType();
    fillSelectHotels() 
    validateNumberInputs();
}


var table_parameters;

async function fillSelectByBed() {
    const response = await fetch("Room/GetList");
    const data = await response.json();
    createSelect(data.beds, "selectBeds", "name", "id");
}


async function fillSelectByRoomType() {
    const response = await fetch("Room/GetList");
    const data = await response.json();
    createSelect(data.roomsType, "selectRoomType", "name", "id");
}

async function fillSelectHotels() {
    const response = await fetch("Room/GetList");
    const data = await response.json();
    createSelect(data.hotels, "selectHotel", "name", "id");
}




function RoomList() {
    table_parameters = {
        url: "Room/List",
        id: "table-room",
        headers: ["id", "nombre", "precio x noche", "# personas", "piscina", "Wifi","Vista al mar"],
        properties: ["id", "name", "priceByNight", "numberOfPeople", "hasPool", "hasWifi", "hasSeaView"],
        propierty_id: "id"
    }

    CreateTable(table_parameters)
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
    setValues(`Product/Get/?id=${id}`, "formRoom");
}


function Clear() {
    ClearValues("formRoom")
}

function SaveData() {
    var data = document.getElementById("formRoom");
    var form = new FormData(data);
    fetch("Room/SaveData", {
        method: "POST",
        body: form
    }).then(res => res.text())
        .then(res => {
            // console.log([...form.entries()]) se utiliza para mostrar en consola todos los campos y valores de un objeto FormData
            RoomList();
        })
}

function Delete(id) {
    deleteRow(id, "Product/DeleteProduct", ProductList)
}